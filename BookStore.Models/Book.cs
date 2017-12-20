using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BookStore.Models.Common;

namespace BookStore.Models
{
    public class Book : AuditableEntity
    {
        [StringLength(5000, MinimumLength = 3)]
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public decimal? Price { get; set; }
        public int Copies { get; set; }
        public string Edition { get; set; }
        public string Isbn { get; set; }
        public string Url { get; set; }
        public Author Author { get; set; }
        public int AuthorId { get; set; }
        public DateTime PublishedDate { get; set; }
        public int? Pages { get; set; }
        public string PublishingCompany { get; set; }
        public double Rating { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}