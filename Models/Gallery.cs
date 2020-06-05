using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaxsPetCare.Models
{
    public class Gallery
    {
        public int ID { get; set; }

        public string ImgURL { get; set; }

        [Required]
        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.jpeg|.pdf|.docx|.txt)$", ErrorMessage = "Only Document or Image files allowed.")]
        public HttpPostedFileBase Image { get; set; }
    }
}