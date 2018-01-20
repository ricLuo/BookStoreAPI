using System;
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
    public class UnitTest1
    {
        private Random gen = new Random();

        [TestMethod]
        public void TestMethod1()
        {
        }

        [TestMethod]
        public void GenerateRandomIdentityData()
        {
            UserRepository repository = new UserRepository(BookStoreDbContext.Create(),
                new ApplicationUserManager(new UserStore<ApplicationUser>(new BookStoreDbContext())),
                new ApplicationRoleManager(new RoleStore<IdentityRole>(new BookStoreDbContext())));

            var personGenerator = new PersonNameGenerator();
            var randomNames = personGenerator.GenerateMultipleFirstAndLastNames(15);


            foreach (var n in randomNames)
            {
                var names = n.Split(' ');
                var user = new ApplicationUser()
                {
                    UserName = names[0] + names[1] + "@gmail.com",
                    Email = names[0] + names[1] + "@gmail.com",
                    FirstName = names[0],
                    LastName = names[1],
                    DateOfBirth = RandomDay()
                };
                var addUserResult = repository.CreatUserAsync(user, "Reddy57!").Result;
            }

            Assert.AreEqual(0, 0);
        }

        DateTime RandomDay()
        {
            DateTime start = new DateTime(1947, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
        }
    }
}