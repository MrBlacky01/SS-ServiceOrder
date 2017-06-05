using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServiceOrder.DataProvider.Entities
{
    public class Album: Entity
    {
        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        public List<Photo> AlbumPhotos { get; set; } 
        public ServiceProvider Provider { get; set; }
    }
}
