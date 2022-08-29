using System.ComponentModel.DataAnnotations;

namespace Hiring.Data.ViewModels
{
    public class AdminVm
    {
        public int Id { get; set; }

        [Display(Name = "الاسم كاملا")]
        public string FullName { get; set; }

        [Display(Name = "الصورة")]
        public string? ImageUrl { get; set; }

        public UserVm User { get; set; }
    }
}