using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using PrsServer6.Models;

namespace PrsServer6.Models {
    public class PrsDbContext : DbContext {

        public DbSet<PrsServer6.Models.User> Users { get; set; }
        public DbSet<PrsServer6.Models.Vendor> Vendors { get; set; }
        public DbSet<PrsServer6.Models.Product> Products { get; set; }
        public DbSet<PrsServer6.Models.Request> Requests { get; set; }
        public DbSet<PrsServer6.Models.Requestline> Requestlines { get; set; }

        public PrsDbContext(DbContextOptions<PrsDbContext> options)
            : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder builder) {
            builder.Entity<User>(e => {
                e.HasKey(p => p.Id); //.HasAnnotation("SqlServer: Identity", "1, 1");
                e.Property(p => p.Username).HasMaxLength(30).IsRequired();
                e.Property(p => p.Password).HasMaxLength(30).IsRequired();
                e.Property(p => p.Firstname).HasMaxLength(30).IsRequired();
                e.Property(p => p.Lastname).HasMaxLength(30).IsRequired();
                e.Property(p => p.Phone).HasMaxLength(12).IsRequired(false);
                e.Property(p => p.Email).HasMaxLength(100).IsRequired(false);
                e.HasIndex(p => p.Username).IsUnique();
            });
            builder.Entity<Vendor>(e => {
                e.HasKey(p => p.Id);
                e.Property(p => p.Code).HasMaxLength(30).IsRequired();
                e.Property(p => p.Name).HasMaxLength(30).IsRequired();
                e.Property(p => p.Address).HasMaxLength(30).IsRequired();
                e.Property(p => p.City).HasMaxLength(30).IsRequired();
                e.Property(p => p.State).HasMaxLength(2).IsRequired();
                e.Property(p => p.Zip).HasMaxLength(5).IsRequired();
                e.Property(p => p.Phone).HasMaxLength(12).IsRequired(false);
                e.Property(p => p.Email).HasMaxLength(100).IsRequired(false);
                e.HasIndex(p => p.Code).IsUnique();
            });
            builder.Entity<Product>(e => {
                e.HasKey(p => p.Id);
                e.Property(p => p.PartNbr).HasMaxLength(30).IsRequired();
                e.Property(p => p.Name).HasMaxLength(30).IsRequired();
                e.Property(p => p.Price).HasColumnType("decimal(11,2)");
                e.Property(p => p.Unit).HasMaxLength(15).IsRequired(false);
                e.Property(p => p.PhotoPath).HasMaxLength(255).IsRequired(false);
                e.HasOne(p => p.Vendor).WithMany(v => v.Products).HasForeignKey(p => p.VendorId).OnDelete(DeleteBehavior.Restrict);
                e.HasIndex(p => p.PartNbr).IsUnique();
            });
            builder.Entity<Request>(e => {
                e.HasKey(p => p.Id);
                e.Property(p => p.Description).HasMaxLength(80).IsRequired();
                e.Property(p => p.Justification).HasMaxLength(80).IsRequired(false);
                e.Property(p => p.RejectionReason).HasMaxLength(80).IsRequired(false);
                e.Property(p => p.DeliveryMode).HasMaxLength(30).IsRequired();
                e.Property(p => p.Status).HasMaxLength(15).IsRequired();
                e.Property(p => p.Total).HasColumnType("decimal(11,2)");
                e.HasOne(p => p.User).WithMany(u => u.Requests).HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.Restrict);
                e.HasMany(p => p.Requestlines).WithOne(r => r.Request).HasForeignKey(p => p.RequestId).OnDelete(DeleteBehavior.Restrict);
            });
            builder.Entity<Requestline>(e => {
                e.HasKey(p => p.Id);
                e.Property(p => p.Quantity);
                e.HasOne(p => p.Request).WithMany(r => r.Requestlines).HasForeignKey(p => p.RequestId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(p => p.Product).WithMany(r => r.Requestlines).HasForeignKey(p => p.ProductId).OnDelete(DeleteBehavior.Restrict);
            });
        }

    }
}
