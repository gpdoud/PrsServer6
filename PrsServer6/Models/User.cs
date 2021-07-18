using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrsServer6.Models {
    public record User(
        int Id, string Username, string Password, string Firstname, string Lastname,
        string Phone, string Email, bool IsReviewer, bool IsAdmin
    ) { 
        public virtual IEnumerable<Request> Requests { get; set; }
    }
}
