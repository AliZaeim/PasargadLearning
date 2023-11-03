using System;
using System.Collections.Generic;
using System.Text;

namespace PLCore.DTOs.Course
{
    public class CourseStatusResult
    {
        /// <summary>
        /// فعال/غیرفعال
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// مهلت ثبت نام دارد
        /// </summary>
        public bool HasRegTime { get; set; }
        /// <summary>
        /// ظرفیت ثبت نام دارد
        /// </summary>
        public bool HasCapicity { get; set; }
        /// <summary>
        /// رایگان/نقدی
        /// </summary>
        public bool IsFree { get; set; }
        /// <summary>
        /// دارای تخفیف پلکانی
        /// </summary>
        public bool HasSteppedDiscount { get; set; }
    }
}
