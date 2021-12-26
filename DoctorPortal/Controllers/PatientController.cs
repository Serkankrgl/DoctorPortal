using DoctorPortal.Data;
using DoctorPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorPortal.Controllers
{

    [Authorize(Roles = "Patient")]
    public class PatientController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PatientController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(Patient patient)
        {
            patient.PatientId = _userManager.GetUserId(User);
            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Index()
        {
            Patient patient = _context.Patients.Where(x => x.PatientId == _userManager.GetUserId(User)).FirstOrDefault();
            return View(patient);
        }


        public IActionResult SharePost()
        {
            SetViewBag();
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddComment(Comment comment)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.GetUserAsync(User);
                comment.User = _context.AppUser.Where(x => x.Id == user.Id).FirstOrDefault();
                comment.Post = _context.Posts.Where(x => x.PostId == comment.PostId).FirstOrDefault();
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return View(nameof(Index));
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SharePost(Post post)
        {
            SetViewBag();
            Console.WriteLine("GIRDI");
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.GetUserAsync(User);
                post.PatientId = user.Id;
                post.Patient = _context.Patients.Where(x => x.PatientId == user.Id).FirstOrDefault();
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ShowPost));
            }
            else
            {
                return View(post);
            }
        }

        public IActionResult ShowPost()
        {
            SetViewBag();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ShowPostDetail(Comment comment)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Post = _context.Posts.Where(x => x.PostId == comment.PostId).FirstOrDefault();
                ViewBag.Comments = _context.Comments.Where(x => x.PostId == comment.PostId).ToList();
                return View();
            }
            else
            {
                return View(nameof(ShowPost));
            }
        }


        public void SetViewBag()
        {
            ViewBag.Posts = _context.Posts.ToList();
            ViewBag.UserName = _userManager.GetUserName(HttpContext.User);
            ViewBag.Specialities = _context.Specialities.ToList();
        }
        public async Task<IActionResult> CreateAppointment()

        #region Appointment
        public async Task<IActionResult> AppointmentPage()
        {
            List<Appointment> appointments = await _context.Appointments.Include(x => x.Doctor.User)
                                                                        .Include(x => x.Patient.User)
                                                                        .Where(x => x.PatientId == _userManager.GetUserAsync(User).GetAwaiter().GetResult().Id).ToListAsync();
            return View(appointments);
        }
        public IActionResult CreateAppointment()
        {

            //Doktor dropdownı için selectlist dolduruluyor.
            GetDocSelectList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAppointment(Appointment model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.GetUserAsync(User);
                model.PatientId = user.Id;
                model.Patient = _context.Patients.Where(x => x.PatientId == user.Id).FirstOrDefault();
                model.Status = "0";
                model.Doctor = _context.Doctors.Where(x => x.DoctorId == model.DoctorId).FirstOrDefault();
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(AppointmentPage));
            }
            GetDocSelectList();
            return View(model);
        }

        public async Task<IActionResult> DeleteAppointment(int id)
        {
            Appointment appointment = _context.Appointments.Where(x => x.AppointmentId == id).FirstOrDefault();

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(AppointmentPage));
        }

        void GetDocSelectList()
        {
            List<Doctor> doctors =  _context.Doctors.Include(a => a.Speciality).Include(a => a.User).ToList();
            IEnumerable<SelectListItem> items = from value in doctors
                                                select new SelectListItem
                                                {
                                                    Value = value.DoctorId.ToString(),
                                                    Text = value.GetFulName(),
                                                };
            ViewBag.Doctors = items;
        }
        #endregion
        #region Prescription
        public async Task<IActionResult> PrescriptionPage()
        {
            List<Prescription> prescriptions = await _context.Prescriptions.Include(x => x.Doctor.User)
                                                                        .Include(x => x.Patient.User)
                                                                        .Where(x => x.PatientId == _userManager.GetUserAsync(User).GetAwaiter().GetResult().Id).ToListAsync();
            return View(prescriptions);
        }
        #endregion

    }
}
