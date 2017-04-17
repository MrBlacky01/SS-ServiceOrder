using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceOrder.ViewModel.ViewModels.Implementation.ServiceTypeViewModels;

namespace ServiceOrder.ViewModel.ViewModels.Implementation.ServiceProvidersViewModels
{
    public class ServiceProviderViewModel
    {
        
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Сервисы")]
        public string ServicesString {
            get
            {
                string temp = "";
                foreach (var element in Services)
                {
                    temp += element.Title + ";";
                }
                return temp;
            } }

        public List<Service> Services { get; set; }


        
    }

    public class Service
    {
        public string Title { get; set; }
        public int ServiceCategoryId { get; set; }
    }
}
