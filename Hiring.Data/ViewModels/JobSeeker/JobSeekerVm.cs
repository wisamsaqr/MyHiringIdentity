using System.ComponentModel.DataAnnotations;

namespace Hiring.Data.ViewModels
{
    // There is no need for this view model, since the only difference between the model and this view model is UserId
    // property. Exactly as the Foundation, for which we haven't created a view model, and instead we used the model
    // itself. However, we created this view model for training, nomore.
    public class JobSeekerVm
    {
        public int Id { get; set; }

        [Display(Name = "الاسم كاملا")]
        public string FullName { get; set; }

        [Display(Name = "الصورة")]
        public string? ImageUrl { get; set; }

        [Display(Name = "السيرة الذاتية")]
        public string? CvUrl { get; set; }

        // We can use the User model instead, but by using UserVm, we take advantage of the data annotations of UserVm.
        public UserVm User { get; set; }
    }
}