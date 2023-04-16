using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyCustomUmbracoProject.Models;
using MyCustomUmbracoProject.Interfaces;

namespace MyCustomUmbracoProject
{
    [Route("api/[controller]")]
    public class ContentController : ControllerBase
    {
        private IContentService _contentService;

        public ContentController(IContentService contentService)
        {
            _contentService = contentService;
        }

        [HttpGet]
        [Route("home-content")]
        public ActionResult<HomepageDTO> FetchHomeContent()
        {
            var result = this._contentService.GetHomeContent();

            return Ok(result);
        }
    }

}