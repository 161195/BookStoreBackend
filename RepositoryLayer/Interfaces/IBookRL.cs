using CommonLayer;
using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IBookRL
    {
        public BookResponse AddBook(InsertBookDetails bookDetails, long UserId);
    }
}
