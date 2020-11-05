using DemoBooks.Models;
using ServiceBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoBooks.Utils
{
    public static class Extensions
    {
        public static BookModel MapOrDefault(this BookViewModel obj)
        {
            if (obj == null) { throw new ApplicationException("Objeto nulo"); }

            return new BookModel
            {
                ID = obj.ID,
                Title = obj.Title,
                Description = obj.Description,
                Excerpt = obj.Excerpt,
                PageCount = obj.PageCount,
                PublishDate = obj.PublishDate
            };
        }

        public static BookViewModel MapOrDefault(this BookModel obj)
        {
            if (obj == null) { return null;  }

            return new BookViewModel
            {
                ID = obj.ID,
                Title = obj.Title,
                Description = obj.Description,
                Excerpt = obj.Excerpt,
                PageCount = obj.PageCount,
                PublishDate = obj.PublishDate
            };
        }
    }
}