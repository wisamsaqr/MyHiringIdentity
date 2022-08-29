using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Hiring.Data.ViewModels
{
    public class EditJobSeekerVm
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "الاسم")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "الصورة")]
        public IFormFile? Image { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "السيرة الذاتية")]
        public IFormFile? Cv { get; set; }

        public EditUserVm User { get; set; }
    }
}