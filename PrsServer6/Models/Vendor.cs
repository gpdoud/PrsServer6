using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrsServer6.Models {
    public record Vendor(

        int Id, string Code, string Name,
        string Address, string City, string State, string Zip,
        string Phone, string Email

    ) {
        public virtual IEnumerable<Product> Products { get; set; }
    }
}