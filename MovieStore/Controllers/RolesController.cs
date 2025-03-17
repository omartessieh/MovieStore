using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Repositories.Abstract;
using MovieStore.Repositories.Implementation;

namespace MovieStore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly IRoleRepository _roleRepository;

        public RolesController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _roleRepository.GetRolesAsync();
            return View(roles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new IdentityRole()); // Ensure model is passed
        }

        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole model)
        {
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                ModelState.AddModelError("Name", "Role name is required.");
                return View(model);
            }

            var result = await _roleRepository.CreateRoleAsync(model.Name);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(Create));
            }

            TempData["msg"] = "Error on server side";
            return View(model);
        }
    }
}