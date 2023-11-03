using PLDataLayer.Entities.Training;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PLDataLayer.Entities.Sale
{
    public class CourseStaticDiscount
    {
        [Key]
        public int CSD_Id { get; set; }
        public int Course_Id { get; set; }
        public int SD_Id { get; set; }
        #region Relations
        [ForeignKey("Course_Id")]
        public Course Course { get; set; }
        [ForeignKey("SD_Id")]
        public StaticDiscount StaticDiscount { get; set; }
        #endregion
    }
}
