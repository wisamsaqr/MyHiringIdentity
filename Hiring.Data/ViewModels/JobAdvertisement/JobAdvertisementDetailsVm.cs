using System.ComponentModel.DataAnnotations;
using Hiring.Data.Enums;
using Hiring.Data.Models;

namespace Hiring.Data.ViewModels
{
    // Here we can use JobAdvertisement model directly, but for the learning let's create JobAdvertisementDetailsVm.
    public class JobAdvertisementDetailsVm
    {
        public int Id { get; set; }

        [Display(Name = "عنوان الإعلان")]
        public string Title { get; set; }

        [Display(Name = "تفاصيل الإعلان")]
        public string Details { get; set; }

        [Display(Name = "اسم المؤسسة")]
        public FoundationVm Foundation { get; set; }
        
        [Display(Name = "تارخ انتهاء الإعلان")]
        public DateTime Deadline { get; set; }
        
        [Display(Name = "حالة الإعلان")]
        public JobAdvertisementStatus Status { get; set; }

        public List<JobAdvertisementAttachment> Attachments { get; set; }

        // The trainer has created JobAdvertisementAttachmentVm to use it here, as shown below,
        // but I don't see any reason for that.
        //public List<JobAdvertisementAttachmentVm> Attachments { get; set; }
    }
}