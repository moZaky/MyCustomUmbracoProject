using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCustomUmbracoProject.Models
{
    public class GenericResult<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; } = null;
        public string Error { get; set; } = null;
        public IEnumerable<string> ErrorMessages { get; set; } = Enumerable.Empty<string>();

        public GenericResult()
        {

        }
    }
    // public record GenericResult<T>(T Data, bool Success = true, string? Message = null, string? Error = null, List<string>? ErrorMessages = null);

}