using Microsoft.Practices.Unity.Configuration;
using StackExchange.Redis;
using System.Configuration;
using Unity;
using Unity.WebApi;

namespace eShop.WebApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            // Provide configuration directly
            // container.RegisterType<IProductRepository, ProductRepository>();
            // container.RegisterType<ICartRepository, CartRepository>();
            // container.RegisterType<ProductDataContext>(new HierarchicalLifetimeManager());
            var redisConnectionString = ConfigurationManager.AppSettings["eShop_Cart"];
            var configuration = ConfigurationOptions.Parse(redisConnectionString, true);
            configuration.AbortOnConnectFail = false;
            configuration.ResolveDns = true;
            container.RegisterInstance(ConnectionMultiplexer.Connect(configuration));
            // Provide configuration through config file.
            container.LoadConfiguration(ConfigurationManager.AppSettings["UnityContainer"]);
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}