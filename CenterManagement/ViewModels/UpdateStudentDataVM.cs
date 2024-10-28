using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CenterManagement.ViewModels
{
    public class UpdateStudentDataVM
    {
        public string Id { get; set; }

        public string FristName { get; set; }

        public string LastName { get; set; }

        public string AcademyYear { get; set; }

        public string PhoneNumber { get; set; }

        public string GuardingPhoneNumber { get; set; }

        public string Email { get; set; }

        public string ImageUrl { get; set; } = null!;

        public string Governorate { get; set; } = null!;

        public string City { get; set; } = null!;

        [Display(Name = "Add a picture")]
        [DataType(DataType.Upload)]
        [FileExtensions(Extensions = "jpg,png,gif,jpeg,bmp,svg")]
        public IFormFile ImageFile { get; set; }
        public string ImageName { get; set; }
    }
}
