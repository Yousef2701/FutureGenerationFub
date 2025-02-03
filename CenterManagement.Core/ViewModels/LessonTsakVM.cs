using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CenterManagement.ViewModels
{
    public class LessonTsakVM
    {
        public string LessonId { get; set; }


        [Display(Name = "Add a File")]
        [DataType(DataType.Upload)]
        [FileExtensions(Extensions = "pdf")]
        public IFormFile taskFile { get; set; }
        public string taskName { get; set; }
    }
}
