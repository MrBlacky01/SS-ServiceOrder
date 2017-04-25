using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceOrder.DataProvider.Entities
{
    public class Order : Entity
    {
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
