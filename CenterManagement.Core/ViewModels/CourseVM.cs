using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CenterManagement.ViewModels
{
    public class CourseVM
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string AcademyYear { get; set; }

        public string SupnallUrl { get; set; }

        public string price { get; set; }


        [Display(Name = "Add a picture")]
        [DataType(DataType.Upload)]
        [FileExtensions(Extensions = "jpg,png,gif,jpeg,bmp,svg")]
        public IFormFile ImageFile { get; set; }
        public string ImageName { get; set; }
    }
}
