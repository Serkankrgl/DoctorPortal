using DoctorPortal.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorPortal.Models
{
    public class Doctor
    {
        
        public string DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public ApplicationUser User { get; set; }
        public int SpecialityId { get; set; }
        [ForeignKey("SpecialityId")]
        public Speciality Speciality { get; set; }
        public string ClinicAdress { get; set; }
        public int ClinicPhone { get; set; }
        public DateTime GraduationDate { get; set; }
        public string UPIN { get; set; }
        public ICollection<Prescription> Prescriptions { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
