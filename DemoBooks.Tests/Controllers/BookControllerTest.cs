using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DemoBooks;
using DemoBooks.Controllers;
using ServiceBook;
using ServiceBook.Models;

namespace DemoBooks.Tests.Controllers
{
    [TestClass]
    public class BookControllerTest
    {
        [TestMethod]
        public void Get()
        {
            // Arrange
            IServiceBookManager manager = new FakeServiceBookManager();

            // Act
            var result = manager.Get();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetById()
        {
            // Arrange
            IServiceBookManager manager = new MemoryFakeServiceBookManager();

            // Act
            var result = manager.Get(1);

            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void Post()
        {
            // Arrange
            IServiceBookManager manager = new MemoryFakeServiceBookManager();

            var obj = new BookModel
            {
                ID = 1,
                Title = "Arroz con Mangos",
                Description = "Arroz con Mangos",
                Excerpt = "Arroz con Mangos",
                PageCount = 500,
                PublishDate = new System.DateTime(2020, 11, 05)
            };
            // Act
            var result = manager.CreateOrUpdate(obj);

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
