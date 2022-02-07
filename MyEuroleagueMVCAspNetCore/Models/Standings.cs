using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyEuroleagueMVCAspNetCore.Models
{
    public class Standings
    {
        [DisplayName("Position #")]
        public int PositionNo { get; set; }
        [DisplayName("Matches #")]
        public int MatchesNo { get; set; }
        [DisplayName("Image Name")]
        [NotMapped]
        public string TeamLogoImageName { get; set; }
        [DisplayName("Team")]
        public string TeamName { get; set; }
        public int Wins { get; set; }
        public int Loses { get; set; }
        [DisplayName("Points +")]
        public int PointsPlus { get; set; }
        [DisplayName("Points -")]
        public int PointsMinus { get; set; }
        [DisplayName("Points Difference")]
        public int PointsDif { get; set; }
        [DisplayName("Extra Time Matches #")]
        public int ExtraTimeMatches { get; set; }
    }
}
