using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace ServiceOrder.ViewModel.ViewModels.Implementation.ServiceProvidersViewModels
{
    public class ServiceProviderPaginationFiltrationList
    {
        public IPagedList<ServiceProviderViewModel> ProviderList { get; set; }
        public int? Page { get; set; }
        public int? ServiceId { get; set; }
        public int? RegionId { get; set; }
        public int? CategoryId { get; set; }
    }
}
