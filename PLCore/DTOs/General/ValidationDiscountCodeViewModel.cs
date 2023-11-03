using System;
using System.Collections.Generic;
using System.Text;

namespace PLCore.DTOs.General
{
    public class ValidationDiscountCodeViewModel
    {
        public bool IsValid { get; set; }
        public string Message { get; set; }
        public string PrivateMessage { get; set; }
    }
}
