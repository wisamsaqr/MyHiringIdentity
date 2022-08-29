using Hiring.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Hiring.Data.ViewModels
{
    public class UserVm
    {
        public string Id { get; set; }

        [Display(Name = "اسم المستخدم")]
        public string UserName { get; set; }

        [Display(Name = "البريد الإلكتروني")]
        public string Email { get; set; }

        [Display(Name = "رقم الهاتف")]
        public string PhoneNumber { get; set; }

        [Display(Name = "نوع المستخدم")]
        public UserType UserType { get; set; }

        [Display(Name = "تاريخ الإضافة")]
        public DateTime CreatedAt { get; set; }

        public FoundationVm Foundation { get; set; }
    }
}