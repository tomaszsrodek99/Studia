using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ST1.Models
{
    public class CountryModel
    {
        public enum Group
        {
            A,B,C,D,E,F
        }
        public int CountryID { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Coach")]
        public int CoachID { get; set; }
        [StringLength(35, MinimumLength = 4, ErrorMessage = "The name must be 4 to 35 characters long.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage = "The field can only contain letters. The name must start with a capital letter.")]
        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Country Name")]
        public string CountryName { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Group")]
        public Group Grupa { get; set; }
    }
}