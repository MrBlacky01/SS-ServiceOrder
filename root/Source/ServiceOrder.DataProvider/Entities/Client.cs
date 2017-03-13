using System.ComponentModel.DataAnnotations;

namespace ServiceOrder.DataProvider.Entities
{
    public class Client : Entity
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public int UserId { get; set; }

        public User ClientUser { get; set; }
    }
}
