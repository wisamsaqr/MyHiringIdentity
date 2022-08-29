using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Hiring.Data.ViewModels
{
    public class CreateJobSeekerVm
    {
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "الاسم")]
        public string FullName { get; set; }

        [Display(Name = "الصورة")]
        public IFormFile? Image { get; set; }

        [Display(Name = "السيرة الذاتية")]
        public IFormFile? Cv { get; set; }

        public CreateUserVm User { get; set; }
    }
}