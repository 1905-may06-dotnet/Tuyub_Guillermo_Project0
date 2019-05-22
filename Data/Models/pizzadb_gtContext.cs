using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Data.Models
{
    public partial class pizzadb_gtContext : DbContext
    {
        public pizzadb_gtContext()
        {
        }

        public pizzadb_gtContext(DbContextOptions<pizzadb_gtContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Crust> Crust { get; set; }
        public virtual DbSet<IndivPizza> IndivPizza { get; set; }
        public virtual DbSet<Ingredients> Ingredients { get; set; }
        public virtual DbSet<Orderpizza> Orderpizza { get; set; }
        public virtual DbSet<ResLocation> ResLocation { get; set; }
        public virtual DbSet<Size> Size { get; set; }
        public virtual DbSet<UserInfo> UserInfo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=gt-dbserver.database.windows.net;Database=pizzadb_gt;user id=guillermoT;Password=Kratos0909!;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Crust>(entity =>
            {
                entity.Property(e => e.CrustId).HasColumnName("crustID");

                entity.Property(e => e.Crust1)
                    .IsRequired()
                    .HasColumnName("Crust")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Totalcost)
                    .HasColumnName("totalcost")
                    .HasColumnType("decimal(5, 2)");
            });

            modelBuilder.Entity<IndivPizza>(entity =>
            {
                entity.HasKey(e => e.PizzaId)
                    .HasName("PK__indiv_pi__4D4C90CF355D3481");

                entity.ToTable("indiv_pizza");

                entity.Property(e => e.PizzaId).HasColumnName("pizzaID");

                entity.Property(e => e.CrustFid).HasColumnName("crustFID");

                entity.Property(e => e.Ingredient0Fid).HasColumnName("Ingredient0FID");

                entity.Property(e => e.Ingredient1Fid).HasColumnName("Ingredient1FID");

                entity.Property(e => e.Ingredient2Fid).HasColumnName("Ingredient2FID");

                entity.Property(e => e.Ingredient3Fid).HasColumnName("Ingredient3FID");

                entity.Property(e => e.Ingredient4Fid).HasColumnName("Ingredient4FID");

                entity.Property(e => e.OrderFid).HasColumnName("OrderFID");

                entity.Property(e => e.SizeFid).HasColumnName("sizeFID");

                entity.Property(e => e.Totalcost)
                    .HasColumnName("totalcost")
                    .HasColumnType("decimal(5, 2)");
            });

            modelBuilder.Entity<Ingredients>(entity =>
            {
                entity.HasKey(e => e.IngId)
                    .HasName("PK__Ingredie__12743785B56A58F2");

                entity.Property(e => e.IngId).HasColumnName("ingID");

                entity.Property(e => e.Cost)
                    .HasColumnName("cost")
                    .HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Ingredient)
                    .HasColumnName("ingredient")
                    .HasMaxLength(16)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Orderpizza>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__Orderpiz__0809337D96F69E88");

                entity.Property(e => e.OrderId).HasColumnName("orderID");

                entity.Property(e => e.LocationFid).HasColumnName("LocationFID");

                entity.Property(e => e.Timecheck)
                    .IsRequired()
                    .HasColumnName("timecheck")
                    .IsRowVersion();

                entity.Property(e => e.UserFid).HasColumnName("UserFID");
            });

            modelBuilder.Entity<ResLocation>(entity =>
            {
                entity.HasKey(e => e.LocationId)
                    .HasName("PK__ResLocat__30646B0E6EA5A8D2");

                entity.Property(e => e.LocationId).HasColumnName("locationID");

                entity.Property(e => e.City)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ResName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(2)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Size>(entity =>
            {
                entity.Property(e => e.SizeId).HasColumnName("sizeID");

                entity.Property(e => e.Size1)
                    .IsRequired()
                    .HasColumnName("Size")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Totalcost)
                    .HasColumnName("totalcost")
                    .HasColumnType("decimal(5, 2)");
            });

            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__UserInfo__CB9A1CDFBAAD93EB");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });
        }
    }
}
