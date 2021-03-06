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

    public partial class Match
    {
        public int MatchID { get; set; }
        public Nullable<System.DateTime> MatchDate { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public int Home { get; set; }
        [Range(0, 99, ErrorMessage = "Min. value is 0.")]
        public Nullable<int> HomeScore { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public int Visitor { get; set; }
        [Range(0, 99, ErrorMessage ="Min. value is 0.")]
        public Nullable<int> VisitorScore { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string PlayStage { get; set; }
        public string PenaltyScore { get; set; }
        [Range(0, 99, ErrorMessage = "Min. value is 0.")]
        public Nullable<int> HomeScoreP { get; set; }
        [Range(0, 99, ErrorMessage = "Min. value is 0.")]
        public Nullable<int> VisitorScoreP { get; set; }
    
        public virtual Country Country { get; set; }
    }
}
