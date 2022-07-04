using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ST1.Models
{
    public class MatchModel
    {
        public int MatchID { get; set; }
        [Display(Name = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime MatchDate { get; set; }
        public string Home { get; set; }
        [Display(Name = "Home score")]
        public int HomeScore { get; set; }
        public string Visitor { get; set; }
        [Display(Name = "Visitor score")]
        public int VisitorScore { get; set; }
        [Display(Name = "Play stage")]
        public string PlayStage { get; set; }
        [Display(Name = "Penalty score")]
        public string PenaltyScore { get; set; }
        [Display(Name = "Home pen.")]
        public Nullable<int> HomeScoreP { get; set; }
        [Display(Name = "Visitor pen.")]
        public Nullable<int> VisitorScoreP { get; set; }

    }
}