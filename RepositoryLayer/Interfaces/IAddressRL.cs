using CommonLayer.AddressModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IAddressRL
    {
        public AddressResponse AddressAdding(long TypeId, AddressModel model, long UserId);
        public List<AddressResponse> GetAddress(long UserId);
    }
}
