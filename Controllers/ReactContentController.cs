using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Web.Common;
using MyCustomUmbracoProject.Models;
using ContentModels = Umbraco.Cms.Web.Common.PublishedModels;

public class ReactContentController : ControllerBase
{
    private UmbracoHelper _umbracoHelper;
    private readonly ILogger<ReactContentController> _logger;

    public ReactContentController(ILogger<ReactContentController> logger, UmbracoHelper umbracoHelper)
    {
        _logger = logger;
        _umbracoHelper = umbracoHelper;

    }

    [HttpGet]
    public ActionResult<HomepageDTO> Get()
    {
        var model = this._umbracoHelper?.ContentAtRoot()?.DescendantsOrSelf<ContentModels.Homepage>().FirstOrDefault() ?? null;

        return new HomepageDTO()
        {
            Title = model?.Title ?? "",
            //  ImageUrl=model?.Image?.Src??""
        };
    }
}