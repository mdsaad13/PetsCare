using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaxsPetCare.Models
{
    public class ContactUs
    {
        public int ID { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Enter a valid name")]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Enter valid email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Mobile Number")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Enter a valid mobile number")]
        public string Mobile { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Enter a valid subject")]
        public string Subject { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Enter a valid message")]
        public string Message { get; set; }

        public DateTime DateTime { get; set; }

        public int Seen { get; set; }
    }
}