using System.ComponentModel.DataAnnotations;

namespace Hiring.Data.ViewModels
{
    public class EditUserVm
    {
        [Required]
        public string Id { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "البريد")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "رقم الهاتف")]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}