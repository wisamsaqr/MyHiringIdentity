using AutoMapper;
using Hiring.Data;
using Hiring.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hiring.Service.Utilities;
using Hiring.Data.ViewModels;

namespace Hiring.Service.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    public UserService(ApplicationDbContext db, UserManager<User> userManager, IMapper mapper)
    {
        _db = db;
        _userManager = userManager;
        _mapper = mapper;
    }

    public List<UserVm> GetAll()
    {
        return _mapper.Map<List<UserVm>>(_userManager.Users.Include(x => x.Foundation));
    }

    public async Task<string> Create(CreateUserVm createUserVm)
    {
        if (_db.Users.Any(x => x.Email == createUserVm.Email || x.PhoneNumber == createUserVm.PhoneNumber))
        {
            throw new AppIdentityException("البريد أو الهاتف مستخدم مسبقا");
        }

        var dbUser = _mapper.Map<User>(createUserVm);
        dbUser.UserName = dbUser.Email;
        var identityResult = await _userManager.CreateAsync(dbUser, createUserVm.Password);
        if (!identityResult.Succeeded)
        {
            throw new AppIdentityException(identityResult.Errors.ToString());
        }

        return dbUser.Id;
    }
    //public async Task<OperationResult<string>> Create2(CreateUserVm createUserVm)
    //{
    //    if (_db.Users.Any(x => x.Email == createUserVm.Email || x.PhoneNumber == createUserVm.PhoneNumber))
    //    {
    //        return new OperationResult<string>
    //        {
    //            Status = false,
    //            Message = "البريد أو الهاتف مستخدم مسبقا"
    //        };
    //    }

    //    var dbUser = _mapper.Map<User>(createUserVm);
    //    dbUser.UserName = dbUser.Email;
    //    var identityResult = await _userManager.CreateAsync(dbUser, createUserVm.Password);
    //    if (identityResult.Succeeded)
    //    {
    //        return new OperationResult<string>
    //        {
    //            Status = true,
    //            Id = dbUser.Id,
    //            Message = "تمت علمية الإضافة بنجاح"
    //        };
    //    }
    //    else
    //    {
    //        return new OperationResult<string>
    //        {
    //            Status = false,
    //            Message = identityResult.Errors.ToString()
    //        };
    //    }
    //}

    public async Task<EditUserVm> GetEditVm(string id)
    {
        var dbUser = await _userManager.FindByIdAsync(id);
        return _mapper.Map<EditUserVm>(dbUser);
    }
    public async Task Edit(EditUserVm editUserVm)
    {
        var dbUser = await _userManager.FindByIdAsync(editUserVm.Id);
        _mapper.Map(editUserVm, dbUser);
        await _userManager.UpdateAsync(dbUser);
    }

    public async Task Delete(string id)
    {
        await _userManager.DeleteAsync(await _userManager.FindByIdAsync(id));
    }
}
public interface IUserService
{
    public List<UserVm> GetAll();
    public Task<EditUserVm> GetEditVm(string id);
    public Task<string> Create(CreateUserVm newUserVm);
    public Task Edit(EditUserVm modifiedUserVm);
    public Task Delete(string id);
}