using AutoMapper;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using System;

namespace Nancy.Scaffolding.Models
{
    public class ApiBasicConfiguration
    {
        public string ApiName { get; set; }

        public int ApiPort { get; set; }

        public string EnvironmentVariablesPrefix { get; set; }

        public Func<NancyContext, TinyIoCContainer, TinyIoCContainer> RequestContainer { get; set; }
        
        public Func<TinyIoCContainer, TinyIoCContainer> ApplicationContainer { get; set; }

        public Func<IPipelines, TinyIoCContainer, IPipelines> Pipelines { get; set; }

        public Func<IMapperConfigurationExpression, TinyIoCContainer, IMapperConfigurationExpression> Mapper { get; set; }
    }
}
