using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BookStore.Models.Common
{
    public class AuditableEntity : Entity, IAuditableEntity
    {
        [ScaffoldColumn(false)]
        [JsonIgnore]
        public DateTime? CreatedDate { get; set; }

        [MaxLength(256)]
        [ScaffoldColumn(false)]
        [JsonIgnore]
        public string CreatedBy { get; set; }

        [ScaffoldColumn(false)]
        [JsonIgnore]
        public DateTime? UpdatedDate { get; set; }

        [MaxLength(256)]
        [ScaffoldColumn(false)]
        [JsonIgnore]
        public string UpdatedBy { get; set; }
    }
}