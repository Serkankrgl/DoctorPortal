using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorPortal.Models
{
    public class Prescription
    {

        public int PrescriptionId { get; set; }

        [ForeignKey("Patient")]

        [DisplayName("PatientDisplay")]
        [Required(ErrorMessage = "{0} is required!")]
        public string PatientId { get; set; }
        [ForeignKey("Doctor")]
        [DisplayName("DoctorDisplay")]
        [Required(ErrorMessage = "{0} is required!")]
        public string DoctorId { get; set; }

        [DisplayName("PrescriptionContentDisplay")]
        [Required(ErrorMessage = "{0} is required!")]
        public string PrescriptionContent { get; set; }
        [DisplayName("EffOutDateDisplay")]
        [Required(ErrorMessage = "{0} is required!")]
        public DateTime EffOutDate { get; set; }
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
    }
}
