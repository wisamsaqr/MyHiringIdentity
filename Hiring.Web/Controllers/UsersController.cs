using Microsoft.AspNetCore.Mvc;
using Hiring.Service.Services;
using Hiring.Service.Utilities;
using Hiring.Data.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hiring.Web.Controllers;

public class UsersController : BaseController
{
    private readonly IUserService userService;
    private readonly IFoundationService foundationService;
    public UsersController(IUserService userService, IFoundationService foundationService)
    {
        this.userService = userService;
        this.foundationService = foundationService;
    }

    public IActionResult Index()
    {
        return View(userService.GetAll());
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.FoundationsList = new SelectList(foundationService.GetAllForDropdownList(), "Id", "Name");
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateUserVm createUserVm)
    {
        if(ModelState.IsValid)
        {
            try
            {
                await userService.Create(createUserVm);
                TempData["Message"] = "s:تمت عملية الإضافة بنجاح";
                return RedirectToAction("Index");
            }
            catch(AppIdentityException ex)
            {
                TempData["Message"] = "e:" + ex.Message;
            }
        }

        return View(createUserVm);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        return View(await userService.GetEditVm(id));
    }
    [HttpPost]
    public async Task<IActionResult> Edit(EditUserVm editUserVm)
    {
        await userService.Edit(editUserVm);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(string id)
    {
        await userService.Delete(id);
        return RedirectToAction("Index");
    }
}