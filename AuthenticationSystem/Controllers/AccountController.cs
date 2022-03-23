using AuthenticationSystem.Models;
using AuthenticationSystem.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Encodings.Web;

namespace AuthenticationSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAsync(RegisterVM registerVM, string? returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }
            var newUser = new ApplicationUser { FullName = registerVM.FullName, UserName = registerVM.Email, Email = registerVM.Email, TermsAgreement = registerVM.TermsAgreement };
            var isUserCreated = await _userManager.CreateAsync(newUser, registerVM.Password);
            if (isUserCreated.Succeeded)
            {
                //var code = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                //var callbackUrl = Url.Page("/Account/ConfirmEmail",pageHandler: null, values: new { area = "Identity", userId = newUser.Id, code = code },protocol: Request.Scheme);
                //await _emailSender.SendEmailAsync(newUser.Email, "Confirm your email",
                //$"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(value: callbackUrl ?? "~/")}'>clicking here</a>.");
                return LocalRedirect(returnUrl);
            }
            else
            {
                foreach (var error in isUserCreated.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View();
            }
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsync(LoginVM loginVM, string? returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }
            var isSignIn = await _signInManager.PasswordSignInAsync(loginVM.Email, loginVM.Password, loginVM.RememberMe, false);
            if (isSignIn.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            if (isSignIn.IsLockedOut)
            {
                return LocalRedirect(nameof(Lockout));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Please use valid credentials");
                return View();
            }
        }
        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ResetPasswordAsync(ResetPasswordVM resetPasswordVM)
        {
            if (!ModelState.IsValid)
            {
                return View(resetPasswordVM);
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogoutAsync(string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _signInManager.SignOutAsync();
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public IActionResult Lockout()
        {
            return View();
        }
    }
}
