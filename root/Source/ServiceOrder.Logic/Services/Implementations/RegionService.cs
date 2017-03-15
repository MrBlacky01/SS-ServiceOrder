using System;
using System.Collections.Generic;
using AutoMapper;
using ServiceOrder.DataProvider.Entities;
using ServiceOrder.DataProvider.Interfaces;
using ServiceOrder.ViewModel.ViewModels.Implementation;

namespace ServiceOrder.Logic.Services.Implementations
{
    public class RegionService : IRegionService
    {
        private IUnitOfWork DataBase { get; set; }

        public RegionService(IUnitOfWork dataBase)
        {
            DataBase = dataBase;
        }

        public void Add(RegionViewModel item)
        {
            Mapper.Initialize(config => config.CreateMap<RegionViewModel,Region>());
            DataBase.Regions.Create(Mapper.Map<RegionViewModel,Region>(item));
            DataBase.Save();
        }

        public void Delete(int? id)
        {
            if (id == null)
                throw new Exception("Не установлено id региона");
            DataBase.Regions.Delete((int)id);
            DataBase.Save();
        }

        public void Update(RegionViewModel item)
        {
            Mapper.Initialize(config => config.CreateMap<RegionViewModel, Region>());
            DataBase.Regions.Update(Mapper.Map<RegionViewModel, Region>(item));
            DataBase.Save();
        }

        public RegionViewModel Get(int? id)
        {

            if (id == null)
                throw new Exception("Не установлено id региона");
            var region = DataBase.Regions.Get((int) id);
            if (region == null)
            {
                throw new Exception("No region with this Id");
            }
            Mapper.Initialize(config => config.CreateMap<Region,RegionViewModel>());
            return Mapper.Map<Region, RegionViewModel>(region);
        }

        public IEnumerable<RegionViewModel> GetAll()
        {
            Mapper.Initialize(config => config.CreateMap<Region, RegionViewModel>());
            return Mapper.Map<IEnumerable<Region>, IEnumerable<RegionViewModel>>(DataBase.Regions.GetAll());
        }

        public void Dispose()
        {
            DataBase.Dispose();
        }
    }
}
