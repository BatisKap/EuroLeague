using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyEuroleagueMVCAspNetCore.Models
{
    public class Teams
    {
        [Key]
        [Required]
        public int TeamID { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Country { get; set; }
        [MaxLength(50)]
        public string Coach { get; set; }
        [MaxLength(100)]
        [DisplayName("Image Name")]
        public string TeamLogoImageName { get; set; }
        [NotMapped]
        [DisplayName("Upload Team Logo")]
        public IFormFile ImageFileTeamLogo { get; set; }
        [MaxLength(100)]
        [DisplayName("Existing Photo Path")]
        [NotMapped]
        public string ExistingPhotoPath { get; set; }
    }
}
