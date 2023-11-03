using PLDataLayer.Entities.Training;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PLCore.DTOs.General
{
    public class PrepareFeeViewModel
    {
        public int CourseId { get; set; }
        public PLDataLayer.Entities.Training.Course Course { get; set; }
        public int urId { get; set; }
        [Display(Name = "کد تخفیف")]
        public string DisCode { get; set; }
        [Display(Name = "مبلغ")]
        public int Fee { get; set; }
        public int DiscountValue { get; set; }
        public int TotalValue { get; set; }

        public bool IsValid { get; set; }
    }
}
