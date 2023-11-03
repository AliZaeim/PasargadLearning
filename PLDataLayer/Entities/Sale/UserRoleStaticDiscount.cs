
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using PLDataLayer.Entities.User;

namespace PLDataLayer.Entities.Sale
{
    public class UserRoleStaticDiscount
    {
        [Key]
        public int URSD_Id { get; set; }
        public int URId { get; set; }
        public int SD_Id { get; set; }
        #region Relations
        [ForeignKey("URId")]
        public UserRole UserRole { get; set; }
        [ForeignKey("SD_Id")]
        public StaticDiscount StaticDiscount { get; set; }
        #endregion
    }
}
