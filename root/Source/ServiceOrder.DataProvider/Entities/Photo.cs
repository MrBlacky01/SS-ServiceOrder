﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceOrder.DataProvider.Entities
{
    public class Photo : Entity
    {

        [StringLength(255)]
        public string FileName { get; set; }

        [StringLength(100)]
        public string ContentType { get; set; }

        [Required]
        [Column(TypeName = "image")]
        public byte[] PhotoImage { get; set; }

        public Album PhotoAlbum { get; set; }
    }
}
