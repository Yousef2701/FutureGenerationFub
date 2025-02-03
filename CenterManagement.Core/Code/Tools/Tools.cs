using Microsoft.AspNetCore.Http;

namespace ArabityAuth
{
    public class Tools
    {

        
        public Tools()
        {
           
        }
        public string AddImages(IFormFile imagefile, string username)
        {

            if (imagefile == null)
            { return "Defult User Image" + username; }
            string imageUrl = imagefile.FileName;
            string uploads = Path.Combine(Environment.CurrentDirectory, "Uploads");
            string path = Path.Combine(uploads, username.Substring(0, username.Length - 4) + imageUrl);

            if (System.IO.File.Exists(imageUrl))
            {
                string temporary = Path.Combine(Environment.CurrentDirectory, "ImagePackups");
                File.Copy(path, temporary);
                string newFilePath = Path.Combine(path, username.Substring(0, username.Length - 4) + imageUrl);
                File.Move(temporary, newFilePath);
            }
            else
            { imagefile.CopyTo(new FileStream(path, FileMode.Create)); }


            return username.Substring(0, username.Length - 4) + imageUrl;
        }

        public string AddTasks(IFormFile taskFile, string username)
        {

            if (taskFile == null)
            { return "Defult File" + username; }
            string imageUrl = taskFile.FileName;
            string files = Path.Combine(Environment.CurrentDirectory, "Files");
            string path = Path.Combine(files, username + imageUrl);

            if (System.IO.File.Exists(imageUrl))
            {
                string temporary = Path.Combine(Environment.CurrentDirectory, "ImagePackups");
                File.Copy(path, temporary);
                string newFilePath = Path.Combine(path, username + imageUrl);
                File.Move(temporary, newFilePath);
            }
            else
            { taskFile.CopyTo(new FileStream(path, FileMode.Create)); }


            return username + imageUrl;
        }

        public string AddVedios(IFormFile vediofile, string username)
        {

            if (vediofile == null)
            { return "Defult User Image" + username; }
            string vedioUrl = vediofile.FileName;
            string vedios = Path.Combine(Environment.CurrentDirectory, "Vedios");
            string path = Path.Combine(vedios, username.Substring(0, username.Length - 4) + vedioUrl);

            if (System.IO.File.Exists(vedioUrl))
            {
                string temporary = Path.Combine(Environment.CurrentDirectory, "ImagePackups");
                File.Copy(path, temporary);
                string newFilePath = Path.Combine(path, username.Substring(0, username.Length - 4) + vedioUrl);
                File.Move(temporary, newFilePath);
            }
            else
            { vediofile.CopyTo(new FileStream(path, FileMode.Create)); }


            return username.Substring(0, username.Length - 4) + vedioUrl;
        }

        public string UpdateImages(IFormFile imagefile,string imagename)
        {
            string imageUrl = imagefile.FileName;
            string path = Path.Combine(Environment.CurrentDirectory, "Uploads");
            if (System.IO.File.Exists(imagename))
            {
              System.IO.File.Delete(imagename);
              imagefile.CopyTo(new FileStream(path, FileMode.Create));
            }
            
            
            return imageUrl;
        }
    }
}
