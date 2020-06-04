using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaxsPetCare.Models
{
    public class Training
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        [Required]
        public int PackageID { get; set; }

        [Required]
        public string PetType { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string PetBreed { get; set; }

        [Required]
        public DateTime PickUpDate { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string PickUpAddress { get; set; }

        public DateTime DateTime { get; set; }

        public int Seen { get; set; }

        public TrainingPackages PackageDetails { get; set; }
    }

    public class TrainingPackages
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Type { get; set; }

        [Required]
        public int Status { get; set; }
    }
}