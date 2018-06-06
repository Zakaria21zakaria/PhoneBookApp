using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhoneBookApp.Controllers;
using PhoneBookApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace PhoneBookApp.Tests.Controllers
{
    [TestClass]
    public class PhoneBooksControllerTest
    {
        [TestMethod]
        public void CreatePhoneBook_save_phonebook_via_context_Test()
        {
            var mockSet = new Mock<DbSet<PhoneBook>>();

            var mockContext = new Mock<PhoneBookDBContext>();
            mockContext.Setup(m => m.PhoneBook).Returns(mockSet.Object);

            PhoneBooksController controller =new PhoneBooksController(mockContext.Object);
            controller.Create(new PhoneBook() { ID = 1, Name = "Test Book", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now });

            mockSet.Verify(m => m.Add(It.IsAny<PhoneBook>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        
        [TestMethod]
        public void GetAllPhoneBooks_Test()
        {
            var data = new List<PhoneBook>
            {
               new PhoneBook { ID = 1, Name = "Test Book 1", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now },
               new PhoneBook { ID = 2, Name = "Test Book 2", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now }

            }.AsQueryable();

            var mockSet = new Mock<DbSet<PhoneBook>>();
            mockSet.As<IQueryable<PhoneBook>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<PhoneBook>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<PhoneBook>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<PhoneBook>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<PhoneBookDBContext>();
            mockContext.Setup(c => c.PhoneBook).Returns(mockSet.Object);


            PhoneBooksController controller = new PhoneBooksController(mockContext.Object);
            //  var service = new BlogService(mockContext.Object);
            var result = controller.Index() as ViewResult;
            var phoneBooks = (List<PhoneBook>)  result.ViewData.Model;
            
            Assert.AreEqual(2, phoneBooks.Count);
            Assert.AreEqual("Test Book 1", phoneBooks[0].Name);
            Assert.AreEqual("Test Book 2", phoneBooks[1].Name);
          
        }


        [TestMethod]
        public void GetPhoneBookDetails_givenID_Test()
        {
            var data = new List<PhoneBook>
            {
               new PhoneBook { ID = 1, Name = "Test Book 1", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now },
               new PhoneBook { ID = 2, Name = "Test Book 2", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now }

            }.AsQueryable();

            int testid = 2;
            var mockSet = new Mock<DbSet<PhoneBook>>();
            mockSet.As<IQueryable<PhoneBook>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<PhoneBook>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<PhoneBook>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<PhoneBook>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());



            mockSet.Setup(m => m.Find(It.IsAny<int>())).Returns(data.Where(x=> x.ID == testid).FirstOrDefault);
            var mockContext = new Mock<PhoneBookDBContext>();
            mockContext.Setup(c => c.PhoneBook).Returns(mockSet.Object);


          
            PhoneBooksController controller = new PhoneBooksController(mockContext.Object);

            var resultTest = controller.Details(testid) as ViewResult;
    
            var phoneBook = resultTest.ViewData.Model as PhoneBook;

            
            Assert.AreEqual("Test Book 2", phoneBook.Name);
            

        }



    }
}
