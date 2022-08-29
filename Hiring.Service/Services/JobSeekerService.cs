using AutoMapper;
using Hiring.Data;
using Hiring.Data.Models;
using Microsoft.EntityFrameworkCore;
using Hiring.Service.Utilities;
using Hiring.Data.ViewModels;

namespace Hiring.Service.Services;

public class JobSeekerService : IJobSeekerService
{
    private readonly ApplicationDbContext db;
    private readonly IUserService userService;
    private readonly IMapper mapper;
    private readonly IFileService fileService;
    public JobSeekerService(ApplicationDbContext db, IUserService userService, IMapper mapper, IFileService fileService)
    {
        this.db = db;
        this.userService = userService;
        this.mapper = mapper;
        this.fileService = fileService;
    }

    public async Task<Pagination<IEnumerable<JobSeekerVm>>> GetAll(string q, int pageNumber)
    {
        var pageSize = 10;
        var pagesCount = Math.Ceiling(await db.JobSeekers.CountAsync(x =>
            (x.FullName.Contains(q) || x.User.Email.Contains(q) || x.User.PhoneNumber.Contains(q)
            || string.IsNullOrEmpty(q)) && !x.IsDelete) / (double)(pageSize));

        pageNumber = pageNumber < 1 || pageNumber > pagesCount ? 1 : pageNumber;

        var skipValue = (pageNumber - 1) * pageSize;
        var dbJobSeekers = await db.JobSeekers.Include(x => x.User)
            .Where(x => (x.FullName.Contains(q) || x.User.Email.Contains(q) || x.User.PhoneNumber.Contains(q)
            || string.IsNullOrEmpty(q)) && !x.IsDelete)
            .Skip(skipValue).Take(pageSize).ToListAsync();

        var jobSeekerVms = mapper.Map<IEnumerable<JobSeekerVm>>(dbJobSeekers);

        Pagination<IEnumerable<JobSeekerVm>> pagination = new Pagination<IEnumerable<JobSeekerVm>>();
        pagination.PagesCount = (int)pagesCount;
        pagination.CurrentPage = pageNumber;
        pagination.Data = jobSeekerVms;
        return pagination;
    }

    public async Task Create(CreateJobSeekerVm createJobSeekerVm)
    {
        var dbJobSeeker = mapper.Map<JobSeeker>(createJobSeekerVm);
        dbJobSeeker.User.UserName = dbJobSeeker.User.Email;
        dbJobSeeker.User.UserType = Data.Enums.UserType.JobSeeker;

        if (createJobSeekerVm.Cv != null)
        {
            var cvName = await fileService.SaveFile2(createJobSeekerVm.Cv, "cvs");
            dbJobSeeker.CvUrl = cvName;
        }
        if (createJobSeekerVm.Image != null)
        {
            var imageName = await fileService.SaveFile2(createJobSeekerVm.Image, "images");
            dbJobSeeker.ImageUrl = imageName;
        }

        await db.JobSeekers.AddAsync(dbJobSeeker);
        await db.SaveChangesAsync();
    }

    public async Task<EditJobSeekerVm> GetEditVm(int id)
    {
        var dbJobSeeker = await db.JobSeekers.Include(x => x.User)
            .SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);
        return mapper.Map<EditJobSeekerVm>(dbJobSeeker);
    }
    public async Task Edit(EditJobSeekerVm editJobSeekerVm)
    {
        var dbJobSeeker = await db.JobSeekers.Include(f => f.User)
            .SingleOrDefaultAsync(x => !x.IsDelete && x.Id == editJobSeekerVm.Id);
        mapper.Map(editJobSeekerVm, dbJobSeeker);
        db.JobSeekers.Update(dbJobSeeker);
        await db.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var dbJobSeeker = await db.JobSeekers.SingleOrDefaultAsync(x => !x.IsDelete && x.Id == id);
        dbJobSeeker.IsDelete = true;
        db.JobSeekers.Update(dbJobSeeker);
        await db.SaveChangesAsync();
    }
}

public interface IJobSeekerService
{
    public Task<Pagination<IEnumerable<JobSeekerVm>>> GetAll(string q, int pageNumber);
    public Task Create(CreateJobSeekerVm createJobSeekerVm);
    public Task<EditJobSeekerVm> GetEditVm(int id);
    public Task Edit(EditJobSeekerVm editJobSeekerVm);
    public Task Delete(int id);
}