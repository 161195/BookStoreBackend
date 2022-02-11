using BuisnessLayer.Interfaces;
using CommonLayer.AddressModel;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Services
{
    public class AddressBL : IAddressBL
    {
        IAddressRL AddressRL;
        public AddressBL(IAddressRL addressRL)
        {
            this.AddressRL = addressRL;
        }

        public AddressResponse AddressAdding(long TypeId, AddressModel model, long UserId)
        {
            try
            {
                return this.AddressRL.AddressAdding(TypeId, model, UserId);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public List<AddressResponse> GetAddress(long UserId)
        {
            try
            {
                return this.AddressRL.GetAddress(UserId);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
