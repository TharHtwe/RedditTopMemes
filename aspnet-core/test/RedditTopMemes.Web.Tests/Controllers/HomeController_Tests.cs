using System.Threading.Tasks;
using RedditTopMemes.Models.TokenAuth;
using RedditTopMemes.Web.Controllers;
using Shouldly;
using Xunit;

namespace RedditTopMemes.Web.Tests.Controllers
{
    public class HomeController_Tests: RedditTopMemesWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}