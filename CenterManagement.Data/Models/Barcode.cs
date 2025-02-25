﻿namespace CenterManagement.Models
{
    public class Barcode
    {
        public string barcode { get; set; }

        public string Month { get; set; }

        public string AcademyYear { get; set; }

        public string TeacherId { get; set; }

        public string? StudendId { get; set; }

        public virtual Teacher Teacher { get; set; } = null!;
    }
}
