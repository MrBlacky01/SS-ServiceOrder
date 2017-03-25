using System;
using System.Collections.Generic;
using AutoMapper;
using ServiceOrder.DataProvider.Entities;
using ServiceOrder.DataProvider.Interfaces;
using ServiceOrder.ViewModel.ViewModels.Implementation;
using ServiceOrder.ViewModel.ViewModels.Implementation.RegionViewModels;

namespace ServiceOrder.Logic.Services.Implementations
{
    public class RegionService : IRegionService
    {
        private IUnitOfWork DataBase { get; set; }

        public RegionService(IUnitOfWork dataBase)
        {
            DataBase = dataBase;
        }

        public void Add(RegionEntityViewModel item)
        {
            Mapper.Initialize(config => config.CreateMap<RegionEntityViewModel,Region>());
            DataBase.Regions.Create(Mapper.Map<RegionEntityViewModel,Region>(item));
            DataBase.Save();
        }

        public void Delete(int? id)
        {
            if (id == null)
                throw new Exception("Не установлено id региона");
            DataBase.Regions.Delete((int)id);
            DataBase.Save();
        }

        public void Update(RegionEntityViewModel item)
        {
            Mapper.Initialize(config => config.CreateMap<RegionEntityViewModel, Region>());
            DataBase.Regions.Update(Mapper.Map<RegionEntityViewModel, Region>(item));
            DataBase.Save();
        }

        public RegionEntityViewModel Get(int? id)
        {

            if (id == null)
                throw new Exception("Не установлено id региона");
            var region = DataBase.Regions.Get((int) id);
            if (region == null)
            {
                throw new Exception("No region with this Id");
            }
            Mapper.Initialize(config => config.CreateMap<Region,RegionEntityViewModel>());
            return Mapper.Map<Region, RegionEntityViewModel>(region);
        }

        public IEnumerable<RegionEntityViewModel> GetAll()
        {
            Mapper.Initialize(config => config.CreateMap<Region, RegionEntityViewModel>());
            return Mapper.Map<IEnumerable<Region>, IEnumerable<RegionEntityViewModel>>(DataBase.Regions.GetAll());
        }

        public void Dispose()
        {
            DataBase.Dispose();
        }
    }
}
