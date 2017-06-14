namespace ServiceOrder.ViewModel.ViewModels.Implementation.PhotoViewModels
{
    public class ViewDataUploadFilesResult : AbstractDataUploadResult
    {
       
        public string type { get; set; }
        public string url { get; set; }
        public string deleteUrl { get; set; }
        public string thumbnailUrl { get; set; }
        public string deleteType { get; set; }
    }
}
