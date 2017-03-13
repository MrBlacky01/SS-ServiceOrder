using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceOrder.DataProvider.Entities
{
    public class Photo : Entity
    {
        [Required]
        [Column(TypeName = "image")]
        public byte[] PhotoImage { get; set; }
    }
}
