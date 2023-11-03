using PLDataLayer.Entities.Sale;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PLCore.DTOs.Course
{
    public class SteppedDiscountDatailsViewModel
    {
        public int Id { get; set; }
        [Display(Name = "تخفیف پله ای")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? StId { get; set; }
        [Display(Name = "از نفر")]
        public int? FromPerson { get; set; }
        [Display(Name = "تا نفر")]
        public int? ToPerson { get; set; }
        [RegularExpression(@"^(\d{4})\/(0?[1-9]|1[012])\/(0?[1-9]|[12][0-9]|3[01])$", ErrorMessage = "تاریخ نامعتبر است !")]
        [Display(Name = "از تاریخ")]
        public string FromDate { get; set; }
        [RegularExpression(@"^(\d{4})\/(0?[1-9]|1[012])\/(0?[1-9]|[12][0-9]|3[01])$", ErrorMessage = "تاریخ نامعتبر است !")]
        [Display(Name = "تا تاریخ")]
        public string ToDate { get; set; }
        [Display(Name = "از زمان")]
        [RegularExpression("^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "زمان نامعتبر است !")]
        public string FromTime { get; set; }
        [RegularExpression("^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "زمان نامعتبر است !")]
        [Display(Name = "تا زمان")]
        public string ToTime { get; set; }
        [Display(Name = "درصد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(0, 100, ErrorMessage = "عدد باید در محدود 0 تا 100 باشد")]
        public float? Percent { get; set; }

        public string Code { get; set; }
        public SteppedDiscount SteppedDiscount { get; set; }
        public string type { get; set; }
        public List<SteppedDiscount> steppedDiscounts { get; set; }
    }
}
