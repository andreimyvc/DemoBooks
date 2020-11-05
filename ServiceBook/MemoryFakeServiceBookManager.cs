using ServiceBook.Models;
using ServiceBook.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceBook
{
    public class MemoryFakeServiceBookManager : IServiceBookManager
    {
        private readonly List<BookModel>  _lista = new List<BookModel>
            {
                new BookModel{ ID = 1, Title = "Libro 1", Description = "D Libro 1", Excerpt = "Excerpt 1", PageCount = 0, PublishDate = DateTime.Now},
                new BookModel{ ID = 2, Title = "Libro 2", Description = "D Libro 2", Excerpt = "Excerpt 2", PageCount = 0, PublishDate = DateTime.Now},
                new BookModel{ ID = 3, Title = "Libro 3", Description = "D Libro 3", Excerpt = "Excerpt 3", PageCount = 1, PublishDate = DateTime.Now},
                new BookModel{ ID = 4, Title = "Libro 4", Description = "D Libro 4", Excerpt = "Excerpt 4", PageCount = 2, PublishDate = DateTime.Now},
                new BookModel{ ID = 5, Title = "Libro 5", Description = "D Libro 5", Excerpt = "Excerpt 5", PageCount = 2, PublishDate = DateTime.Now}
            };
        public List<BookModel> Get()
        {
            return this._lista;
        }
        public BookModel Get(int id)
        {
            return this.Get().SingleOrDefault(p => p.ID == id);
        }

        public BookModel CreateOrUpdate(BookModel entity)
        {
            BookModel result = null;
            if (entity == null)
            {
                throw new ApplicationException("Está enviando un objeto nulo");
            }
            if (entity.Title.IsNullOrEmptyOrWhiteSpace()) { throw new ApplicationException("Debe indicar el Título"); }
            if (entity.Description.IsNullOrEmptyOrWhiteSpace()) { throw new ApplicationException("Debe indicar el Descripción"); }
            if (entity.PublishDate == null || entity.PublishDate == default(DateTime)) { throw new ApplicationException("Debe indicar la fecha de publicación"); }

            if (entity.ID == 0)
            {
                int id = this._lista.Select(p => p.ID).Max() + 1;
                entity.ID = id;
                this._lista.Add(entity);
                result = entity;
            }
            else
            {
                result = this._lista.SingleOrDefault(p => p.ID == entity.ID);
                if (result == null) { throw new ApplicationException("El registro que intenta modificar no existe"); }

                result.Title = entity.Title;
                result.Description = entity.Description;
                result.Excerpt = entity.Excerpt;
                result.PublishDate = entity.PublishDate;
            }
            return result;
        }

        public bool Delete(int id)
        {
            var old = this._lista.SingleOrDefault(p => p.ID == id);
            if (old == null) { throw new ApplicationException("El registro que intenta eliminar ya no existe"); }

            return this._lista.Remove(old);
        }
    }
}
