using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceOrder.DataProvider.Entities
{
    public class Client 
    {
        [Key]
        [ForeignKey("ClientUser")]
        public string UserId { get; set; }

        public User ClientUser { get; set; }
    }
}
