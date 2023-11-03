using System;
using System.Collections.Generic;
using System.Text;

namespace PLCore.DTOs.General
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string CourseTitle { get; set; }
        public int Count { get; set; }
        public bool CourseState { get; set; }
        public string CourseStateMessage { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
