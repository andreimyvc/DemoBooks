using System.Collections.Generic;
using ServiceBook.Models;

namespace ServiceBook
{
    public interface IServiceBookManager
    {
        BookModel CreateOrUpdate(BookModel entity);
        bool Delete(int id);
        List<BookModel> Get();
        BookModel Get(int id);
    }
}