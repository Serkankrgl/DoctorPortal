using DoctorPortal.Data;
using DoctorPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorPortal.Controllers.PatientControllers
{
    [Authorize(Roles = "Patient")]
    public class PatientController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatientController(ApplicationDbContext context)
        {
            _context = context;
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
        public async Task<IActionResult> SharePost(Post post)
        {
            SetViewBag();
            Console.WriteLine("GIRDI");
            if (ModelState.IsValid)
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(post);
            }
        }


        public void SetViewBag()
        {
            ViewBag.Posts = _context.Posts.ToList();
            List<Speciality> specialityNames = new List<Speciality>();

            ViewBag.Specialities = _context.Specialities.ToList();
        }
    }
}
