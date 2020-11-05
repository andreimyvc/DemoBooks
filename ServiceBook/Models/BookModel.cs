using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceBook.Models
{
    public class BookModel
    {
        public BookModel() { }
        public BookModel(int id, string title, string description, int pageCount, string excerpt, DateTime publishDate)
        {
            this.ID = id;
            this.Title = title;
            this.Description = description;
            this.PageCount = pageCount;
            this.Excerpt = excerpt;
            this.PublishDate = publishDate;
        }

        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PageCount { get; set; }
        public string Excerpt { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
