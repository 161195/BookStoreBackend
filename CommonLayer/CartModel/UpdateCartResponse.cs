﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.CartModel
{
    public class UpdateCartResponse
    {
        public long BookId { get; set; }
        public long Quantity { get; set; }
        public long UserId { get; set; }

    }
}
