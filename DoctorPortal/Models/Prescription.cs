using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorPortal.Models
{
    public class Prescription
    {
        public int PrescriptionId { get; set; }

        [ForeignKey("Patient")]
        public string PatientId { get; set; }
        [ForeignKey("Doctor")]
        public string DoctorId { get; set; }

        public string PrescriptionContent { get; set; }
        public DateTime EffOutDate { get; set; }

        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
    }
}
