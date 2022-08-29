using System.ComponentModel.DataAnnotations;

namespace Hiring.Data.ViewModels
{
    public class EditJobAdvertisementVm
    {
        public int Id { get; set; }

        [Display(Name = "عنوان الإعلان")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string Title { get; set; }

        [Display(Name = "تفاصيل الإعلان")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string Details { get; set; }

        [Display(Name = "تاريخ انتهاء الإعلان")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public DateTime Deadline { get; set; }

        [Display(Name = "المؤسسة")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public int FoundationId { get; set; }
    }
}