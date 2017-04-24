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
        private IMapper _mapper;

        public RegionService(IUnitOfWork dataBase,IMapper mapper)
        {
            DataBase = dataBase;
            _mapper = mapper;
        }

        public void Add(RegionEntityViewModel item)
        {
            DataBase.Regions.Create(_mapper.Map<RegionEntityViewModel,Region>(item));
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
            DataBase.Regions.Update(_mapper.Map<RegionEntityViewModel, Region>(item));
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
           
            return _mapper.Map<Region, RegionEntityViewModel>(region);
        }

        public IEnumerable<RegionEntityViewModel> GetAll()
        {
            return _mapper.Map<IEnumerable<Region>, IEnumerable<RegionEntityViewModel>>(DataBase.Regions.GetAll());
        }

        public void Dispose()
        {
            DataBase.Dispose();
        }
    }
}
