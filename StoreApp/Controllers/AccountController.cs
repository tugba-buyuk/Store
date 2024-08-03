using Entities.Dtos;
using Entities.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Contracts;
using StoreApp.Infrastructure.Extensions;
using StoreApp.Models;
using System.Security.Claims;

namespace StoreApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IServiceManager _manager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IServiceManager manager, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _manager = manager;
            _logger = logger;
        }

        public IActionResult Login([FromQuery(Name = "ReturnUrl")] string ReturnUrl = "/")
        {
            return View(new LoginModel()
            {
                ReturnUrl = ReturnUrl
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                // Kullanıcıyı bul
                var user = await _userManager.FindByNameAsync(model.Name);

                // Kullanıcı varsa, giriş yapmayı dene
                if (user != null)
                {
                    // Önce çıkış yap
                    await _signInManager.SignOutAsync();

                    // Kullanıcı adı ve şifre ile giriş yap
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        // Başarılı giriş, ReturnUrl'e yönlendir
                        _logger.LogInformation("Login successful. Redirecting to: {ReturnUrl}", model.ReturnUrl);
                        return Redirect(model.ReturnUrl ?? "/");

                    }
                    else
                    {
                        // Geçersiz giriş denemesi
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    }
                }
                else
                {
                    // Kullanıcı bulunamadıysa hata mesajı ekle
                    ModelState.AddModelError(string.Empty, "Invalid username or password.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception and add a generic error message
                _logger.LogError(ex, "An error occurred while processing the login.");
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
            }

            // Modeli yeniden görüntüle
            return View(model);
        }


        public async Task<IActionResult> Logout([FromQuery(Name = "ReturnUrl")] string ReturnUrl = "/")
        {
            await _signInManager.SignOutAsync();
            return Redirect(ReturnUrl);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromForm] RegisterDto model)
        {
            var user = new IdentityUser
            {
                UserName = model.UserName,
                Email = model.Email,
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(user, "User");
                if (roleResult.Succeeded)
                {
                    // Generate the email confirmation token
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("ConfirmEmail", "Account",
                        new { userId = user.Id, token = token }, Request.Scheme);

                    // Prepare and send the email
                    var emailMessage = new EmailMessageModel(
                        toAddress: user.Email,
                        subject: "Confirm your email",
                        body: $"Please click the following link to confirm your email: <a href={confirmationLink}>Confirm Email</a>"
                    );

                    await _manager.EmailService.Send(emailMessage);

                    return RedirectToAction("Login", new { ReturnUrl = "/" });
                }
            }
            else
            {
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return BadRequest("User ID and token must be provided.");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest("Invalid user.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return View("ConfirmEmail");
            }

            return View("Error");
        }

        public IActionResult AccessDenied([FromQuery(Name = "ReturnUrl")] string returnUrl)
        {
            return View();
        }

        public IActionResult GoogleLogin(string returnUrl)
        {
            string RedirectUrl = Url.Action("ExternalResponse", "Account", new { returnUrl = returnUrl });

            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", RedirectUrl);
            return new ChallengeResult("Google", properties);

        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalResponse(string returnUrl = "/")
        {
            try
            {
                ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync();

                if (info == null)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);

                    if (result.Succeeded)
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        var user = new IdentityUser
                        {
                            Email = info.Principal.FindFirst(ClaimTypes.Email)?.Value,
                            UserName = (info.Principal.HasClaim(x => x.Type == ClaimTypes.Name)
                                ? info.Principal.FindFirst(ClaimTypes.Name)?.Value
                                    .Replace(' ', '-')
                                    .RemoveTurkishCharacters()
                                    .ToLower()
                                    + info.Principal.FindFirst(ClaimTypes.NameIdentifier)?.Value.Substring(0, 5)
                                : info.Principal.FindFirst(ClaimTypes.NameIdentifier)?.Value.Substring(0, 5))
                                .RemoveTurkishCharacters(),
                            EmailConfirmed = true
                        };

                        var createResult = await _userManager.CreateAsync(user);
                        if (createResult.Succeeded)
                        {
                            var addToRoleResult = await _userManager.AddToRoleAsync(user, "User");
                            if (addToRoleResult.Succeeded)
                            {
                                var loginResult = await _userManager.AddLoginAsync(user, info);
                                if (loginResult.Succeeded)
                                {
                                    await _signInManager.SignInAsync(user, true);
                                    return Redirect(returnUrl);
                                }
                                else
                                {
                                    foreach (var error in loginResult.Errors)
                                    {
                                        ModelState.AddModelError("", error.Description);
                                    }
                                }
                            }
                            else
                            {
                                foreach (var error in addToRoleResult.Errors)
                                {
                                    ModelState.AddModelError("", error.Description);
                                }
                            }
                        }
                        else
                        {
                            foreach (var error in createResult.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }

            return View();
        }

        public IActionResult FacebookLogin(string returnUrl)
        {
            string RedirectUrl = Url.Action("FacebookResponse", "Account", new { returnUrl = returnUrl });

            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Facebook", RedirectUrl);
            return new ChallengeResult("Facebook", properties);

        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> FacebookResponse(string returnUrl = "/")
        {
            try
            {
                ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync();

                if (info == null)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);

                    if (result.Succeeded)
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        var user = new IdentityUser
                        {
                            Email = info.Principal.FindFirst(ClaimTypes.Email)?.Value,
                            UserName = (info.Principal.HasClaim(x => x.Type == ClaimTypes.Name)
                                ? info.Principal.FindFirst(ClaimTypes.Name)?.Value
                                    .Replace(' ', '-')
                                    .RemoveTurkishCharacters()
                                    .ToLower()
                                    + info.Principal.FindFirst(ClaimTypes.NameIdentifier)?.Value.Substring(0, 5)
                                : info.Principal.FindFirst(ClaimTypes.NameIdentifier)?.Value.Substring(0, 5))
                                .RemoveTurkishCharacters(),
                            EmailConfirmed = true
                        };

                        var createResult = await _userManager.CreateAsync(user);
                        if (createResult.Succeeded)
                        {
                            var addToRoleResult = await _userManager.AddToRoleAsync(user, "User");
                            if (addToRoleResult.Succeeded)
                            {
                                var loginResult = await _userManager.AddLoginAsync(user, info);
                                if (loginResult.Succeeded)
                                {
                                    await _signInManager.SignInAsync(user, true);
                                    return Redirect(returnUrl);
                                }
                                else
                                {
                                    foreach (var error in loginResult.Errors)
                                    {
                                        ModelState.AddModelError("", error.Description);
                                    }
                                }
                            }
                            else
                            {
                                foreach (var error in addToRoleResult.Errors)
                                {
                                    ModelState.AddModelError("", error.Description);
                                }
                            }
                        }
                        else
                        {
                            foreach (var error in createResult.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }

            return View();
        }

    }
}
