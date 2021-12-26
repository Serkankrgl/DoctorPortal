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
        public IActionResult Index()
        {
            return View();
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
        {

            //Doktor dropdownı için selectlist dolduruluyor.
            List<Doctor> doctors = await _context.Doctors.Include(a => a.Speciality).Include(a => a.User).ToListAsync();
            IEnumerable<SelectListItem> items = from value in doctors
            select new SelectListItem
            {
                Value = value.DoctorId.ToString(),
                Text = value.GetFulName(),
            };
            ViewBag.Doctors = items;
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
                return RedirectToAction(nameof(Index));
            }
            return View();
        }


    }
}
