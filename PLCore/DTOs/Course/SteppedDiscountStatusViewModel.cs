using System;
using System.Collections.Generic;
using System.Text;

namespace PLCore.DTOs.Course
{
    public class SteppedDiscountStatusViewModel
    {
        /// <summary>
        /// شرح وضعیت
        /// </summary>
        public string StatusMessage { get; set; }
        /// <summary>
        /// فعال/غیرفعال(اتمام)
        /// </summary>
        public string StatusAbstract { get; set; }
        /// <summary>
        /// درصد تخفیف
        /// </summary>
        public float DisPercent { get; set; }
        /// <summary>
        /// مبلغ وضعیت
        /// </summary>
        public int StatusFee { get; set; }
        /// <summary>
        /// وضعیت فعلی ردیف تخفیف
        /// </summary>

        public bool IsActive { get; set; }

    }
}
