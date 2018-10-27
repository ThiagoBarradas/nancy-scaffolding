using AutoMapper;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Nancy.Scaffolding.Models
{
    public class ApiBasicConfiguration
    {
        public string ApiName { get; set; }

        public int ApiPort { get; set; }

        public string EnvironmentVariablesPrefix { get; set; }

        public IEnumerable<Assembly> AutoRegisterAssemblies { get; set; }

        public Func<NancyContext, TinyIoCContainer, TinyIoCContainer> RequestContainer { get; set; }
        
        public Func<TinyIoCContainer, TinyIoCContainer> ApplicationContainer { get; set; }

        public Func<IPipelines, TinyIoCContainer, IPipelines> Pipelines { get; set; }

        public Func<IMapperConfigurationExpression, TinyIoCContainer, IMapperConfigurationExpression> Mapper { get; set; }
    }
}
