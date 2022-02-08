using CommonLayer;
using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Interfaces
{
    public interface IBookBL
    {
        public BookResponse AddBook(InsertBookDetails bookDetails, long UserId);

    }
}
