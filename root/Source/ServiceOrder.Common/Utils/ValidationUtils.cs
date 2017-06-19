using System.Web;

namespace ServiceOrder.Common.Utils
{
    public static class ValidationUtils
    {
        public static bool CheckFileIsImage(HttpPostedFileBase file)
        {
            return file.ContentType.Contains("image");
        }
    }
}
