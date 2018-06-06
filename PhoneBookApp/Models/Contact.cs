using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PhoneBookApp.Models
{
    public class Contact
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Contact Name")]
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name ="Phone Number")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone Number must be numeric")]
        public string PhoneNumber { get; set; }

        [ForeignKey("PhoneBook")]
        [Required]
        public int PhoneBookID { get; set; }
        public virtual PhoneBook PhoneBook { get; set; }

    }
}