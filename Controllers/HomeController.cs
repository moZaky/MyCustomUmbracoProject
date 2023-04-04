using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using MimeKit.Encodings;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
namespace MyCustomUmbracoProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : RenderController
    {
        public HomeController(ILogger<HomeController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor)
            : base(logger, compositeViewEngine, umbracoContextAccessor)
        {

        }
        [HttpGet("/")]

        public override ActionResult Index()
        {
            return View("~/Views/Homepage.cshtml");
        }
    }
}