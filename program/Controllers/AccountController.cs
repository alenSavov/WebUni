using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using program.Controllers;
using SchoolDiary.Models;
using SchoolDiary.Services.Accounts.Models;
using SchoolDiary.Services.Contracts;
using SchoolDiary.Services.Utilities;
using SchoolDiary.Web.Models.Account;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SchoolDiary.Web.Controllers
{
    public class AccountController : Controller
    {
        private const string UserProfilePath = "profile?username=";

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAccountService _accountService;

        public AccountController(
           UserManager<User> userManager,
           SignInManager<User> signInManager,
           ILogger<AccountController> logger,
           RoleManager<IdentityRole> roleManager,
           IAccountService accountService)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._logger = logger;
            this._roleManager = roleManager;
            this._accountService = accountService;
        }

        [TempData]
        public string ErrorMessage { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return Redirect("\\");
            }

            return this.View();
        }

        public IActionResult Register()
        {
            return this.View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Login(LoginInputViewModel model, string returnUrl = null)
        //{
        //    ViewData["ReturnUrl"] = returnUrl;
        //    if (ModelState.IsValid)
        //    {
        //        if (model.Username == null || model.Password == null)
        //        {
        //            return this.View(model);
        //        }

        //        var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);

        //        if (result.Succeeded)
        //        {
        //            if (returnUrl == null)
        //            {

        //                return RedirectToAction(nameof(HomeController.Index), "Home");
        //            }

        //            return RedirectToAction(returnUrl);
        //        }
        //        if (!result.Succeeded)
        //        {

        //            TempData[GlobalConstants.TempDataErrorMessageKey] = GlobalConstants.InvalidLogin; 
        //            return View(model);
        //        }


        //        if (result.IsLockedOut)
        //        {
        //            RedirectToAction(nameof(Lockout));
        //        }
        //        else
        //        {
        //            ModelState.AddModelError(string.Empty, GlobalConstants.InvalidLogin);
        //            return View(model);
        //        }
        //    }

        //    return View(model);
        //}

        [HttpPost]
        public async Task<IActionResult> Login(LoginInputViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                if (model.Username == null || model.Password == null)
                {
                    return this.View(model);
                }

                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    if (returnUrl == null)
                    {

                        return RedirectToAction(nameof(HomeController.Index), "Home");
                    }

                    return RedirectToAction(returnUrl);
                }
                if (!result.Succeeded)
                {

                    TempData[GlobalConstants.TempDataErrorMessageKey] = GlobalConstants.InvalidLogin;
                    return View(model);
                }


                if (result.IsLockedOut)
                {
                    RedirectToAction(nameof(Lockout));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, GlobalConstants.InvalidLogin);
                    return View(model);
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterInputViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                if (model.Username == null ||
                    model.Password == null ||
                    model.Email == null ||
                    model.FirstName == null ||
                    model.LastName == null ||
                    model.ConfirmPassword == null)
                {
                    return this.View(model);
                }

                var userCreateResult = this._accountService.Create(model.Username, model.Password, model.ConfirmPassword, model.Email, model.FirstName, model.LastName);
                if (await userCreateResult)
                {
                    if (returnUrl == null)
                    {
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                    }

                    return RedirectToAction(returnUrl);
                }
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();

        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                ErrorMessage = $"Error from external provider: {remoteError}";
                return RedirectToAction(actionName: "Login", controllerName: "Users");
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(actionName: "Login", controllerName: "Users");
            }

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                return RedirectToAction(actionName: "Index", controllerName: "Home");
            }
            if (result.IsLockedOut)
            {
                return RedirectToAction(actionName: "Index", controllerName: "Home");
            }
            else
            {
                // If the user does not have an account, then ask the user to create an account.
                ViewData["ReturnUrl"] = returnUrl;
                ViewData["LoginProvider"] = info.LoginProvider;
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);

                return View("ExternalLogin", new ExternalLoginViewModel
                {
                    Email = email
                });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    throw new ApplicationException("Error loading external login information during confirmation.");
                }
                var user = new User
                {
                    UserName = model.UserName,
                    Email = model.Email,
                };
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction(actionName: "Index", controllerName: "Home");
                    }
                }
                AddErrors(result);
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(nameof(ExternalLogin), model);
        }

        [Authorize]
        public IActionResult Profile(string username)
        {
            var user = this.User.Identity.Name;

            var profile = this._accountService.Profile(username);
            var courses = this._accountService.GetUserCourses(user);
            var events = this._accountService.GetUserEvents(user);

            var profileViewModelCollection = new UserProfileCollectionViewModel
            {
                Profile = profile,
                Courses = courses,
                Events = events
            };

            return this.View(profileViewModelCollection);
        }

        [Authorize]
        public IActionResult UpdateProfile(string username)
        {
            var profile = this._accountService.Profile(username);

            return this.View(profile);
        }

        [HttpPost]
        public IActionResult UpdateProfile(UserProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData[GlobalConstants.TempDataErrorMessageKey] = "Invalid data.";
                return Redirect(UserProfilePath + model.Username);
            }

            if (model.FirstName == null || model.LastName == null)
            {
                TempData[GlobalConstants.TempDataErrorMessageKey] = "Invalid data.";
                return Redirect(UserProfilePath + model.Username);
            }

            this._accountService.UpdateProfile(model);

            return Redirect(UserProfilePath + model.Username);
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                return RedirectToAction(nameof(SetPassword));
            }
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                AddErrors(changePasswordResult);
                return View(model);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);

            TempData[GlobalConstants.TempDataSuccessMessageKey] = "Your password has been changed.";
            return Redirect(UserProfilePath + user.UserName);
        }

        [HttpGet]
        public async Task<IActionResult> SetPassword()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null )
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (user.PasswordHash == null)
            {
                TempData[GlobalConstants.TempDataErrorMessageKey] = "Your password can not be changed.";
                return Redirect(UserProfilePath + user.UserName);
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);

            if (hasPassword)
            {
                return RedirectToAction(nameof(ChangePassword));
            }
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var addPasswordResult = await _userManager.AddPasswordAsync(user, model.NewPassword);
            if (!addPasswordResult.Succeeded)
            {
                AddErrors(addPasswordResult);
                return View(model);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            StatusMessage = "Your password has been set.";

            return RedirectToAction(nameof(SetPassword));
        }
    }
}
