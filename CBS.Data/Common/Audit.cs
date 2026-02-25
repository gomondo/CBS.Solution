using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBS.Data.Common
{
    public class Audit
    {
        public string CreateBy { get; set; }

        public DateTime? CreatedOn { get; set; } 

        public string LastUpdateBy { get; set; }

        public DateTime? LastUpdatedOn { get; set; } 

        public string DeletedBy { get; set; }

        public DateTime? DeletedOn { get; set; } 

        public bool Deleted { get; set; } = false;
    }
}