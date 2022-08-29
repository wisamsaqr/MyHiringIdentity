using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiring.Data.Enums
{
    public enum UserType
    {
        [Display(Name = "مدير النظام")]
        Admin = 1,
        
        [Display(Name = "مدير المؤسسة")]
        Foundation = 2,
        
        [Display(Name = "باحث عن عمل")]
        JobSeeker = 3
    }
}