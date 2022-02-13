using BuisnessLayer.Interfaces;
using CommonLayer.OrderModel;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Services
{
    public class OrderBL : IOrderBL
    {
        IOrderRL OrderRL;
        public OrderBL(IOrderRL orderRL)
        {
            this.OrderRL = orderRL;
        }

        public OrderResponse OrderPlaced(long BookId, OrderModel model, long UserId)
        {
            try
            {
                return this.OrderRL.OrderPlaced(BookId, model, UserId);
            }
            catch (Exception ex)
            {
                throw;
            };
        }
        public List<OrderResponse> GetAllOrder(long UserId)
        {
            try
            {
                return this.OrderRL.GetAllOrder(UserId);
            }
            catch (Exception ex)
            {
                throw;
            };

        }

    }
}
