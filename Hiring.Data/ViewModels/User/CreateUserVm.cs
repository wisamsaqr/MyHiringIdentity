using Hiring.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Hiring.Data.ViewModels
{
    public class CreateUserVm
    {
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
        [Display(Name = "نوع المستخدم")]
        public UserType UserType { get; set; }

        [Display(Name = "المؤسسة")]
        public int? FoundationId { get; set; }
    }
}