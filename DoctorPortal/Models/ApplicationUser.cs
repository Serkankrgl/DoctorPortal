using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorPortal.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        [Column(TypeName ="nvarchar(100)")]
        [Display(Name = "İsim")]
        public string Name { get; set; }
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        [Display(Name = "Soyisim")]
        public string Surname { get; set; }
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        [Display(Name = "Cinsiyet")]
        public string Gender { get; set; }
        [PersonalData]
        [Column(TypeName = "nvarchar(5)")]
        [Display(Name = "Doktor mu?")]
        public string IsDoctor { get; set; }
        [PersonalData]
        [Column(TypeName = "nvarchar(11)")]

        [Display(Name = "Doktor mu?")]
        public string TC { get; set; }
    }
}
