using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PLDataLayer.Entities.Sale
{
    public class SteppedDiscount
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "کد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Code { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "نوع")]
        public int TypeId { get; set; }
        [Display(Name = "شرح")]
        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Comment { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public DateTime RegDate { get; set; }
        [Display(Name = "فعال/غیرفعال")]
        public bool IsActive { get; set; }
        #region Relations
        [ForeignKey(nameof(TypeId))]
        [Display(Name = "نوع")]
        public SteppedDiscountType SteppedDiscountType { get; set; }
        public ICollection<SteppedDiscountDetail> SteppedDiscountDetails { get; set; }
        #endregion
    }
}
