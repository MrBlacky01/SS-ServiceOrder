﻿using System.ComponentModel.DataAnnotations;

namespace ServiceOrder.ViewModel.ViewModels.Implementation.PhotoViewModels
{
    public class ShortPhotoViewModel
    {
        public int Id { get; set; }

        [StringLength(255, ErrorMessage = "File name must has at least 1 symbol and length less than 255", MinimumLength = 1)]
        [Required]
        public string FileName { get; set; }

        [StringLength(100)]
        public string ContentType { get; set; }
    }
}
