using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("Name")]
        public string Name { get; set; }
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        [DisplayName ( "Surname")]
        public string Surname { get; set; }
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        [DisplayName ( "GenderDisplay")]
        public string Gender { get; set; }
        [PersonalData]
        [Column(TypeName = "nvarchar(5)")]
        [DisplayName( "IsDoctor")]
        public string IsDoctor { get; set; }
        [PersonalData]
        [Column(TypeName = "nvarchar(11)")]

        [DisplayName ( "TC")]
        public string TC { get; set; }

        public string GetFulName()
        {
            return Name.ToString() + " " + Surname.ToString();
        }
    }
}
