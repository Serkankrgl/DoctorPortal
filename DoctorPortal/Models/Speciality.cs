using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorPortal.Models
{
    public class Speciality
    {
        public int SpecialityId { get; set; }
        public string SpecialityName { get; set; }
        public ICollection<Doctor> Doctors { get; set; }
        public ICollection<Post> Posts { get; set; }

    }
}
