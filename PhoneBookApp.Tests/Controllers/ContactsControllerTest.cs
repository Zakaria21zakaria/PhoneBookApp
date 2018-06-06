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
    public class ContactsControllerTest
    {
        [TestMethod]
        public void CreateContact_save_contact_via_context_Test()
        {
            var mockSet = new Mock<DbSet<Contact>>();

            var mockContext = new Mock<PhoneBookDBContext>();
            mockContext.Setup(m => m.Contacts).Returns(mockSet.Object);

            var controller = new ContactsController(mockContext.Object);
            controller.Create(new Contact() { ID = 1, Name = "Fname SurName", PhoneNumber = "0743486820", PhoneBookID = 1});

            mockSet.Verify(m => m.Add(It.IsAny<Contact>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }


        [TestMethod]
        public void GetAllContacts_Test()
        {
            var data = new List<Contact>
            {
               new Contact() { ID = 1, Name = "Fname SurName", PhoneNumber = "0743400020", PhoneBookID = 1},
               new Contact() { ID = 2, Name = "Fname2 SurName2", PhoneNumber = "0743500820", PhoneBookID = 2}

            }.AsQueryable();

            var mockSet = new Mock<DbSet<Contact>>();
            mockSet.As<IQueryable<Contact>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Contact>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Contact>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Contact>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            mockSet.Setup(s => s.Include(It.IsAny<string>())).Returns(mockSet.Object);

            var mockContext = new Mock<PhoneBookDBContext>();
            mockContext.Setup(c => c.Contacts).Returns(mockSet.Object);


            var controller = new ContactsController(mockContext.Object);
            //  var service = new BlogService(mockContext.Object);
            var result = controller.Index(null) as ViewResult;
            var contacts = (List<Contact>)result.ViewData.Model;

            Assert.AreEqual(2, contacts.Count);
            Assert.AreEqual("Fname SurName", contacts[0].Name);
            Assert.AreEqual("Fname2 SurName2", contacts[1].Name);


        }

   }
}
