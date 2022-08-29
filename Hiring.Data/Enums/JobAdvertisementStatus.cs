using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiring.Data.Enums
{
    public enum JobAdvertisementStatus
    {
        [Display(Name = "قيد الانتظار")]
        Pending = 1,

        [Display(Name = "مقبول")]
        Approved= 2,
        
        [Display(Name = "مرفوض")]
        Rejected = 3
    }
}