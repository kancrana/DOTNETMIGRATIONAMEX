using Microsoft.Practices.Unity.Configuration;
using System;
using System.Configuration;
using Unity;

namespace eShop.Web
{
    public static class UnityConfig
    {
#region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });
        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;

#endregion
        public static void RegisterTypes(IUnityContainer container)
        {
            container.LoadConfiguration(ConfigurationManager.AppSettings["UnityContainer"]);
        }
    }
}