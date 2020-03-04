using Microsoft.Extensions.Configuration;

using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.Azure;
using Shared.Interfaces.Infrastructure;
using Shared.Interfaces.Messages;
using Shared.Models.Azure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Shared.Logic
{
    public class DIHelper
    {
        private IConfiguration configurationManager;


        public IServiceProvider ReturnServiceProvider()
        {
            //var builder = new ConfigurationBuilder()
            //   .SetBasePath(Directory.GetCurrentDirectory())
            //   .AddJsonFile("appsettings.json");

            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            configurationManager = builder.Build();

            IServiceCollection services = new ServiceCollection();

            //services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
            services.AddSingleton<IConfiguration>(this.configurationManager);
            services.AddTransient<IMessage, AzureMessage>();
            services.AddTransient<IQueueRepo, AzureQueRepo>();
            services.AddTransient<ITableRepo, AzureTableRepo>();

            var serviceProvider = services.BuildServiceProvider();
            
            return serviceProvider;

        }
    }
}
