using Hiring.Data.ViewModels;
using Hiring.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hiring.Web.Controllers
{
    public class JobSeekersController : BaseController
    {
        private readonly IJobSeekerService jobSeekerService;
        public JobSeekersController(IJobSeekerService jobSeekerService, IUserService userService) : base(userService)
        {
            this.jobSeekerService = jobSeekerService;
        }

        public async Task<IActionResult> Index(string q = "", int page = 1)
        {
            ViewBag.q = q;
            return View(await jobSeekerService.GetAll(q, page));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        // Trainer
        //public async Task<IActionResult> Create([FromForm]CreateJobSeekerVm createJobSeekerVm)
        public async Task<IActionResult> Create(CreateJobSeekerVm createJobSeekerVm)
        {
            if(ModelState.IsValid)
            {
                await jobSeekerService.Create(createJobSeekerVm);
                return RedirectToAction("Index");
            }
            return View(createJobSeekerVm);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            return View(await jobSeekerService.GetEditVm(id));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditJobSeekerVm editJobSeekerVm)
        {
            if(ModelState.IsValid)
            {
                await jobSeekerService.Edit(editJobSeekerVm);
                return RedirectToAction("Index");
            }
            return View(editJobSeekerVm);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await jobSeekerService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}