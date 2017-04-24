using System;
using System.Linq;
using AutoMapper;
using Ninject;

namespace ServiceOrder.Logic.Automapper
{
    public  class AutoMapperBind
    {
        public void Register(IKernel container)
        {
            //Automapper configuration
            var profiles = GetType().Assembly.GetTypes().Where(t => typeof(Profile).IsAssignableFrom(t)).Select(t => (Profile)Activator.CreateInstance(t));

            var config = new MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }
                cfg.ForAllMaps(
                    (map, expresion) =>
                        expresion.ForAllMembers(
                            options => options.Condition((source, destination, member) => member != null)));
            });
            var mapper = config.CreateMapper();
            container.Bind<MapperConfiguration>().ToConstant(config).InSingletonScope();
            container.Bind<IMapper>().ToConstant(mapper).InSingletonScope();
        }
    }
}
