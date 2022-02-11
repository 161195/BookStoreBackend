using CommonLayer.AddressModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Interfaces
{
    public interface IAddressBL
    {
        public AddressResponse AddressAdding(long TypeId, AddressModel model, long UserId);
        public List<AddressResponse> GetAddress(long UserId);
        public AddressUpdateResponse AddressEdit(long AddressId, UpdateModel model, long UserId);
        public bool DeletetWithWAddressId(long AddressId, long UserId);
    }
}
