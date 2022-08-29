using System.ComponentModel.DataAnnotations;

namespace Hiring.Data.ViewModels
{
    public class FoundationVm
    {
        public int Id { get; set; }

        [Display(Name = "اسم المؤسسة")]
        public string Name { get; set; }
        
        [Display(Name = "رقم الهاتف")]
        public string PhoneNumber { get; set; }
        
        [Display(Name = "البريد الإلكتروني")]
        public string Email { get; set; }
        
        [Display(Name = "طبيعة العمل")]
        public string WorkNature { get; set; }
        
        [Display(Name = "العنوان")]
        public string Address { get; set; }

        [Display(Name = "العنوان")]
        public string OwnerName { get; set; }
    }
}