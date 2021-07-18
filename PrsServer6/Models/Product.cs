using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrsServer6.Models {
    public record Product (

        int Id, string PartNbr, string Name,
        decimal Price, string Unit = "Each", string PhotoPath = null,
        int VendorId = 0

    ) {
        public virtual Vendor Vendor { get; set; }
        public virtual IEnumerable<Requestline> Requestlines { get; set; }
    }
}
