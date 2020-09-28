using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

namespace IdentityServer4.Contrib.Administration.Tests.Fixture
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class WebApplicationFactoryFixture : WebApplicationFactory<Startup>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.UseEnvironment("CI");
            return base.CreateHost(builder);
        }
    }
}