using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace NetWorthCalculator.Models
{
    public partial class NetWorthContext : DbContext
    {
        public IConfiguration Configuration { get; }
        public NetWorthContext()
        {
        }

        public NetWorthContext(DbContextOptions<NetWorthContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }

        public virtual DbSet<NetWorth> NetWorths { get; set; }
        public virtual DbSet<NetWorthItem> NetWorthItems { get; set; }
        public virtual DbSet<NetWorthItemResult> NetWorthItemResults { get; set; }
        public virtual DbSet<User> Users { get; set; }
       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<NetWorth>(entity =>
            {
                entity.ToTable("NetWorth");

                entity.Property(e => e.Advice)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("date");
                
                entity.Property(e => e.UserId)
               .IsRequired()
               .HasMaxLength(10)
               .IsUnicode(false);
            });

            modelBuilder.Entity<NetWorthItem>(entity =>
            {
                entity.ToTable("NetWorthItem");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<NetWorthItemResult>(entity =>
            {
                entity.ToTable("NetWorthItemResult");

                entity.Property(e => e.Value).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.ItemNavigation)
                    .WithMany(p => p.NetWorthItemResults)
                    .HasForeignKey(d => d.Item)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NetWorthItemResult_NetWorthItem");

                entity.HasOne(d => d.NetWorth)
                    .WithMany(p => p.NetWorthItemResults)
                    .HasForeignKey(d => d.NetWorthId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NetWorthItemResult_NetWorth");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PictureUrl)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('https://static.vecteezy.com/system/resources/thumbnails/010/260/479/small/default-avatar-profile-icon-of-social-media-user-in-clipart-style-vector.jpg')");

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('User')");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
