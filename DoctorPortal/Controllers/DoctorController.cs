using DoctorPortal.Data;
using DoctorPortal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorPortal.Controllers
{
    public class DoctorController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationUser _user;
        public DoctorController( ApplicationDbContext context,
                                  UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            

        }
        public IActionResult Index()
        {
         
            return View();
        }

    #region Appointment
        public async Task<IActionResult> AppointmentPage()
        {
            List<Appointment> appointments = await _context.Appointments.Include(x => x.Patient.User).ToListAsync();
            return View(appointments);
        }

        public async Task<IActionResult> ApproveAppointment(int id)
        {
            if (ModelState.IsValid)
            {
                Appointment appointment = await _context.Appointments.Where(x => x.AppointmentId == id).FirstOrDefaultAsync();
                appointment.Status = "1";
                _context.Update(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(AppointmentPage));
            }
            return RedirectToAction(nameof(AppointmentPage));
        }

        public async Task<IActionResult> RejectAppointment(int id)
        {
            if (ModelState.IsValid)
            {
                Appointment appointment = await _context.Appointments.Where(x => x.AppointmentId == id).FirstOrDefaultAsync();
                appointment.Status = "2";
                _context.Update(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(AppointmentPage));
            }
            return RedirectToAction(nameof(AppointmentPage));
        }
        #endregion

        #region Prescription
        public async Task<IActionResult> PrescriptionPage()
        {
            List<Prescription> prescriptions = await _context.Prescriptions.Include(x => x.Patient.User).ToListAsync();

            
            return View(prescriptions);
        }
        #region Create
        public async Task<IActionResult> CreatePrescription()
        {
            List<Patient> patients = await _context.Patients.Include(x => x.User).ToListAsync();
            IEnumerable<SelectListItem> items = from value in patients
                                                select new SelectListItem
                                                {
                                                    Value = value.PatientId.ToString(),
                                                    Text = value.User.GetFulName(),
                                                };

            ViewBag.Patients = items;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePrescription(Prescription prescription)
        {
            if (ModelState.IsValid)
            {
                //prescription.Doctor = _context.Doctors.Where(x => x.DoctorId == _user.Id).FirstOrDefault();
                prescription.DoctorId =  _userManager.GetUserAsync(User).GetAwaiter().GetResult().Id;
                _context.Add(prescription);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(PrescriptionPage));
            }
            return View(prescription);
        }
        #endregion

        #region Edit
        public IActionResult EditPrescription(int id)
        {
            Prescription prescription = _context.Prescriptions.Where(x => x.PrescriptionId == id).Include(x=>x.Patient.User).FirstOrDefault();

            return View(prescription);
        }

        [HttpPost]
        public async Task<IActionResult> EditPrescription(Prescription prescription)
        {
            if (ModelState.IsValid)
            {
                _context.Update(prescription);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(PrescriptionPage));
            }
            return View(prescription);
        }
        #endregion
        #region Delete

        public async Task<IActionResult> DeletePrescription(int id)
        {
            Prescription prescription = _context.Prescriptions.Where(x => x.PrescriptionId == id).Include(x => x.Patient.User).FirstOrDefault();

            _context.Prescriptions.Remove(prescription);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(PrescriptionPage));
        }

        #endregion
        #endregion
    }
}
