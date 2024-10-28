using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CenterManagement.ViewModels
{
    public class LessonVM
    {
        public string Id { get; set; }

        public string LessonTitle { get; set; }

        public string Discreption { get; set; }

        public string AcademyYear { get; set; }

        public string Month { get; set; }

        public string VedioUrl { get; set; }

        [Display(Name = "Add a vedio")]
        [DataType(DataType.Upload)]
        [FileExtensions(Extensions = "mp4")]
        public IFormFile VedioFile { get; set; }
        public string VedioName { get; set; }

        [Display(Name = "Add an Image")]
        [DataType(DataType.Upload)]
        [FileExtensions(Extensions = "jpg,png,gif,jpeg,bmp,svg")]
        public IFormFile ImageFile { get; set; }
        public string ImageName { get; set; }
    }
}
