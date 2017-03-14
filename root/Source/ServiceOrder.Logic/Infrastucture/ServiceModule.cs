using Ninject.Modules;
using ServiceOrder.DataProvider.Interfaces;
using ServiceOrder.DataProvider.Repositories;

namespace ServiceOrder.Logic.Infrastucture
{
    public class ServiceModule : NinjectModule
    {
        private string connectionString;

        public ServiceModule(string connection)
        {
            connectionString = connection;
        }

        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument(connectionString);
        }
    }
}
