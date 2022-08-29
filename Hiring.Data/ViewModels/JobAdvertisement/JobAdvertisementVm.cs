using System.ComponentModel.DataAnnotations;
using Hiring.Data.Enums;
using Hiring.Data.Models;

namespace Hiring.Data.ViewModels
{
    public class JobAdvertisementVm
    {
        public int Id { get; set; }

        [Display(Name = "عنوان الإعلان")]
        public string Title { get; set; }

        [Display(Name = "اسم المؤسسة")]
        public FoundationVm Foundation { get; set; }

        [Display(Name = "تارخ انتهاء الإعلان")]
        public DateTime Deadline { get; set; }

        [Display(Name = "حالة الإعلان")]
        public JobAdvertisementStatus Status { get; set; }

        [Display(Name = "عدد المتقدمين")]
        public int ApplicantsCount { get; set; }
    }
}