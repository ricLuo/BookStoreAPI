using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Models.Common;

namespace BookStore.Models
{
    public class OrderItem: AuditableEntity
    {
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public virtual Order Order { get; set; }
        public virtual Book Book { get; set; }

    }
}
