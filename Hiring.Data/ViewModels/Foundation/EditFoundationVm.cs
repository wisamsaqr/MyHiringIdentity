using System.ComponentModel.DataAnnotations;

namespace Hiring.Data.ViewModels
{
    public class EditFoundationVm
    {
        [Required]
        public int Id { get; set; }

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
    }

    //// ////

    public class EditFoundationVm2
    {
        [Required]
        public int Id { get; set; }

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

        public EditUserVm User { get; set; }
    }
}