using System.Collections.Generic;
using BookStore.Models.Common;
using Newtonsoft.Json;

namespace BookStore.Models
{
    public class Category : AuditableEntity
    {
        public string Name { get; set; }

        [JsonIgnore]
        public string Description { get; set; }

        [JsonIgnore]
        public ICollection<Book> Books { get; set; }
    }
}