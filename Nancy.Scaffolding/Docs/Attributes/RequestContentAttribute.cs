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
        public QueryContentAttribute(string name, Type modelType = null)
            : base(ParameterIn.Query)
        {
            if (modelType == null)
            {
                modelType = typeof(string);
            }

            this.ParamType = modelType;
            this.Name = name;
        }
    }

    public class PathContentAttribute : RouteParamAttribute
    {
        public PathContentAttribute(string name, Type modelType = null)
            : base(ParameterIn.Path)
        {
            if (modelType == null)
            {
                modelType = typeof(string);
            }
            
            this.ParamType = modelType;
            this.Name = name;
        }
    }

    public class HeaderContentAttribute : RouteParamAttribute
    {
        public HeaderContentAttribute(string name, Type modelType = null)
            : base(ParameterIn.Header)
        {
            if (modelType == null)
            {
                modelType = typeof(string);
            }

            this.ParamType = modelType;
            this.Name = name;
        }
    }
}
