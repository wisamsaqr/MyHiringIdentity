using AutoMapper;
using Hiring.Data;
using Hiring.Data.Models;
using Hiring.Data.ViewModels;
using Hiring.Service.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Hiring.Service.Services;

public class FoundationService : IFoundationService
{
    private readonly ApplicationDbContext db;
    private readonly IUserService userService;
    private readonly IMapper mapper;
    public FoundationService(ApplicationDbContext db, IUserService userService, IMapper mapper)
    {
        this.db = db;
        this.userService = userService;
        this.mapper = mapper;
    }

    public IEnumerable<FoundationVm> GetAll()
    {
        // Here we didn't create FoundationVm because the the data that is viewed is the same as the Foundation model,
        // so ther is noneed to create FoundationVm for viewind the data inside the index.
        return mapper.Map<IEnumerable<FoundationVm>>(db.Foundations.Where(f => !f.IsDelete));
    }
    public async Task<DataPagination<IEnumerable<FoundationVm>>> GetAllDataPagination(int pageNumber)
    {
        // GetAllPaginated
        var pageSize = 10;
        var pagesCount = Math.Ceiling(await db.Foundations.CountAsync(f => !f.IsDelete) / (double)(pageSize));

        pageNumber = pageNumber < 1 || pageNumber > pagesCount ? 1 : pageNumber;

        var skipValue = (pageNumber - 1) * pageSize;
        var foundations = mapper.Map<IEnumerable<FoundationVm>>(await db.Foundations.Where(f => !f.IsDelete)
            .Skip(skipValue).Take(pageSize).ToListAsync());

        DataPagination<IEnumerable<FoundationVm>> dataPagination =
            new DataPagination<IEnumerable<FoundationVm>>(foundations, pageNumber, (int)pagesCount);
        return dataPagination;
    }
    public async Task<Pagination<IEnumerable<FoundationVm>>> GetAllPagination(string q, int pageNumber)
    {
        // GetAllPaginated2
        var pageSize = 10;
        var pagesCount = Math.Ceiling(await db.Foundations.CountAsync(f =>
            (f.Name.Contains(q) || f.Email.Contains(q) || f.PhoneNumber.Contains(q)
            || string.IsNullOrEmpty(q)) && !f.IsDelete) / (double)(pageSize));

        pageNumber = pageNumber < 1 || pageNumber > pagesCount ? 1 : pageNumber;

        var skipValue = (pageNumber - 1) * pageSize;
        var foundations = mapper.Map<IEnumerable<FoundationVm>>(await db.Foundations
            .Where(f => (f.Name.Contains(q) || f.Email.Contains(q) || f.PhoneNumber.Contains(q)
            || string.IsNullOrEmpty(q)) && !f.IsDelete).Skip(skipValue).Take(pageSize).ToListAsync());

        Pagination<IEnumerable<FoundationVm>> pagination = new Pagination<IEnumerable<FoundationVm>>();
        pagination.PagesCount = (int)pagesCount;
        pagination.CurrentPage = pageNumber;
        pagination.Data = foundations;
        return pagination;
    }

    public async Task Create(CreateFoundationVm createFoundationVm)
    {
        var dbFoundation = mapper.Map<Foundation>(createFoundationVm);
        await db.Foundations.AddAsync(dbFoundation);
        await db.SaveChangesAsync();

        try
        {
            await userService.Create(new CreateUserVm()
            {
                Email = createFoundationVm.Email,
                PhoneNumber = createFoundationVm.PhoneNumber,
                Password = createFoundationVm.Password,
                UserType = Data.Enums.UserType.Foundation,
                FoundationId = dbFoundation.Id
            });
            await db.SaveChangesAsync();

        }
        catch (AppIdentityException exc)
        {
            db.Foundations.Remove(dbFoundation);
            await db.SaveChangesAsync();
        }
    }

    public async Task<EditFoundationVm> GetEditVm(int id)
    {
        var dbFoundation = await db.Foundations.SingleOrDefaultAsync(f => f.Id == id && !f.IsDelete);
        var editFoundationVm = mapper.Map<EditFoundationVm>(dbFoundation);
        return editFoundationVm;
    }
    public async Task Edit(EditFoundationVm editFoundationVm)
    {
        var dbFoundation = await db.Foundations.SingleOrDefaultAsync(f => !f.IsDelete && f.Id == editFoundationVm.Id);
        mapper.Map(editFoundationVm, dbFoundation);
        db.Foundations.Update(dbFoundation);
        await db.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var dbFoundation = await db.Foundations.SingleOrDefaultAsync(f => !f.IsDelete && f.Id == id);
        dbFoundation.IsDelete = true;
        db.Foundations.Update(dbFoundation);
        await db.SaveChangesAsync();
    }

    public IEnumerable<FoundationVm> GetAllForDropdownList()
    {
        var xxx = mapper.Map<IEnumerable<FoundationVm>>(db.Foundations.Where(x => !x.IsDelete));
        return mapper.Map<IEnumerable<FoundationVm>>(db.Foundations.Where(x => !x.IsDelete));
    }
}

public interface IFoundationService
{
    public IEnumerable<FoundationVm> GetAll();
    public Task<DataPagination<IEnumerable<FoundationVm>>> GetAllDataPagination(int pageNumber);
    public Task<Pagination<IEnumerable<FoundationVm>>> GetAllPagination(string q, int pageNumber);
    public Task Create(CreateFoundationVm newFoundationVm);
    public Task<EditFoundationVm> GetEditVm(int id);
    public Task Edit(EditFoundationVm editFoundationVm);
    public Task Delete(int id);
    public IEnumerable<FoundationVm> GetAllForDropdownList();
}




/**** ****
 * await _db.Foundations.AddAsync(newFoundation);
 * Create       => Create   HttpPost
 * 
 * await _db.Organizations.SingleOrDefaultAsync(o => o.Id == id && !o.IsDelete);
 * GetEditVm    => Edit     HttpGet
 * Edit         => Edit     HttpPost
 * Delete       => Delete
 * 
 * await _db.SaveChangesAsync();
 * Create
 * Edit
 * Delete
 * 
 * _db.Organizations.Update(organization);
 * _db.Organizations.Remove(organization);
 * 
 * --------------------------------------------------------------------------------------------------
 * 
 * var dbUser = await _userManager.FindByIdAsync(id);
 * var result = await _userManager.CreateAsync(user, createUserDto.Password);
 * var result = await _userManager.UpdateAsync(updatedUser);
 * var result = await _userManager.DeleteAsync(user);
 * var result = await _userManager.DeleteAsync(await _userManager.FindByIdAsync(id));
 */