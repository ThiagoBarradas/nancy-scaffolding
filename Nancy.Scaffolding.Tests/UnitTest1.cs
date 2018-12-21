using Nancy.Testing;
using Xunit;

namespace Nancy.Scaffolding.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var bootstrapper = new Bootstrapper();
            var browser = new Browser(bootstrapper);
        }
    }
}
