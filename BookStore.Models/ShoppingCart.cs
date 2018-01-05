using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Models.Common;

namespace BookStore.Models
{
   public class ShoppingCart : AuditableEntity
    {
        public string CustomerId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }

        public virtual Book Book { get; set; }

        [ForeignKey("CustomerId")]
        public virtual ApplicationUser Customer { get; set; }

    }
}
