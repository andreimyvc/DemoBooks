using DemoBooks.Models;
using DemoBooks.Utils;
using ServiceBook;
using ServiceBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DemoBooks.Controllers.Api
{
    public class BooksController : ApiController
    {
        //private IServiceBookManager service = new MemoryFakeServiceBookManager();
        private IServiceBookManager service = new FakeServiceBookManager();
        // GET: api/Book
        public HttpResponseMessage Get()
        {
            try
            {
                var lista = service.Get();
                var result = new List<BookViewModel>();
                if (lista != null)
                {
                    foreach (var item in lista)
                    {
                        result.Add(item.MapOrDefault());
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                HttpError err = new HttpError(ex.Message);
                return Request.CreateResponse(HttpStatusCode.Conflict, err);
            }
        }

        // GET: api/Book/5
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var data = service.Get(id);
                var result = data.MapOrDefault();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                HttpError err = new HttpError(ex.Message);
                return Request.CreateResponse(HttpStatusCode.Conflict, err);
            }
        }


        // POST: api/Book
        [HttpPost]
        public HttpResponseMessage Post(BookViewModel entity)
        {
            try
            {
                var model = entity.MapOrDefault();
                var result = service.CreateOrUpdate(model);
                return Request.CreateResponse(HttpStatusCode.Created, result);
            }
            catch (Exception ex)
            {
                HttpError err = new HttpError(ex.Message);
                return Request.CreateResponse(HttpStatusCode.Conflict, err);
            }
        }

        // PUT: api/Book/5
        [HttpPut]
        public HttpResponseMessage Put(BookViewModel entity)
        {
            var model = entity.MapOrDefault();
            try
            {
                var result = service.CreateOrUpdate(model);
                return Request.CreateResponse(HttpStatusCode.Created, result);
            }
            catch (Exception ex)
            {
                HttpError err = new HttpError(ex.Message);
                return Request.CreateResponse(HttpStatusCode.Conflict, err);
            }
        }

        //// PUT: api/Book/5
        //[HttpPut]
        //public HttpResponseMessage Put([FromBody] int id, [FromBody]string title, string description, string excerpt, DateTime publishDate, int pageCount)
        //{
        //    var model = new BookModel(id, title, description, pageCount, excerpt, publishDate);
        //    try
        //    {
        //        int result = service.CreateOrUpdate(model);
        //        return Request.CreateResponse(HttpStatusCode.Created);
        //    }
        //    catch (Exception ex)
        //    {
        //        HttpError err = new HttpError(ex.Message);
        //        return Request.CreateResponse(HttpStatusCode.Conflict, err);
        //    }
        //}

        // DELETE: api/Book/5

        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var old = service.Get(id);
                if (old == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, new HttpError("No se encontró el registro"));
                }
                var result = service.Delete(id);
                return Request.CreateResponse<bool>(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                HttpError err = new HttpError(ex.Message);
                return Request.CreateResponse(HttpStatusCode.Conflict, err);
            }
        }
    }
}