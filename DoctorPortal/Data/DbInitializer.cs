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
                Name = "Can",
               Surname = "Yuzkollar",
               TC="11111111111",
               Gender ="E",
               Email ="admin@gmail.com",
               EmailConfirmed=true,
               PhoneNumber="1112223344"               
            },"Admin123#").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(_db.Users.FirstOrDefaultAsync(u => u.Email == "admin@gmail.com").GetAwaiter().GetResult(), "Admin").GetAwaiter().GetResult();

            _userManager.CreateAsync(new ApplicationUser()
            {
                UserName = "b181210050@gmail.com",
                Name = "Serkan",
                Surname = "Kuruoglu",
                TC = "11111111111",
                Gender = "E",
                Email = "b181210050@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "1112223344"
            }, "Pat123#").GetAwaiter().GetResult();

            _userManager.AddToRoleAsync(_db.Users.FirstOrDefaultAsync(u => u.Email == "b181210050@gmail.com").GetAwaiter().GetResult(), "Patient").GetAwaiter().GetResult();

            _userManager.CreateAsync(new ApplicationUser()
            {
                UserName = "b191210551@gmail.com",
                Name = "Ruhid",
                Surname = "Ibadli",
                TC = "11111111111",
                Gender = "E",
                Email = "b191210551@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "1112223344"
            }, "Doc123#").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(_db.Users.FirstOrDefaultAsync(u => u.Email == "b191210551@gmail.com").GetAwaiter().GetResult(), "Doctor").GetAwaiter().GetResult();


            _db.Specialities.Add(new Speciality()
            {
                SpecialityName = "Anesthesiology",
            });
            _db.Specialities.Add(new Speciality()
            {
                SpecialityName = "Dermatology",
            });
            _db.Specialities.Add(new Speciality()
            {
                SpecialityName = "Pediatrics",
            });
            _db.Specialities.Add(new Speciality()
            {
                SpecialityName = "Psychiatry",
            });
            _db.Specialities.Add(new Speciality()
            {
                SpecialityName = "Surgery",
            });
            _db.Specialities.Add(new Speciality()
            {
                SpecialityName = "Urology",
            });
            _db.Specialities.Add(new Speciality()
            {
                SpecialityName = "Neurology",
            });
            _db.SaveChanges();
            ApplicationUser patient = _db.Users.FirstOrDefaultAsync(u => u.Email == "b181210050@gmail.com").GetAwaiter().GetResult();
            ApplicationUser doctor = _db.Users.FirstOrDefaultAsync(u => u.Email == "b191210551@gmail.com").GetAwaiter().GetResult();
            Speciality speciality = _db.Specialities.FirstOrDefaultAsync(u => u.SpecialityName == "Neurology").GetAwaiter().GetResult();
            _db.Patients.Add(new Patient()
            {
                PatientId = patient.Id,
                Age = 21,
                Length = 175,
                Weight = 95,
            });

            _db.Doctors.Add(new Doctor()
            {
                DoctorId = doctor.Id,
                SpecialityId = speciality.SpecialityId,
                GraduationDate = DateTime.Today,
            });
            _db.SaveChanges();

            _db.Prescriptions.Add(new Prescription()
            {
                PatientId = patient.Id,
                DoctorId = doctor.Id,
                PrescriptionContent = "Test Prescription",
                EffOutDate = DateTime.Today,
            });


            _db.Appointments.Add(new Appointment()
            {
                PatientId = patient.Id,
                DoctorId = doctor.Id,
                AppointmentDate = DateTime.Today,
                Status = "Pending",
                Description = "Testing System",
                DenyReason = "None",
            });

            _db.SaveChanges();
          
        
        
        }
    }
}
