using System;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;


namespace About
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            PopulateServiceCollection(builder.Services);

            var host = builder.Build();

            PopulateServiceProvider(host.Services);

            await host.RunAsync();
        }

        public static void PopulateServiceCollection(IServiceCollection services)
        {
            services
                .AddBaseAddressHttpClient()
                .AddBlazorise(options =>
                {
                    options.ChangeTextOnKeyPress = true;
                })
                .AddBootstrapProviders()
                .AddFontAwesomeIcons();

        }

        public static void PopulateServiceProvider(IServiceProvider provider)
        {
            provider
                .UseBootstrapProviders()
                .UseFontAwesomeIcons();
        }
    }
}
