using System;
using System.ComponentModel.DataAnnotations;

namespace ServiceOrder.ViewModel.ViewModels.Implementation.PhotoViewModels
{
    public class PhotoViewModel
    {
        private string _photoImage; 
        public int Id { get; set; }

        [StringLength(255,ErrorMessage = "File name must has at least 1 symbol and length less than 255",MinimumLength = 1)]
        [Required]
        public string FileName { get; set; }

        [StringLength(100)]
        public string ContentType { get; set; }

        [Required]
        public string PhotoImage {
            get
            {
                if (ImageBytes == null || ImageBytes.Length == 0)
                {
                    return _photoImage;
                }
                return Convert.ToBase64String(ImageBytes);
            }
            set { _photoImage = value; } }

        public byte[] ImageBytes { get; set; }
    }
}
