using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PLDataLayer.Entities.Sale
{
    public class SteppedDiscountDetail
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "تخفیف پله ای")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int StId { get; set; }
        [Display(Name = "از نفر")]
        public int? FromPerson { get; set; }
        [Display(Name = "تا نفر")]
        public int? ToPerson { get; set; }
        [Display(Name = "از تاریخ")]
        public DateTime? FromDate { get; set; }
        [Display(Name = "تا تاریخ")]
        public DateTime? ToDate { get; set; }
        [Display(Name = "درصد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(0, 100, ErrorMessage = "عدد بین 0 و 100 باشد !")]
        public float Percent { get; set; }
        #region Relations
        [ForeignKey(nameof(StId))]
        [Display(Name = "تخفیف پله ای")]
        public SteppedDiscount SteppedDiscount { get; set; }
        #endregion
    }
}
