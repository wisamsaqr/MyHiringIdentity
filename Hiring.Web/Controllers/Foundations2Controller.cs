//using Hiring.Data.ViewModels;
//using Hiring.Service.Services;
//using Microsoft.AspNetCore.Mvc;

//namespace Hiring.Web.Controllers;

//public class Foundations2Controller : Controller
//{
//    private readonly IFoundationService2 _foundationService;
//    public Foundations2Controller(IFoundationService2 foundatioService)
//    {
//        _foundationService = foundatioService;
//    }

//    public IActionResult Index()
//    {
//        return View(_foundationService.GetAll());
//    }
//    public async Task<IActionResult> IndexDataPagination(int page)
//    {
//        // IndexPaginated
//        return View(await _foundationService.GetAllDataPagination(page));
//    }
//    public async Task<IActionResult> IndexPagination(string q = "", int page = 1)
//    {
//        // IndexPaginated2
//        ViewBag.q = q;

//        // For _MyPaginationX.cshtml
//        //ViewBag.currentPage = page;
//        //ViewBag.pagesCount = 28;
//        ///////////////////////////////
//        return View(await _foundationService.GetAllPagination(q, page));
//    }

//    [HttpGet]
//    public IActionResult Create()
//    {
//        return View();
//    }
//    [HttpPost]
//    public async Task<IActionResult> Create(CreateFoundationVm2 createFoundationVm)
//    {
//        if (ModelState.IsValid)
//        {
//            await _foundationService.Create(createFoundationVm);
//            return RedirectToAction("IndexPagination");
//        }
//        else
//        {
//            return View(createFoundationVm);
//        }
//    }

//    [HttpGet] public async Task<IActionResult> Edit(int id) => View(await _foundationService.GetEditVm(id));
//    [HttpPost]
//    public async Task<IActionResult> Edit(EditFoundationVm2 editFoundationVm)
//    {
//        if (ModelState.IsValid)
//        {
//            await _foundationService.Edit(editFoundationVm);
//            return RedirectToAction("IndexPagination");
//        }
//        else
//        {
//            return View(editFoundationVm);
//        }
//    }

//    public async Task<IActionResult> Delete(int id)
//    {
//        await _foundationService.Delete(id);
//        return RedirectToAction("IndexPagination");
//    }
//}