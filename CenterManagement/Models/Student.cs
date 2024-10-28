using MessagePack;
using System.ComponentModel.DataAnnotations;

namespace CenterManagement.Models
{
    public class Student
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

        public virtual ICollection<Result> Results { get; } = new List<Result>();

        public virtual ICollection<Students_Task> Students_Tasks { get; } = new List<Students_Task>();
    }
}
