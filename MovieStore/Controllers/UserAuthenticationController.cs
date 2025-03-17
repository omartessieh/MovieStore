using Microsoft.AspNetCore.Mvc;
using MovieStore.Models.DTO;
using MovieStore.Repositories.Abstract;

namespace MovieStore.Controllers
{
    public class UserAuthenticationController : Controller
    {
        private IUserAuthenticationService authService;

        public UserAuthenticationController(IUserAuthenticationService authService)
        {
            this.authService = authService;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var registrationModel = new RegistrationModel
            {
                Email = model.Email,
                Username = model.Username,
                Name = model.Name,
                Password = model.Password,
                PasswordConfirm = model.PasswordConfirm,
                Role = "User"
            };

            var result = await authService.RegisterAsync(registrationModel);

            if (result.StatusCode == 1)
            {
                TempData["msg"] = "Registered Successfully";
                return RedirectToAction("Register");
            }
            else
            {
                TempData["msg"] = "Could not Register.";
                return View(model);
            }
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await authService.LoginAsync(model);
            if (result.StatusCode == 1)
                return RedirectToAction("Index", "Home");
            else
            {
                TempData["msg"] = "Could not logged in..";
                return RedirectToAction(nameof(Login));
            }
        }

        public async Task<IActionResult> Logout()
        {
            await authService.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}