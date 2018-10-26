using Nancy.Swagger.Annotations.Attributes;
using Swagger.ObjectModel;
using System;

namespace Nancy.Scaffolding.Docs.Attributes
{
    public class BodyContentAttribute : RouteParamAttribute
    {
        public BodyContentAttribute(Type modelType)
            : base(ParameterIn.Body)
        {
            this.ParamType = modelType;
            this.Name = "body";
        }
    }

    public class QueryContentAttribute : RouteParamAttribute
    {
        public QueryContentAttribute(string name, Type modelType)
            : base(ParameterIn.Query)
        {
            this.ParamType = modelType;
            this.Name = name;
        }
    }

    public class PathContentAttribute : RouteParamAttribute
    {
        public PathContentAttribute(string name, Type modelType)
            : base(ParameterIn.Path)
        {
            this.ParamType = modelType;
            this.Name = name;
        }
    }
}
