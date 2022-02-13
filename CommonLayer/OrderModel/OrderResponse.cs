using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.OrderModel
{
    public class OrderResponse
    {
        public long BookId { get; set; }
        public long AddressId { get; set; }        
        public long Price { get; set; }
        public long Quantity { get; set; }
        public long UserId { get; set; }
    }
}
