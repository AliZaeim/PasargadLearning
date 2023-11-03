
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PLDataLayer.Entities.Sale
{
    public class SteppedDiscountType
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Display(Name = "شرح")]
        public string Title { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Display(Name = "نام")]
        public string Name { get; set; }
        #region Relations
        public ICollection<SteppedDiscount> SteppedDiscounts { get; set; }
        #endregion
    }
}
