using System;
using System.Collections.Generic;
using System.Text;

namespace PLCore.DTOs.General
{
    public class ShoppingCart
    {
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
        public int RowSum { get; set; }

    }
}
