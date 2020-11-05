using Newtonsoft.Json;
using ServiceBook.Models;
using ServiceBook.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace ServiceBook
{
    public class FakeServiceBookManager : IServiceBookManager
    {
        private List<BookModel> _lista = null;
        private string _url = "";
        private HttpClient client = null;

        public FakeServiceBookManager(string url = "https://fakerestapi.azurewebsites.net/")
        {
            this._url = url;
            client = new HttpClient
            {
                BaseAddress = new Uri(url)
            };

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/text"));         
        }
        public List<BookModel> Get()
        {
            var response = client.GetAsync(_url + "api/books").Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<BookModel>>(result);
                return data;
            }
            else
            {
                return null;
            }
        }
        public BookModel Get(int id)
        {
            var response = client.GetAsync(_url + $"api/books/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var data = Newtonsoft.Json.JsonConvert.DeserializeObject<BookModel>(result);
                return data;
            }
            else
            {
                return null;
            }
        }

        public BookModel CreateOrUpdate(BookModel entity)
        {
            if (entity == null)
            {
                throw new ApplicationException("Está enviando un objeto nulo");
            }
            if (entity.Title.IsNullOrEmptyOrWhiteSpace()) { throw new ApplicationException("Debe indicar el Título"); }
            if (entity.Description.IsNullOrEmptyOrWhiteSpace()) { throw new ApplicationException("Debe indicar el Descripción"); }
            if (entity.PublishDate == null || entity.PublishDate == default(DateTime)) { throw new ApplicationException("Debe indicar la fecha de publicación"); }

            if (entity.ID == 0)
            {
                var strdata = JsonConvert.SerializeObject(entity);
                var buffer = Encoding.UTF8.GetBytes(strdata);
                var byArray = new ByteArrayContent(buffer);


                byArray.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = client.PostAsync(_url + $"api/books", byArray).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<BookModel>(result);
                    return data;
                }
            }
            else
            {
                var objeto = new
                {
                    id = entity.ID,
                    book = entity
                };
                var strdata = JsonConvert.SerializeObject(entity);
                var buffer = Encoding.UTF8.GetBytes(strdata);
                var byArray = new ByteArrayContent(buffer);
                byArray.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = client.PutAsync(_url + $"api/books/{entity.ID}", byArray).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<BookModel>(result);
                    return data;
                }
            }
            return null;
        }

        public bool Delete(int id)
        {
            var response = client.DeleteAsync(_url + $"api/books/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var data = Newtonsoft.Json.JsonConvert.DeserializeObject(result);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
