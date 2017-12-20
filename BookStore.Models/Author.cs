using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BookStore.Models.Common;
using Newtonsoft.Json;

namespace BookStore.Models
{
    public class Author : AuditableEntity
    {
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [JsonIgnore]
        public string WebSite { get; set; }

        [JsonIgnore]
        public ICollection<Book> Books { get; set; }
    }
}