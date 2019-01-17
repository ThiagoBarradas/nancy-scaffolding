using System;

namespace Nancy.Scaffolding.Docs.Attributes
{
    public class DescribeJsonResourceAttribute : DescribeResourceBase
    {
        public DescribeJsonResourceAttribute(Type controllerType, string methodName)
            : base(controllerType, methodName, "application/json", "application/json")
        { }
    }
}
