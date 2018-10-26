﻿using Nancy.Conventions;
using Nancy.Scaffolding.Modules;
using Nancy.Swagger;
using Nancy.Swagger.Annotations;
using Nancy.Swagger.Services;
using Swagger.ObjectModel;
using System;
using System.IO;
using WebApi.Models.Response;

namespace Nancy.Scaffolding.Docs
{
    public class SwaggerConfiguration
    {
        public static void Register()
        {
            if (Api.DocsSettings.Enabled == true)
            {
                var docsDescription = AppDomain.CurrentDomain.BaseDirectory + "/Docs/DOCS-DESCRIPTION.md";

                if (File.Exists(docsDescription))
                {
                    string readme = File.ReadAllText(docsDescription);
                }

                var contact = new Contact()
                {
                    EmailAddress = Api.DocsSettings.AuthorEmail,
                    Name = Api.DocsSettings.AuthorName,
                    Url = Api.ApiSettings.AppUrl
                };

                SwaggerConfig.ResourceListingPath = BaseModule.GetModulePath();
                SwaggerMetadataProvider.SetInfo(Api.DocsSettings.Title, Api.ApiSettings.Version, Api.DocsSettings.Description, contact, Api.DocsSettings.TermsOfService);
                SwaggerAnnotationsConfig.ShowOnlyAnnotatedRoutes = true;
                SwaggerTypeMapping.AddTypeMapping(typeof(Guid), typeof(string));
            }
        }

        public static void AddConventions(NancyConventions conventions)
        {
            var pathPrefix = BaseModule.GetModulePath();

            conventions.StaticContentsConventions.Add(
                StaticContentConventionBuilder.AddDirectory(pathPrefix + "/Docs/Assets", @"Docs/Assets")
            );

            conventions.StaticContentsConventions.Add(
                StaticContentConventionBuilder.AddDirectory(pathPrefix + "/Docs/Redoc", @"Docs/Redoc")
            );
        }
    }
}