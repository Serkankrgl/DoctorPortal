using DoctorPortal.Data;
using DoctorPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DoctorPortal.Controllers
{   
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
            _logger = logger;
        }
        private void SetSession() {
            var user = _userManager.GetUserAsync(User).GetAwaiter().GetResult();
            HttpContext.Session.SetString("Userid",user.Id);
        }

        public IActionResult Index()
        {
            SetSession();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult ShowPost()
        {
            SetSession();
            SetViewBag();
            IEnumerable<Post> posts =  _context.Posts.Include(x => x.Patient).ThenInclude(x => x.User).Include(x => x.Comments).ThenInclude(x => x.User).Include(x => x.Speciality).ToList();
            return View(posts);
        }
        public IActionResult ShowPostDetail(int  id)
        {

                Post post = _context.Posts.Include(x => x.Patient).ThenInclude(x=>x.User).Include(x => x.Comments).ThenInclude(x=> x.User).Include(x => x.Speciality).Where(x=> x.PostId == id).FirstOrDefault();
                return View(post);

        }


        public void SetViewBag()
        {
            ViewBag.Posts = _context.Posts.ToList();
            ViewBag.UserName = _userManager.GetUserName(HttpContext.User);
            ViewBag.Specialities = _context.Specialities.ToList();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> AddComment(int id)
        {
            Comment comment = new Comment() { PostId = id };
                return View(comment);
        }
        [HttpPost]
        public async Task<IActionResult> AddComment(Comment comment)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.GetUserAsync(User);
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction("ShowPost", "Home");
            }
            return View(comment);
        }

    }
}
