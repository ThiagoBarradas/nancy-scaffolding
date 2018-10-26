using Nancy.Swagger;
using Nancy.Swagger.Services;
using WebApi.Models.Response;

namespace Nancy.Scaffolding.Docs
{
    public class ErrorsResponseModelDataProvider :
        GenericModelDataProvider<ErrorsResponse>, ISwaggerModelDataProvider
    { }

    public class ErrorItemResponseModelDataProvider :
        GenericModelDataProvider<ErrorItemResponse>, ISwaggerModelDataProvider
    { }

    public class GenericModelDataProvider<T>
    {
        public SwaggerModelData GetModelData()
        {
            return SwaggerModelData.ForType<T>(with =>
            {
                with.Description(string.Empty);
            });
        }
    }
}
