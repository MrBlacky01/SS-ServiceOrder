using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceOrder.DataProvider.Entities
{
    public class Order : Entity
    {

        public int ClientId { get; set; }
        public int RegionId { get; set; }
        public int ServiceTypeId { get; set; }
        public int ServiceProviderId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime BeginTime { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime EndTime { get; set; }

        public Client OrderClient { get; set; }
        public Region OrderRegion { get; set; }
        public ServiceType OrderType { get; set; }
        public ServiceProvider OrderProvider { get; set; }

    }
}
