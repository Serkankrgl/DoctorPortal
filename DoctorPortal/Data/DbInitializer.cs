using DoctorPortal.Areas.Identity.Data;
using DoctorPortal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorPortal.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext db,
                             UserManager<ApplicationUser> usermanager,
                             RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = usermanager;
            _roleManager = roleManager;
        }
        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }

            }
            catch (Exception )
            {

            }

            if (_db.Roles.Any(m => m.Name == "Admin")) return;

            _roleManager.CreateAsync(new IdentityRole("Admin")).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole("Patient")).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole("Doctor")).GetAwaiter().GetResult();

            _userManager.CreateAsync(new ApplicationUser() {
                UserName = "admin@gmail.com",
                Name = "Admin",
               Surname = "Admin",
               TC="11111111111",
               Gender ="D",
               Email ="admin@gmail.com",
               EmailConfirmed=true,
               PhoneNumber="1112223344"               
            },"Admin123#").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(_db.Users.FirstOrDefaultAsync(u => u.Email == "admin@gmail.com").GetAwaiter().GetResult(), "Admin").GetAwaiter().GetResult();

            _userManager.CreateAsync(new ApplicationUser()
            {
                UserName = "pat@gmail.com",
                Name = "Pat",
                Surname = "Pat",
                TC = "11111111111",
                Gender = "D",
                Email = "pat@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "1112223344"
            }, "Pat123#").GetAwaiter().GetResult();

            _userManager.AddToRoleAsync(_db.Users.FirstOrDefaultAsync(u => u.Email == "pat@gmail.com").GetAwaiter().GetResult(), "Patient").GetAwaiter().GetResult();

            _userManager.CreateAsync(new ApplicationUser()
            {
                UserName = "doc@gmail.com",
                Name = "Doc",
                Surname = "Doc",
                TC = "11111111111",
                Gender = "D",
                Email = "doc@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "1112223344"
            }, "Doc123#").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(_db.Users.FirstOrDefaultAsync(u => u.Email == "doc@gmail.com").GetAwaiter().GetResult(), "Doctor").GetAwaiter().GetResult();

        }
    }
}
