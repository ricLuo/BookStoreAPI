using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BookStore.Models.Common;

namespace BookStore.Models
{
   public class Author: AuditableEntity
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        public string WebSite { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
