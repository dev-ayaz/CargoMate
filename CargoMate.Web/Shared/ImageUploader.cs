using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace CargoMateSolution.Shared
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

        public static string SaveImageFromBase64(string imgStr)
        {
            try
            {
                var random = new Random();
                var randomNumber = random.Next(0, 100);

                var imageName = string.Format("VehicleRegistrationImage-{0}-{1}.jpg", DateTime.Now.ToString("yyyyMMddTHHmmss"), randomNumber);

                var imgPath = Path.Combine(VehicleImagesUrl, imageName);

                byte[] imageBytes = Convert.FromBase64String(imgStr.Split(',')[1]);

                File.WriteAllBytes(imgPath, imageBytes);

                return imageName;
            }
            catch (Exception)
            {
                
                return string.Empty;
            }
        }
    }


}
