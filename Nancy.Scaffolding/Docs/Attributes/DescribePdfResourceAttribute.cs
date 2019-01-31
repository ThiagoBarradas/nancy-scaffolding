using System;

namespace Nancy.Scaffolding.Docs.Attributes
{
    public class DescribePdfResourceAttribute : DescribeResourceBase
    {
        public DescribePdfResourceAttribute(Type controllerType, string methodName)
            : base(controllerType, methodName, "application/pdf", "text/plain")
        { }
    }
}
