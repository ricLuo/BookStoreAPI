using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Models.Common;

namespace BookStore.Models
{
    public class Book : AuditableEntity
    {
        [StringLength(50, MinimumLength = 3)]
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public decimal Price { get; set; }
        public int Copies { get; set; }
        public string Edition { get; set; }
        public string Isbn { get; set; }
        public string Url { get; set; }
        public Author Author { get; set; }
        public int AuthorId { get; set; }
        public DateTime PublishedDate { get; set; }

        public ICollection<Category> Categories { get; set; }
    }
}