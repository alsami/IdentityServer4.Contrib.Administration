using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using IdentityServer4.Contrib.Administration.Tests.Fixture;
using Xunit;
using Xunit.Abstractions;

namespace IdentityServer4.Contrib.Administration.Tests
{
    public class ClaimsControllerIntegrationTests : IClassFixture<WebApplicationFactoryFixture>
    {
        private const string ControllerBasePath = "api/v1/claims";

        private readonly HttpClient _client;

        private readonly ITestOutputHelper _testOutputHelper;
        private HttpResponseMessage _response = null!;

        public ClaimsControllerIntegrationTests(WebApplicationFactoryFixture webApplicationFactoryFixture,
            ITestOutputHelper testOutputHelper)
        {
            _client = webApplicationFactoryFixture.CreateClient();
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async Task ClaimsController_LoadJwtClaims_ReturnsJwtClaims()
        {
            // GivenDefaultSetup()
            await WhenLoadingJwtClaimsAsync();
            await ThenJwtClaimsAreReturned();
        }

        private async Task ThenJwtClaimsAreReturned()
        {
            _testOutputHelper.WriteLine(_response.StatusCode.ToString());
            _response.IsSuccessStatusCode.Should().BeTrue();
            var claims =
                JsonSerializer.Deserialize<Dictionary<string, string>>(await _response.Content.ReadAsStringAsync());
            _testOutputHelper.WriteLine("Claims: {0}, ClaimCommonName: {1}", string.Join(", ", claims.Values),
                string.Join(", ", claims.Keys));
            claims.Values.Should().NotBeEmpty().And.NotContain(claim => string.IsNullOrWhiteSpace(claim));
        }

        private async Task WhenLoadingJwtClaimsAsync()
        {
            _response = await _client.GetAsync($"{ControllerBasePath}/jwt");
        }
    }
}