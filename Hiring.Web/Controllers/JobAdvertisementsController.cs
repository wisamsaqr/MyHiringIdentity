using Hiring.Data.Enums;
using Hiring.Data.ViewModels;
using Hiring.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hiring.Web.Controllers
{
    public class JobAdvertisementsController : BaseController
    {
        private readonly IJobAdvertisementService jobAdvertisementService;
        private readonly IFoundationService foundationService;
        public JobAdvertisementsController(IJobAdvertisementService jobAdvertisementService,
            IFoundationService foundationService, IUserService userService) : base(userService)
        {
            this.jobAdvertisementService = jobAdvertisementService;
            this.foundationService = foundationService;
        }

        public async Task<IActionResult> Index(JobAdvertisementStatus status, string q = "", int page = 1)
        {
            ViewBag.q = q;
            ViewBag.status = status.ToString("D");
            return View(await jobAdvertisementService.GetAll(status, q, page));
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await jobAdvertisementService.Get(id));
        }

        public async Task<IActionResult> Applicants(int id)
        {
            return View(await jobAdvertisementService.GetAllApplicants(id));
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.FoundationsList = new SelectList(foundationService.GetAllForDropdownList(), "Id", "Name");
            return View();
        }
        [HttpPost]
        // Trainer
        //public async Task<IActionResult> Create([FromForm]CreateJobAdvertisementVm createJobAdvertisementVm)
        public async Task<IActionResult> Create(CreateJobAdvertisementVm createJobAdvertisementVm)
        {
            if(ModelState.IsValid)
            {
                await jobAdvertisementService.Create(createJobAdvertisementVm);
                return RedirectToAction("Index");
            }
            return View(createJobAdvertisementVm);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.FoundationsList = new SelectList(foundationService.GetAllForDropdownList(), "Id", "Name", id);
            return View(await jobAdvertisementService.GetEditVm(id));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditJobAdvertisementVm editJobAdvertisementVm)
        {
            if(ModelState.IsValid)
            {
                await jobAdvertisementService.Edit(editJobAdvertisementVm);
                return RedirectToAction("Index");
            }
            ViewBag.FoundationsList = new SelectList(foundationService.GetAllForDropdownList(), "Id", "Name",
                editJobAdvertisementVm.Id);
            //return View(editJobAdvertisementVm);
            return View();
        }

        public async Task<IActionResult> SetStatus(int id, JobAdvertisementStatus status)
        {
            await jobAdvertisementService.SetStatus(id, status);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await jobAdvertisementService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}