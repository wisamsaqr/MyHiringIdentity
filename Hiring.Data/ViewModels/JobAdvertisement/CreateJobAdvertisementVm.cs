using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Hiring.Data.ViewModels
{
    public class CreateJobAdvertisementVm
    {
        [Display(Name = "عنوان الإعلان")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string Title { get; set; }

        [Display(Name = "تفاصيل الإعلان")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string Details { get; set; }

        [Display(Name = "تاريخ انتهاء الإعلان")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DataType(DataType.Date)]
        public DateTime Deadline { get; set; }

        [Display(Name = "المؤسسة")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public int FoundationId { get; set; }

        [Display(Name = "المرفقات")]
        public List<IFormFile>? Attachments { get; set; }
    }
}