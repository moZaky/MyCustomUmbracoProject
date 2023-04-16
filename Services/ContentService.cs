
using MyCustomUmbracoProject.Interfaces;
using MyCustomUmbracoProject.Models;
using Umbraco.Cms.Web.Common;
using ContentModels = Umbraco.Cms.Web.Common.PublishedModels;

namespace MyCustomUmbracoProject.Services
{
    public class ContentService : IContentService
    {
        private UmbracoHelper _umbracoHelper;
        private readonly ILogger<ContentService> _logger;

        public ContentService(ILogger<ContentService> logger, UmbracoHelper umbracoHelper)
        {
            _logger = logger;
            _umbracoHelper = umbracoHelper;

        }

        public GenericResult<HomepageDTO> GetHomeContent()
        {
            GenericResult<HomepageDTO> result = new GenericResult<HomepageDTO>();
            try
            {
                var model = this._umbracoHelper?.ContentAtRoot()?.DescendantsOrSelf<ContentModels.Homepage>().FirstOrDefault() ?? null;
                result.Data = new HomepageDTO()
                {
                    Title = model?.Title ?? "",
                    ImageUrl = model?.Image?.Src ?? ""
                };
                result.Success = true;
                result.Message = "Content Fetched Successfully";
            }
            catch (System.Exception ex)
            {
                result.Success = false;
                result.Message = "Something went wrong";

                result.Error = ex.Message;
                this._logger.LogError(ex, "error while getting data for HomeContent");
            }
            return result;
        }
    }
}