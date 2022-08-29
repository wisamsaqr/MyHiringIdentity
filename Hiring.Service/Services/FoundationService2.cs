//using AutoMapper;
//using Hiring.Data;
//using Hiring.Data.Models;
//using Microsoft.EntityFrameworkCore;
//using Hiring.Service.Utilities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Hiring.Data.ViewModels;

//namespace Hiring.Service.Services;

//public class FoundationService2 : IFoundationService2
//{
//    private readonly ApplicationDbContext _db;
//    private readonly IUserService _userService;
//    private readonly IMapper _mapper;
//    public FoundationService2(ApplicationDbContext db, IUserService userService, IMapper mapper)
//    {
//        _db = db;
//        _userService = userService;
//        _mapper = mapper;
//    }


//    public IEnumerable<FoundationVm> GetAll()
//    {
//        // Here we didn't create FoundationVm because the the data that is viewed is the same as the Foundation model,
//        // so ther is noneed to create FoundationVm for viewind the data inside the index.
//        return _db.Foundations.Include(f => f.User).Where(f => !f.IsDelete);
//    }
//    public async Task<DataPagination<IEnumerable<FoundationVm>>> GetAllDataPagination(int pageNumber)
//    {
//        // GetAllPaginated
//        var pageSize = 10;
//        var pagesCount = Math.Ceiling(await _db.Foundations.CountAsync(f => !f.IsDelete) / (double)(pageSize));

//        pageNumber = pageNumber < 1 || pageNumber > pagesCount ? 1 : pageNumber;

//        var skipValue = (pageNumber - 1) * pageSize;
//        var foundations = await _db.Foundations.Include(f => f.User).Where(f => !f.IsDelete)
//            .Skip(skipValue).Take(pageSize).ToListAsync();

//        DataPagination<IEnumerable<FoundationVm>> dataPagination =
//            new DataPagination<IEnumerable<FoundationVm>>(foundations, pageNumber, (int)pagesCount);
//        return dataPagination;
//    }
//    public async Task<Pagination<IEnumerable<FoundationVm>>> GetAllPagination(string q, int pageNumber)
//    {
//        // GetAllPaginated2
//        var pageSize = 10;
//        var pagesCount = Math.Ceiling(await _db.Foundations.CountAsync(f =>
//            (f.Name.Contains(q) || f.User.Email.Contains(q) || f.User.PhoneNumber.Contains(q)
//            || string.IsNullOrEmpty(q)) && !f.IsDelete) / (double)(pageSize));

//        pageNumber = pageNumber < 1 || pageNumber > pagesCount ? 1 : pageNumber;

//        var skipValue = (pageNumber - 1) * pageSize;
//        var foundations = await _db.Foundations.Include(f => f.User)
//            .Where(f => (f.Name.Contains(q) || f.User.Email.Contains(q) || f.User.PhoneNumber.Contains(q)
//            || string.IsNullOrEmpty(q)) && !f.IsDelete)
//            .Skip(skipValue).Take(pageSize).ToListAsync();

//        Pagination<IEnumerable<FoundationVm>> pagination = new Pagination<IEnumerable<FoundationVm>>();
//        pagination.PagesCount = (int)pagesCount;
//        pagination.CurrentPage = pageNumber;
//        pagination.Data = foundations;
//        return pagination;
//    }


//    public async Task Create(CreateFoundationVm2 createFoundationVm)
//    {
//        var dbFoundation = _mapper.Map<Foundation>(createFoundationVm);
//        dbFoundation.Users.Add.UserName = dbFoundation.User.Email;
//        dbFoundation.User.UserType = Data.Enums.UserType.Foundation;

//        await _db.Foundations.AddAsync(dbFoundation);
//        await _db.SaveChangesAsync();
//    }


//    public async Task<EditFoundationVm2> GetEditVm(int id)
//    {
//        var dbFoundation = await _db.Foundations.Include(f => f.User)
//            .SingleOrDefaultAsync(f => f.Id == id && !f.IsDelete);
//        return _mapper.Map<EditFoundationVm2>(dbFoundation);
//    }
//    public async Task Edit(EditFoundationVm2 editFoundationVm)
//    {
//        var dbFoundation = await _db.Foundations.Include(f => f.User)
//            .SingleOrDefaultAsync(f => !f.IsDelete && f.Id == editFoundationVm.Id);
//        _mapper.Map(editFoundationVm, dbFoundation);
//        _db.Foundations.Update(dbFoundation);
//        await _db.SaveChangesAsync();
//    }


//    public async Task Delete(int id)
//    {
//        var dbFoundation = await _db.Foundations.SingleOrDefaultAsync(f => !f.IsDelete && f.Id == id);
//        dbFoundation.IsDelete = true;
//        _db.Foundations.Update(dbFoundation);
//        await _db.SaveChangesAsync();
//    }


//    public IEnumerable<FoundationVm> GetAllForDropdownList()
//    {
//        return _db.Foundations.Where(x => !x.IsDelete);
//    }
//}

//public interface IFoundationService2
//{
//    public IEnumerable<FoundationVm> GetAll();
//    public Task<DataPagination<IEnumerable<FoundationVm>>> GetAllDataPagination(int pageNumber);
//    public Task<Pagination<IEnumerable<FoundationVm>>> GetAllPagination(string q, int pageNumber);
//    public Task Create(CreateFoundationVm2 createFoundationVm);
//    public Task<EditFoundationVm2> GetEditVm(int id);
//    public Task Edit(EditFoundationVm2 editFoundationVm);
//    public Task Delete(int id);
//    public IEnumerable<FoundationVm> GetAllForDropdownList();
//}