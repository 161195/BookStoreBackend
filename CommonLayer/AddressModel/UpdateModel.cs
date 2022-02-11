using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.AddressModel
{
    public class UpdateModel
    {
        public long TypeId { get; set; }
        public string FullName { get; set; }
        public string FullAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
