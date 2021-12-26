using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorPortal.Models
{
    public class Speciality
    {
        public int SpecialityId { get; set; }
        [DisplayName("SpecialityDisplay")]
        [Required(ErrorMessage = "{0} is required!")]
        public string SpecialityName { get; set; }
        public ICollection<Doctor> Doctors { get; set; }
        public ICollection<Post> Posts { get; set; }

    }
}
