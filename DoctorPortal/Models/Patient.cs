using DoctorPortal.Areas.Identity.Data;
using System;
using System.Collections.Generic;
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
        public int Age { get; set; }
        public int Length { get; set; }
        public decimal Weight { get; set; }
        public ICollection<Prescription> Prescriptions { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<Post> Posts { get; set; }


    }
}
