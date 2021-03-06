using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using DoctorPortal.Data;
using DoctorPortal.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DoctorPortal.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _context;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "İsim")]
            public string Name { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Soyisim")]
            public string Surname { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "TC")]
            [MaxLength(11)]
            [MinLength(11)]
            public string TC { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Cinsiyet")]
            public string Gender { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Doktor musunuz?")]
            public string IsDoctor { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Şifre")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Şifre Doğrulama")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "Branş")]
            public int Speciality { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            ViewData["Speciality"] = await _context.Specialities.ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                if (Input.Speciality == 0)
                {
                    ModelState.AddModelError(string.Empty, "Doktorların Branş Seçmesi Zorunludur.");
                }
                else
                {

                    var user = new ApplicationUser
                    {
                        UserName = Input.Email,
                        Email = Input.Email,
                        Name = Input.Name,
                        Surname = Input.Surname,
                        Gender = Input.Gender,
                        TC = Input.TC,
                        IsDoctor = Input.IsDoctor
                    };
                    var result = await _userManager.CreateAsync(user, Input.Password);


                    if (result.Succeeded)
                    {
                        if (Input.IsDoctor == "1")
                        {

                            var doctor = new Models.Doctor { User = user,SpecialityId =Input.Speciality };
                            _context.Add(doctor);
                            await _context.SaveChangesAsync();
                        }
                        if (Input.IsDoctor == "0")
                        {
                            var patient = new Models.Patient { User = user };
                            _context.Add(patient);
                        }
                        _logger.LogInformation("User created a new account with password.");

                        await _context.SaveChangesAsync();
                        //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        //var callbackUrl = Url.Page(
                        //    "/Account/ConfirmEmail",
                        //    pageHandler: null,
                        //    values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        //    protocol: Request.Scheme);

                        //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
                        if (user.IsDoctor == "1")
                        {
                            await _userManager.AddToRoleAsync(user, "Patient");
                        }
                        else
                        {
                            await _userManager.AddToRoleAsync(user, "Doctor");
                        }

                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);

                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
