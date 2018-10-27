using Nancy.Swagger.Annotations.Attributes;
using System;
using System.Text.RegularExpressions;

namespace Nancy.Scaffolding.Docs.Attributes
{
    public class DescribeJsonResourceAttribute : RouteAttribute
    {
        public DescribeJsonResourceAttribute(Type controllerType, string methodName)
            : base(methodName)
        {
            var controllerName = this.GetControllerName(controllerType);

            this.Summary = this.GetSummary(methodName);
            this.Produces = new[] { "application/json" };
            this.Consumes = new[] { "application/json" };
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
