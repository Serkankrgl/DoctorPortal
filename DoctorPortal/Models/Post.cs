using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorPortal.Models
{
    public class Post
    {
        public int PostId { get; set; }

        [ForeignKey("Patient")]
        public string PatientId { get; set; }

        [ForeignKey("Speciality")]
        public int? SpecialityId { get; set; }
        public string  PostContent { get; set; }
        public string IsAnswered { get; set; }
        public Patient Patient { get; set; }
        public Speciality Speciality { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
