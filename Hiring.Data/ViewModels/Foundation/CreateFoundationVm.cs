using System.ComponentModel.DataAnnotations;

namespace Hiring.Data.ViewModels
{
    public class CreateFoundationVm
    {
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "اسم المؤسسة")]
        public string Name { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "اسم المالك")]
        public string OwnerName { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "طبيعة العمل")]
        public string WorkNature { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "العنوان")]
        public string Address { get; set; }

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
    }

    //// ////

    public class CreateFoundationVm2
    {
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "اسم المؤسسة")]
        public string Name { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "اسم المالك")]
        public string OwnerName { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "طبيعة العمل")]
        public string WorkNature { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "العنوان")]
        public string Address { get; set; }

        // User Data
        // Instead of adding User data properties, we can add User property as CreateUserVm, for receiving user data.
        public CreateUserVm User { get; set; }
    }
}