using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrsServer6.Models {
    public record Requestline (
        int Id,
        int Quantity = 1,
        int RequestId = 0,
        int ProductId= 0
    ){
        public virtual Request Request { get; set; }
        public virtual Product Product { get; set; }
    }
}
