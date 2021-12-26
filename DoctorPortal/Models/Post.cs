using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorPortal.Models
{
    public class Post
    {
        public int PostId { get; set; }

        [ForeignKey("Patient")]
        [DisplayName("PatientDisplay")]
        public string PatientId { get; set; }

        [ForeignKey("Speciality")]

        [DisplayName("SpecialityDisplay")]
        [Required(ErrorMessage = "{0} is required!")]
        public int? SpecialityId { get; set; }

        [DisplayName("PostContentDisplay")]
        [Required(ErrorMessage = "{0} is required!")]
        public string  PostContent { get; set; }
        public string IsAnswered { get; set; }
        public Patient Patient { get; set; }
        public Speciality Speciality { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
