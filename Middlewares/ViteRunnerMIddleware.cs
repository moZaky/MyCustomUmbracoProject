using System.Diagnostics;

namespace Microsoft.AspNetCore.SpaServices.ViteDevelopmentServer
{
    public static class ViteDevelopmentServerMiddlewareExtensions
    {
        public static void UseViteDevelopmentServer(this ISpaBuilder spaBuilder, string npmScript)
        {
            spaBuilder.UseProxyToSpaDevelopmentServer(async () =>
            {
                // Launch Vite as a child process
                var viteProcess = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "npm",
                        Arguments = $"run {npmScript}",
                        WorkingDirectory = "ClientApp",
                        RedirectStandardError = true,
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    },
                    EnableRaisingEvents = true,
                };

                // Print Vite output to console
                viteProcess.OutputDataReceived += (sender, args) => Console.WriteLine(args.Data);
                viteProcess.ErrorDataReceived += (sender, args) => Console.WriteLine(args.Data);

                // Start the Vite process
                viteProcess.Start();
                viteProcess.BeginOutputReadLine();
                viteProcess.BeginErrorReadLine();

                // Wait for the Vite dev server to start
                var timeout = TimeSpan.FromSeconds(30);
                var client = new HttpClient();
                var sw = Stopwatch.StartNew();
                while (sw.Elapsed < timeout)
                {
                    try
                    {
                        var response = await client.GetAsync("http://localhost:3000/");
                        if (response.IsSuccessStatusCode)
                        {
                            break;
                        }
                    }
                    catch { }
                    await Task.Delay(100);
                }

                // Create a URI for the proxy server
                var uri = new UriBuilder("http", "localhost", 3000).Uri;

                // Return the proxy server URI
                return uri;
            });
        }
    }
}
