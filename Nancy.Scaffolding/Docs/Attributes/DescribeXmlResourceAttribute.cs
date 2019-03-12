using System;

namespace Nancy.Scaffolding.Docs.Attributes
{
    public class DescribeXmlResourceAttribute : DescribeResourceBase
    {
        public DescribeXmlResourceAttribute(Type controllerType, string methodName)
            : base(controllerType, methodName, "application/xml", "application/xml")
        { }
    }
}
