using CommonLayer.OrderModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Interfaces
{
    public interface IOrderBL
    {
        public OrderResponse OrderPlaced(long BookId, OrderModel model, long UserId);
        public List<OrderResponse> GetAllOrder(long UserId);
    }
}
