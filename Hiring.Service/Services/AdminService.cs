using AutoMapper;
using Hiring.Data;
using Hiring.Data.Models;
using Microsoft.EntityFrameworkCore;
using Hiring.Service.Utilities;
using Hiring.Data.ViewModels;

namespace Hiring.Service.Services;

public class AdminService : IAdminService
{
    private readonly ApplicationDbContext db;
    private readonly IUserService userService;
    private readonly IMapper mapper;
    private readonly IFileService fileService;
    public AdminService(ApplicationDbContext db, IUserService userService, IMapper mapper, IFileService fileService)
    {
        this.db = db;
        this.userService = userService;
        this.mapper = mapper;
        this.fileService = fileService;
    }

    public async Task<Pagination<IEnumerable<AdminVm>>> GetAll(string q, int pageNumber)
    {
        var pageSize = 10;
        var pagesCount = Math.Ceiling(await db.Admins.CountAsync(x =>
            (x.FullName.Contains(q) || x.User.Email.Contains(q) || x.User.PhoneNumber.Contains(q)
            || string.IsNullOrEmpty(q)) && !x.IsDelete) / (double)(pageSize));

        pageNumber = pageNumber < 1 || pageNumber > pagesCount ? 1 : pageNumber;

        var skipValue = (pageNumber - 1) * pageSize;
        var dbAdmins = await db.Admins.Include(x => x.User)
            .Where(x => (x.FullName.Contains(q) || x.User.Email.Contains(q) || x.User.PhoneNumber.Contains(q)
            || string.IsNullOrEmpty(q)) && !x.IsDelete)
            .Skip(skipValue).Take(pageSize).ToListAsync();

        var adminVms = mapper.Map<IEnumerable<AdminVm>>(dbAdmins);

        Pagination<IEnumerable<AdminVm>> pagination = new Pagination<IEnumerable<AdminVm>>();
        pagination.PagesCount = (int)pagesCount;
        pagination.CurrentPage = pageNumber;
        pagination.Data = adminVms;
        return pagination;
    }

    public async Task Create(CreateAdminVm createAdminVm)
    {
        var dbAdmin = mapper.Map<Admin>(createAdminVm);
        try
        {
            dbAdmin.UserId = await userService.Create(new CreateUserVm()
            {
                Email = createAdminVm.Email,
                PhoneNumber = createAdminVm.PhoneNumber,
                Password = createAdminVm.Password,
                UserType = Data.Enums.UserType.Admin
            });
        }
        catch(AppIdentityException exc)
        {
            db.Admins.Remove(dbAdmin);
            await db.SaveChangesAsync();
        }

        if(createAdminVm.Image != null)
        {
            var imageName = await fileService.SaveFile2(createAdminVm.Image, "images");
            dbAdmin.ImageUrl = imageName;
        }

        await db.Admins.AddAsync(dbAdmin);
        await db.SaveChangesAsync();
    }

    public async Task<EditAdminVm> GetEditVm(int id)
    {
        var dbAdmin = await db.Admins.Include(x => x.User)
            .SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);
        return mapper.Map<EditAdminVm>(dbAdmin);
    }
    public async Task Edit(EditAdminVm editAdminVm)
    {
        var dbAdmin = await db.Admins.Include(f => f.User)
            .SingleOrDefaultAsync(x => !x.IsDelete && x.Id == editAdminVm.Id);
        mapper.Map(editAdminVm, dbAdmin);
        db.Admins.Update(dbAdmin);
        await db.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var dbAdmin = await db.Admins.SingleOrDefaultAsync(x => !x.IsDelete && x.Id == id);
        dbAdmin.IsDelete = true;
        db.Admins.Update(dbAdmin);
        await db.SaveChangesAsync();
    }
}

public interface IAdminService
{
    public Task<Pagination<IEnumerable<AdminVm>>> GetAll(string q, int pageNumber);
    public Task Create(CreateAdminVm createAdminVm);
    public Task<EditAdminVm> GetEditVm(int id);
    public Task Edit(EditAdminVm editAdminVm);
    public Task Delete(int id);
}