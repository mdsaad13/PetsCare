using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaxsPetCare.Models
{
    public class Boarding
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        [Required]
        public DateTime PickUpDate { get; set; }
        
        [Required]
        public DateTime DropDate { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string PickUpAddress { get; set; }

        public float Amount { get; set; }

        [Required]
        public string PetType { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string PetBreed { get; set; }

        public DateTime DateTime { get; set; }

        public int Seen { get; set; }

        public string UserName { get; set; }

    }
}