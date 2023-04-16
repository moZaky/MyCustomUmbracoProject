using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyCustomUmbracoProject.Models;

namespace MyCustomUmbracoProject.Interfaces
{
    public interface IContentService
    {
        GenericResult<HomepageDTO> GetHomeContent();

    }
}