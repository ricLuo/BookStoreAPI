using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Models.Common;

namespace BookStore.Models
{
   public class Order: AuditableEntity
    {
        public Guid OrderGuid { get; set; }
        public string CustomerId { get; set; }
        public int OrderStatusId { get; set; }
        public decimal OrderTotal { get; set; }
        public DateTime OrderCreatedDate { get; set; }

        private ICollection<OrderItem> _orderItems;

        [ForeignKey("CustomerId")]
        public virtual ApplicationUser Customer { get; set; }

        public OrderStatus OrderStatus
        {
            get => (OrderStatus)this.OrderStatusId;
            set => OrderStatusId = (int)value;
        }

        public virtual ICollection<OrderItem> OrderItems
        {
            get { return _orderItems ?? (_orderItems = new List<OrderItem>()); }
            protected set { _orderItems = value; }
        }


    }

    public enum OrderStatus
    {
        /// <summary>
        /// Pending
        /// </summary>
        Pending = 10,
        /// <summary>
        /// Processing
        /// </summary>
        Processing = 20,
        /// <summary>
        /// Complete
        /// </summary>
        Complete = 30,
        /// <summary>
        /// Cancelled
        /// </summary>
        Cancelled = 40
    }

    /// <summary>
    /// Represents a payment status enumeration
    /// </summary>
    public enum PaymentStatus
    {
        /// <summary>
        /// Pending
        /// </summary>
        Pending = 10,
        /// <summary>
        /// Authorized
        /// </summary>
        Authorized = 20,
        /// <summary>
        /// Paid
        /// </summary>
        Paid = 30,
        /// <summary>
        /// Partially Refunded
        /// </summary>
        PartiallyRefunded = 35,
        /// <summary>
        /// Refunded
        /// </summary>
        Refunded = 40,
        /// <summary>
        /// Voided
        /// </summary>
        Voided = 50,
    }
}
