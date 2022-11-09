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
        public virtual DbSet<MonthlyVisualize> MonthlyVisualizes { get; set; }
        public virtual DbSet<PreparedTag> PreparedTags { get; set; }
        public virtual DbSet<RfidRssi> RfidRssis { get; set; }
        public virtual DbSet<RfidXiyuan> RfidXiyuans { get; set; }
        public virtual DbSet<RuDb> RuDbs { get; set; }
        public virtual DbSet<TaipeiLogistic> TaipeiLogistics { get; set; }
        public virtual DbSet<TextileProduct> TextileProducts { get; set; }
        public virtual DbSet<TextileWorkStatus> TextileWorkStatuses { get; set; }
        public virtual DbSet<VWorkStatus> VWorkStatuses { get; set; }

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

                entity.Property(e => e.Manual).HasColumnName("manual");

                entity.Property(e => e.ReaderId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("readerId");

                entity.Property(e => e.TransTime)
                    .HasColumnType("datetime")
                    .HasColumnName("transTime");
            });

            modelBuilder.Entity<MonthlyVisualize>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("monthlyVisualize");

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EmployeeID");

                entity.Property(e => e.EmployeeName).HasMaxLength(50);

                entity.Property(e => e.Kind).HasMaxLength(50);

                entity.Property(e => e.ShowName).HasMaxLength(50);

                entity.Property(e => e.WorkRole).HasMaxLength(50);

                entity.Property(e => e.YearMonth)
                    .HasMaxLength(6)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PreparedTag>(entity =>
            {
                entity.HasKey(e => e.ProjectId)
                    .HasName("PK__Prepared__761ABEF074CDC604");

                entity.ToTable("PreparedTag");

                entity.Property(e => e.ProjectId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.WriteTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<RfidRssi>(entity =>
            {
                entity.HasKey(e => e.Sid)
                    .HasName("PK__Rfid_RSS__CA1E5D7818DF944F");

                entity.ToTable("Rfid_RSSI");

                entity.Property(e => e.Antenna).HasColumnName("antenna");

                entity.Property(e => e.Epc)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("epc");

                entity.Property(e => e.ReaderId)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("readerId");

                entity.Property(e => e.Rssi).HasColumnName("rssi");

                entity.Property(e => e.TransTime)
                    .HasColumnType("datetime")
                    .HasColumnName("transTime");
            });

            modelBuilder.Entity<RfidXiyuan>(entity =>
            {
                entity.HasKey(e => e.Sid)
                    .HasName("PK__RFID_XIY__CA1E5D7835D5C0DC");

                entity.ToTable("RFID_XIYUAN");

                entity.Property(e => e.AntennaId).HasColumnName("antennaId");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("productName");

                entity.Property(e => e.ReadTime).HasColumnName("readTime");

                entity.Property(e => e.ReaderId)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("readerId");

                entity.Property(e => e.TransTime)
                    .HasColumnType("datetime")
                    .HasColumnName("transTime");
            });

            modelBuilder.Entity<RuDb>(entity =>
            {
                entity.HasKey(e => e.Sid)
                    .HasName("PK__ru_db__CA1E5D787D48E63F");

                entity.ToTable("ru_db");

                entity.Property(e => e.AntennaId)
                    .HasMaxLength(20)
                    .HasColumnName("antennaId");

                entity.Property(e => e.Bundle)
                    .HasMaxLength(50)
                    .HasColumnName("bundle");

                entity.Property(e => e.ReaderId)
                    .HasMaxLength(50)
                    .HasColumnName("readerId");

                entity.Property(e => e.Tid)
                    .HasMaxLength(50)
                    .HasColumnName("tid");

                entity.Property(e => e.TransTime)
                    .HasColumnType("datetime")
                    .HasColumnName("transTime");

                entity.Property(e => e.Usermemory)
                    .HasMaxLength(100)
                    .HasColumnName("usermemory");
            });

            modelBuilder.Entity<TaipeiLogistic>(entity =>
            {
                entity.HasKey(e => e.Sid);

                entity.ToTable("taipei_logistics");

                entity.Property(e => e.Epc)
                    .HasMaxLength(100)
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

            modelBuilder.Entity<TextileProduct>(entity =>
            {
                entity.HasKey(e => e.Sid)
                    .HasName("PK__textile___CA1E5D783C87AC9C");

                entity.ToTable("textile_product");

                entity.Property(e => e.Barcode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("barcode");

                entity.Property(e => e.Color)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("color");

                entity.Property(e => e.Epc)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("epc");

                entity.Property(e => e.PType)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("pType");

                entity.Property(e => e.SerialNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("serialNumber");

                entity.Property(e => e.Size)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("size");

                entity.Property(e => e.Vendor)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("vendor");
            });

            modelBuilder.Entity<TextileWorkStatus>(entity =>
            {
                entity.HasKey(e => e.Sid)
                    .HasName("PK__textile___CA1E5D7834548743");

                entity.ToTable("textile_work_status");

                entity.Property(e => e.Epc)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("epc");

                entity.Property(e => e.ReaderId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("readerId");

                entity.Property(e => e.Staff)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("staff");

                entity.Property(e => e.TransTime)
                    .HasColumnType("datetime")
                    .HasColumnName("transTime");
            });

            modelBuilder.Entity<VWorkStatus>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("vWorkStatus");

                entity.Property(e => e.BeginTime).HasColumnType("datetime");

                entity.Property(e => e.Color)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.DeliveryDate).HasColumnType("date");

                entity.Property(e => e.EmployeeName)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.ProjectId)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.ReaderType)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ScheduleBegin).HasColumnType("datetime");

                entity.Property(e => e.ScheduleEnd).HasColumnType("datetime");

                entity.Property(e => e.Sid).ValueGeneratedOnAdd();

                entity.Property(e => e.Size)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.StyleNo)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.WorkType)
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
