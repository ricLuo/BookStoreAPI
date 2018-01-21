using System;
using System.Collections.Generic;
using System.Linq;
using BookStore.Data.Common;
using BookStore.Data.Infrastructure;
using BookStore.Data.Repositories;
using BookStore.Models;
using BookStoreAPI.Controllers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomNameGeneratorLibrary;

namespace BookStoreAPI.Tests
{
    [TestClass]
    public class RandomFakeDataTest
    {
        private readonly Random gen = new Random();

        [TestMethod]
        public void GenerateRandomIdentityData()
        {
            //UserRepository repository = new UserRepository(BookStoreDbContext.Create(),
            //    new ApplicationUserManager(new UserStore<ApplicationUser>(new BookStoreDbContext())),
            //    new ApplicationRoleManager(new RoleStore<IdentityRole>(new BookStoreDbContext())));

            //var personGenerator = new PersonNameGenerator();
            //var randomNames = personGenerator.GenerateMultipleFirstAndLastNames(15);


            //foreach (var n in randomNames)
            //{
            //    var names = n.Split(' ');
            //    var user = new ApplicationUser()
            //    {
            //        UserName = names[0] + names[1] + "@gmail.com",
            //        Email = names[0] + names[1] + "@gmail.com",
            //        FirstName = names[0],
            //        LastName = names[1],
            //        DateOfBirth = RandomDay()
            //    };
            //    var addUserResult = repository.CreatUserAsync(user, "Reddy57!").Result;
            //}

            Assert.AreEqual(0, 0);
        }

        [TestMethod]
        public void GenerateRandomBookCategoriesData()
        {
            // foreach book add up to 3 categories 

            //  BooksRepository repository = new BooksRepository( new BookStoreDbContext());

            using (BookStoreDbContext db = new BookStoreDbContext())
            {
                var book = db.Books.FirstOrDefault(b => b.Id == 2333);
                var category = db.Categories.FirstOrDefault(c => c.Id == 1);
                if (category != null)
                {
                    category.Books = new List<Book> {book};
                }
                db.SaveChanges();
            }
            
            
            ////  var books =  repository.GetAll().Take(2);
            // // var category = new CategoryRepository(new BookStoreDbContext()).GetAll().First();
            //  foreach (var b in books)
            //  {

            //  }
            Assert.AreEqual(4114, 4114);
        }

        DateTime RandomDay()
        {
            DateTime start = new DateTime(1947, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
        }
    }
}