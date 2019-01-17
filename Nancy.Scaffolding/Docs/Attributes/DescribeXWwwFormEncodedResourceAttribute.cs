using Nancy.Swagger.Annotations.Attributes;
using System;
using System.Text.RegularExpressions;

namespace Nancy.Scaffolding.Docs.Attributes
{
    public class DescribeXWwwFormEncodedResourceAttribute : DescribeResourceBase
    {
        public DescribeXWwwFormEncodedResourceAttribute(Type controllerType, string methodName)
            : base(controllerType, methodName, "application/x-www-form-urlencoded", "application/x-www-form-urlencoded")
        { }
    }
}
