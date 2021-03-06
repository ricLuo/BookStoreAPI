﻿using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using BookStore.Models;
using BookStore.Models.Common;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BookStore.Data.Common
{ 
    public class BookStoreDbContext: IdentityDbContext<ApplicationUser>
    {
        public BookStoreDbContext(): base("Name=BookStoreDbContext", throwIfV1Schema: false)
        {
            Configuration.LazyLoadingEnabled = false;

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }




        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .ToTable("Users");

            modelBuilder.Entity<IdentityRole>()
                .ToTable("Roles");

            modelBuilder.Entity<IdentityUserRole>()
                .ToTable("UserRoles");

            modelBuilder.Entity<IdentityUserClaim>()
                .ToTable("UserClaims");

            modelBuilder.Entity<IdentityUserLogin>()
                .ToTable("UserLogins");
        }

        public static BookStoreDbContext Create()
        {
            return new BookStoreDbContext();
        }
        public override int SaveChanges()
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is IAuditableEntity
                            &&
                            (x.State == System.Data.Entity.EntityState.Added ||
                             x.State == System.Data.Entity.EntityState.Modified));

            foreach (var entry in modifiedEntries)
            {
                var entity = entry.Entity as IAuditableEntity;
                if (entity != null)
                {
                    var currentClaims = (Thread.CurrentPrincipal as ClaimsPrincipal)?.Identities.FirstOrDefault()?.Claims;
                    if (currentClaims != null)
                    {
                        string identityName =
                            currentClaims.FirstOrDefault(c => c.Type == "userName")
                            ?.Value;
                    
                        DateTime now = DateTime.Now;

                        if (entry.State == System.Data.Entity.EntityState.Added)
                        {
                            entity.CreatedBy = identityName;
                            entity.CreatedDate = now;
                        }
                        else
                        {
                            base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                            base.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                        }

                        entity.UpdatedBy = identityName;
                        entity.UpdatedDate = now;
                    }
                }
            }

            return base.SaveChanges();
        }
    }
}