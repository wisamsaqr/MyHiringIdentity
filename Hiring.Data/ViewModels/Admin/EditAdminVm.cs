using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Hiring.Data.ViewModels
{
    public class EditAdminVm
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "الاسم")]
        public string FullName { get; set; }
        // User Data
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "البريد")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "رقم الهاتف")]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "كلمة المرور")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "الصورة")]
        public IFormFile? Image { get; set; }
    }
}