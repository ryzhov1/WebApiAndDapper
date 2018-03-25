using DataAccess.Connection;
using DataAccess.Repositories;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;

namespace WebApiTest
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IConnectionFactory, ConnectionFactory>(new HierarchicalLifetimeManager());
            container.RegisterType<IContragentRepository, ContragentRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IBankRepository, BankRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IAccountRepository, AccountRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IContragentAccountsRepository, ContragentAccountsRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ISavingRepository, SavingRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IDeletingRepository, DeletingRepository>(new HierarchicalLifetimeManager());

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}