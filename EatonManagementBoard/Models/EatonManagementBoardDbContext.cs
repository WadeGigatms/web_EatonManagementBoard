using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace EatonManagementBoard.Models
{
    public partial class EatonManagementBoardDbContext : DbContext
    {
        public EatonManagementBoardDbContext()
        {
        }

        public EatonManagementBoardDbContext(DbContextOptions<EatonManagementBoardDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EatonEpc> EatonEpcs { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese_Taiwan_Stroke_CI_AS");

            modelBuilder.Entity<EatonEpc>(entity =>
            {
                entity.HasKey(e => e.Sid);

                entity.ToTable("eaton_epc");

                entity.Property(e => e.Epc)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("epc");

                entity.Property(e => e.ReaderId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("readerId");

                entity.Property(e => e.TransTime)
                    .HasColumnType("datetime")
                    .HasColumnName("transTime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
