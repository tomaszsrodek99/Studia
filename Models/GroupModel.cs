using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ST1.Models
{
    public class GroupModel
    {
        public string Name { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int Points { get; set; }
        public int Matches { get; set; }
        public string Group { get; set; }
    }
}