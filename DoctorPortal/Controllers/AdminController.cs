using DoctorPortal.Data;
using DoctorPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorPortal.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateSpeciality()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateSpeciality(Speciality model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> DeleteComment(string id)
        {
            string[] list = id.Split(";");
            int commnetid = Convert.ToInt32(list[0]);
            int postid = Convert.ToInt32(list[1]);
            Comment comment = _context.Comments.Where(x => x.CommentId == commnetid).FirstOrDefault();
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction("ShowPostDetail","Home", new { id = postid });
        }

        public async Task<IActionResult> AdminPage()
        {
            List<Speciality> specialities = _context.Specialities.ToList();
            return View(specialities);
        }
    }
}
