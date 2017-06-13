using System.IO;
using System.Web;

namespace CargoMate.WebAPI.Shared
{
    public static class ImageUploader
    {
        public static string VehicleImagesUrl = string.Format("{0}{1}", HttpContext.Current.Server.MapPath(@"\"), "SystemImages/VehicleImages");
        public static string SingleFileUploader(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return string.Empty;
            }

            if (file.ContentLength <= 0)
            {
               return string.Empty;  
            }
            //try
            //{
                var isExists = Directory.Exists(VehicleImagesUrl);

            if (!isExists)
                Directory.CreateDirectory(VehicleImagesUrl);

            var imageUrl = string.Format("{0}\\{1}", VehicleImagesUrl,file.FileName);
            file.SaveAs(imageUrl);

            return SessionKeys.VehicleImagePath;
            //}
            //catch (Exception ex)
            //{
            //    return string.Empty;
            //}
            
        }
    }
}
