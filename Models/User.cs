using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ST1.Models
{
    public class User
    {
        public int Id { get; set; }
        [StringLength(25, MinimumLength = 4, ErrorMessage = "Login must be between 4 and 25 characters long.")]
        [Required(ErrorMessage = "This field is required.")]
        public string UserName { get; set; }
        [StringLength(25, MinimumLength = 8, ErrorMessage = "Password must be between 4 and 30 characters long.")]
        [Required(ErrorMessage = "This field is required.")]
        public string Password { get; set; }
        public string Email { get; set; }
        public int Status { get; set; }
    }
}