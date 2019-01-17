using System;

namespace Nancy.Scaffolding.Docs.Attributes
{
    public class DescribeTextResourceAttribute : DescribeResourceBase
    {
        public DescribeTextResourceAttribute(Type controllerType, string methodName)
            : base(controllerType, methodName, "text/plain", "text/plain")
        { }
    }
}
