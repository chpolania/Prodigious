using System.Web.Http;
using Microsoft.Practices.Unity;
using Prod.Resolver;
using Unity.Mvc3;

namespace WebApi
{
    public static class Bootstrapper
    {
        public static void  Initialise()
        {
            var container = BuildUnityContainer();
            System.Web.Mvc.DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            ComponentLoader.LoadContainer(container, ".\\bin", "WebApi.dll");
            ComponentLoader.LoadContainer(container, ".\\bin", "Prod.Services.dll");
            ComponentLoader.LoadContainer(container, ".\\bin", "Prod.DAL.dll");
        }
    }
}