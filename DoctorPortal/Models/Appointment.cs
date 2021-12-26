using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorPortal.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }

        [ForeignKey("Patient")]
        [DisplayName("PatientDisplay")]
        [Required(ErrorMessage = "{0} is required!")]
        public string PatientId { get; set; }

        [ForeignKey("Doctor")]
        [DisplayName("DoctorDisplay")]
        [Required(ErrorMessage = "{0} is required!")]
        public string DoctorId { get; set; }
        [DisplayName("AppointmentDateDisplay")]
        [Required(ErrorMessage = "{0} is required!")]
        [DataType(DataType.DateTime, ErrorMessage = "InvalidDate")]
        public DateTime AppointmentDate { get; set; }
        [DisplayName("StatusDisplay")]
        public string Status { get; set; }
        [DisplayName("DescriptionDisplay")]
        [Required(ErrorMessage = "{0} is required!")]
        public string Description { get; set; }
        public string DenyReason { get; set; }
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
    }
}
