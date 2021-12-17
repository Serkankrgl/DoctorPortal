using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorPortal.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }

        [ForeignKey("Patient")]
        public string PatientId { get; set; }

        [ForeignKey("Doctor")]
        public string DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string DenyReason { get; set; }
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
    }
}
