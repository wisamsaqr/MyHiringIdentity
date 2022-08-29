using Hiring.Data.ViewModels;
using Hiring.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hiring.Web.Controllers
{
    public class AdminsController : BaseController
    {
        private readonly IAdminService adminService;
        public AdminsController(IAdminService adminService, IUserService userService) : base(userService)
        {
            this.adminService = adminService;
        }

        public async Task<IActionResult> Index(string q = "", int page = 1)
        {
            ViewBag.q = q;
            return View(await adminService.GetAll(q, page));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        // Trainer
        //public async Task<IActionResult> Create([FromForm]CreateAdminVm createAdminVm)
        public async Task<IActionResult> Create(CreateAdminVm createAdminVm)
        {
            if(ModelState.IsValid)
            {
                await adminService.Create(createAdminVm);
                return RedirectToAction("Index");
            }
            return View(createAdminVm);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            return View(await adminService.GetEditVm(id));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditAdminVm editAdminVm)
        {
            if(ModelState.IsValid)
            {
                await adminService.Edit(editAdminVm);
                return RedirectToAction("Index");
            }
            return View(editAdminVm);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await adminService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}