using Nancy.Scaffolding.Modules;
using Nancy.Swagger;
using Nancy.Swagger.Annotations;
using Nancy.Swagger.Services;
using Newtonsoft.Json;
using Swagger.ObjectModel;
using System;

namespace Nancy.Scaffolding.Docs
{
    public class SwaggerConfiguration
    {
        public static void Register(JsonSerializerSettings jsonSerializerSettings)
        {
            if (Api.DocsSettings.Enabled == true)
            {
                //var docsDescription = AppDomain.CurrentDomain.BaseDirectory + "/Docs/DOCS-DESCRIPTION.md";
                //
                //if (File.Exists(docsDescription))
                //{
                //    string readme = File.ReadAllText(docsDescription);
                //}

                var version = (string.IsNullOrWhiteSpace(Api.ApiSettings.Version)) 
                    ? "v1" 
                    : Api.ApiSettings.Version;

                var contact = new Contact()
                {
                    EmailAddress = Api.DocsSettings.AuthorEmail,
                    Name = Api.DocsSettings.AuthorName,
                    Url = Api.ApiSettings.AppUrl
                };

                SwaggerConfig.ResourceListingPath = BaseModule.GetModulePath();
                SwaggerConfig.JsonSerializerSettings = jsonSerializerSettings;
                SwaggerMetadataProvider.SetInfo(Api.DocsSettings.Title, version, Api.DocsSettings.Description, contact, Api.DocsSettings.TermsOfService);
                SwaggerAnnotationsConfig.ShowOnlyAnnotatedRoutes = true;
                SwaggerTypeMapping.AddTypeMapping(typeof(Guid), typeof(string));
            }
        }
    }
}