using System.ComponentModel.DataAnnotations;

namespace ServiceOrder.DataProvider.Entities
{
    public class Client : Entity
    {
        [Required]
        public int UserId { get; set; }

        public User ClientUser { get; set; }
    }
}
