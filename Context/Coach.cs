//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ST1.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Coach
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Coach()
        {
            this.Country = new HashSet<Country>();
        }
    
        public int CoachID { get; set; }
        [StringLength(35, MinimumLength = 4, ErrorMessage = "The name must be 4 to 50 characters long.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage = "The field can only contain letters. The name must start with a capital letter.")]
        [Required(ErrorMessage = "This field is required.")]
        public string Firstname { get; set; }
        [StringLength(35, MinimumLength = 4, ErrorMessage = "The name must be 4 to 50 characters long.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage = "The field can only contain letters. The name must start with a capital letter.")]
        [Required(ErrorMessage = "This field is required.")]
        public string Lastname { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Country> Country { get; set; }
    }
}
