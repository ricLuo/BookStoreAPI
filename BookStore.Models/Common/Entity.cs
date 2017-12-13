using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.Common
{
    public abstract class Entity : IEntity
    {
        public virtual int Id { get; set; }
    }
}
