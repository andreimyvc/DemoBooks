using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoBooks.Models
{
    public class BookViewModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PageCount { get; set; }
        public string Excerpt { get; set; }
        public DateTime PublishDate { get; set; }
    }
}