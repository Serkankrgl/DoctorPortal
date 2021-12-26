using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorPortal.Models
{
    public class Patient
    {
        
        public string PatientId { get; set; }
        [ForeignKey("PatientId")]
        public ApplicationUser User { get; set; }

        [DisplayName("AgeDisplay")]
        public int Age { get; set; }

        [DisplayName("LengthDisplay")]
        public int Length { get; set; }
        //TODO: İnt yapılack
        [DisplayName("WeightDisplay")]
        public decimal Weight { get; set; }
        public ICollection<Prescription> Prescriptions { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
