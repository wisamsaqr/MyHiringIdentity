using AutoMapper;
using Hiring.Data;
using Hiring.Data.Enums;
using Hiring.Data.Models;
using Hiring.Data.ViewModels;
using Hiring.Service.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Hiring.Service.Services
{
    public class JobAdvertisementService : IJobAdvertisementService
    {
        private readonly IFileService fileService;
        private readonly IMapper mapper;
        private readonly IEmailService emailService;
        private readonly ApplicationDbContext db;
        public JobAdvertisementService(ApplicationDbContext db, IEmailService emailService,
            IMapper mapper, IFileService fileService)
        {
            this.db = db;
            this.mapper = mapper;
            this.emailService = emailService;
            this.fileService = fileService;
        }

        public async Task<Pagination<IEnumerable<JobAdvertisementVm>>> GetAll(JobAdvertisementStatus status, string q, int page)
        {
            var pageSize = 10;
            var pagesCount = (int)Math.Ceiling(await db.JobAdvertisements.CountAsync(x =>
                (x.Title.Contains(q) || x.Details.Contains(q) || string.IsNullOrEmpty(q))
                    && (x.Status == status || status == 0) && !x.IsDelete) / (double)pageSize);

            page = page < 1 || page > pagesCount ? 1 : page;

            var skip = (page - 1) * pageSize;
            var jobAdvertisementVms = db.JobAdvertisements.Where(x =>
                (x.Title.Contains(q) || x.Details.Contains(q) || string.IsNullOrEmpty(q))
                    && (x.Status == status || status == 0) && !x.IsDelete)
                .Skip(skip).Take(pageSize).Include(x => x.Foundation)
                .Select(x => new JobAdvertisementVm()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Deadline = x.Deadline,
                    Status = x.Status,
                    Foundation = mapper.Map<FoundationVm>(x.Foundation),
                    ApplicantsCount = x.JobSeekers.Count()
                });

            //var jobAdvertisementVms = mapper.Map<IEnumerable<JobAdvertisementVm>>(dbJobAdvertisements);

            Pagination<IEnumerable<JobAdvertisementVm>> pagination = new Pagination<IEnumerable<JobAdvertisementVm>>
            {
                Data = jobAdvertisementVms,
                CurrentPage = page,
                PagesCount = pagesCount
            };

            return pagination;
        }

        public async Task<JobAdvertisementDetailsVm> Get(int id)
        {
            return mapper.Map<JobAdvertisementDetailsVm>(await db.JobAdvertisements.Include(x => x.Attachments)
                .Include(x => x.Foundation).SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete));
        }

        public async Task Create(CreateJobAdvertisementVm createJobAdvertisementVm)
        {
            var dbJobAdvertisement = mapper.Map<JobAdvertisement>(createJobAdvertisementVm);
            dbJobAdvertisement.Status = JobAdvertisementStatus.Pending;
            await db.JobAdvertisements.AddAsync(dbJobAdvertisement);
            await db.SaveChangesAsync();

            foreach(Microsoft.AspNetCore.Http.IFormFile? attachment in createJobAdvertisementVm.Attachments)
            {
                var fileName = await fileService.SaveFile2(attachment, "attachments");
                var dbJobAdvertisementAttachment = new JobAdvertisementAttachment()
                {
                    JobAdvertisementId = dbJobAdvertisement.Id,
                    Url = fileName
                };
                await db.JobAdvertisementAttachments.AddAsync(dbJobAdvertisementAttachment);

                // The trainer has put this line to save changes after erach iteration, but we moved it outside the loop
                // so that we save the entire changes after finishing adding all the attachments.
                //await db.SaveChangesAsync();
            }

            await db.SaveChangesAsync();
        }

        public async Task<EditJobAdvertisementVm> GetEditVm(int id)
        {
            return mapper.Map<EditJobAdvertisementVm>(
                await db.JobAdvertisements.SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete));
        }

        public async Task Edit(EditJobAdvertisementVm editJobAdvertisementVm)
        {
            var dbJobAdvertisement = mapper.Map<JobAdvertisement>(editJobAdvertisementVm);
            db.JobAdvertisements.Update(dbJobAdvertisement);
            await db.SaveChangesAsync();
        }

        public async Task SetStatus(int id, JobAdvertisementStatus status)
        {
            var dbJobAdvertisement = await db.JobAdvertisements.SingleOrDefaultAsync(x => x.Id == id);
            //var dbJobAdvertisement = await db.JobAdvertisements.Include(jobAdvertisement => jobAdvertisement.Foundation)
            //    .ThenInclude(foundation => foundation.User)
            //    .SingleOrDefaultAsync(x => x.Id == id);
            dbJobAdvertisement.Status = status;
            db.JobAdvertisements.Update(dbJobAdvertisement);
            await db.SaveChangesAsync();

            await emailService.SendAsync(dbJobAdvertisement.Foundation.Email,
                "Your job advertisement status has been updated",
                $"Your job advertisement is {status}");
        }


        public async Task Delete(int id)
        {
            var dbJobAdvertisement = await db.JobAdvertisements.SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);
            dbJobAdvertisement.IsDelete = true;
            db.JobAdvertisements.Update(dbJobAdvertisement);
            await db.SaveChangesAsync();
        }

        public async Task<List<JobSeekerVm>> GetAllApplicants(int jobAdvertisementId)
        {
            var jobSeekers = await db.JobAdvertisementJobSeekers.Include(x => x.JobSeeker).ThenInclude(x => x.User).Where(x => x.JobAdvertisementId == jobAdvertisementId)
                .Select(x => x.JobSeeker).ToListAsync();
            //var jobSeekers = await db.JobAdvertisementJobSeekers.Include(x => x.JobSeeker.User).Where(x => x.JobAdvertisementId == jobAdvertisementId)
            //    .Select(x => x.JobSeeker).ToListAsync();

            return mapper.Map<List<JobSeekerVm>>(jobSeekers);
        }
    }

    public interface IJobAdvertisementService
    {
        public Task<Pagination<IEnumerable<JobAdvertisementVm>>> GetAll(JobAdvertisementStatus status, string q, int page);
        public Task<JobAdvertisementDetailsVm> Get(int id);
        public Task Create(CreateJobAdvertisementVm createJobAdvertisementVm);
        public Task<EditJobAdvertisementVm> GetEditVm(int id);
        public Task Edit(EditJobAdvertisementVm editJobAdvertisementVm);
        public Task SetStatus(int id, JobAdvertisementStatus status);
        public Task<List<JobSeekerVm>> GetAllApplicants(int jobAdvertisementId);
        public Task Delete(int id);
    }
}