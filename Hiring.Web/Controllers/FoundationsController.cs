using Hiring.Data.ViewModels;
using Hiring.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hiring.Web.Controllers;

public class FoundationsController : BaseController
{
    private readonly IFoundationService _foundationService;
    public FoundationsController(IFoundationService foundatioService)
    {
        _foundationService = foundatioService;
    }

    public IActionResult Index()
    {
        return View(_foundationService.GetAll());
    }
    public async Task<IActionResult> IndexDataPagination(int page)
    {
        // IndexPaginated
        return View(await _foundationService.GetAllDataPagination(page));
    }
    public async Task<IActionResult> IndexPagination(string q = "", int page = 1)
    {
        // IndexPaginated2
        ViewBag.q = q;

        // For _MyPaginationX.cshtml
        //ViewBag.currentPage = page;
        //ViewBag.pagesCount = 28;
        ///////////////////////////////
        return View(await _foundationService.GetAllPagination(q, page));
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateFoundationVm createFoundationVm)
    {
        if(ModelState.IsValid)
        {
            await _foundationService.Create(createFoundationVm);
            return RedirectToAction("Index");
        }
        else
        {
            return View(createFoundationVm);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        return View(await _foundationService.GetEditVm(id));
    }
    [HttpPost]
    public async Task<IActionResult> Edit(EditFoundationVm editFoundationVm)
    {
        if(ModelState.IsValid)
        {
            await _foundationService.Edit(editFoundationVm);
            return RedirectToAction("Index");
        }
        else
        {
            return View(editFoundationVm);
        }
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _foundationService.Delete(id);
        return RedirectToAction("Index");
    }
}