using Nancy.Swagger.Annotations.Attributes;
using System;
using System.Text.RegularExpressions;

namespace Nancy.Scaffolding.Docs.Attributes
{
    public abstract class DescribeResourceBase : RouteAttribute
    {
        public DescribeResourceBase(Type controllerType, string methodName, string produces, string consumes)
            : base(methodName)
        {
            var controllerName = this.GetControllerName(controllerType);

            this.Summary = this.GetSummary(methodName);
            this.Produces = new[] { produces };
            this.Consumes = new[] { consumes };
            this.Tags = new[] { controllerName };
        }

        private string GetControllerName(Type controllerType)
        {
            return controllerType.Name
                .Replace("Controller", "")
                .Replace("Module", "");
        }

        private string GetSummary(string methodName)
        {
            string[] split = Regex.Split(methodName, @"(?<!^)(?=[A-Z])");
            return string.Join(" ", split);
        }
    }
}