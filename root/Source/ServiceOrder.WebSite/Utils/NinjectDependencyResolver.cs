using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using ServiceOrder.Logic.Services;
using ServiceOrder.Logic.Services.Implementations;

namespace ServiceOrder.WebSite.Utils
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            kernel.Bind<ICategoryService>().To<CategoryService>();
            kernel.Bind<IServiceTypeService>().To<ServiceTypeService>();
            kernel.Bind<IRegionService>().To<RegionService>();
            kernel.Bind<IAccountService>().To<AccountService>();
            kernel.Bind<IManageService>().To<ManageService>();
            kernel.Bind<IServiceProviderService>().To<ServiceProviderService>();
            kernel.Bind<IOrderService>().To<OrderService>();
            kernel.Bind<IAlbumService>().To<AlbumService>();
            kernel.Bind<IPhotoService>().To<PhotoService>();

        }
    }
}