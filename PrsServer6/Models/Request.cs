using Microsoft.EntityFrameworkCore.Metadata.Internal;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrsServer6.Models {
    public record Request (
        int Id,
        string Description,
        string Justification,
        string RejectionReason,
        string DeliveryMode = "Pickup",
        string Status = Request.StatusNew,
        decimal Total = 0,
        int UserId = 0
    ) {
        public virtual User User { get; set; }

        public virtual IEnumerable<Requestline> Requestlines { get; set; }

        public const string StatusNew = "NEW";
        public const string StatusEdit = "EDIT";
        public const string StatusReview = "REVIEW";
        public const string StatusApproved = "APPROVED";
        public const string StatusRejected = "REJECTED";
    }
}
