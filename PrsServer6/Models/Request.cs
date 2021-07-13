using Microsoft.EntityFrameworkCore.Metadata.Internal;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrsServer6.Models {
    public class Request {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Justification { get; set; }
        public string RejectionReason { get; set; }
        public string DeliveryMode { get; set; } = "Pickup";
        public string Status { get; set; } = StatusNew;
        public decimal Total { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public virtual IEnumerable<Requestline> Requestlines { get; set; }

        public static string StatusNew = "NEW";
        public static string StatusEdit = "EDIT";
        public static string StatusReview = "REVIEW";
        public static string StatusApproved = "APPROVED";
        public static string StatusRejected = "REJECTED";
    }
}
