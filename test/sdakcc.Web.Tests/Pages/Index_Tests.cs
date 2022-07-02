using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace sdakcc.Pages;

public class Index_Tests : sdakccWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
