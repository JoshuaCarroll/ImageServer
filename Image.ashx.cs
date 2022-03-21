using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ImageServer
{
    /// <summary>
    /// Summary description for Image
    /// </summary>
    public class Image : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string[] files = Directory.GetFiles(AppContext.BaseDirectory + "/images", "*.jpg");
            Random rand = new Random();
            string randomFilePath = files[rand.Next(files.Length)];

            System.Drawing.Image img = System.Drawing.Image.FromFile(randomFilePath);
            byte[] arrImg = ImageToByteArray(img);
            context.Response.ContentType = "image/jpg";
            context.Response.OutputStream.Write(arrImg, 0, arrImg.Length);
        }

        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}