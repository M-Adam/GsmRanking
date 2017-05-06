using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GsmRanking.Models
{
    public partial class GsmRankingContext : DbContext
    {
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlServer(@"Data Source=gsmranking.database.windows.net;Initial Catalog=GsmRanking;Integrated Security=False;User ID=gsmdbadmin;Password=1qa2ws#ED;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.IdComment)
                    .HasName("PK__Comments__57C9AD58DB2A5A3A");

                entity.Property(e => e.Content).HasColumnType("text");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.HasOne(d => d.IdAutorNavigation)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.IdAutor)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__Comments__IdAuto__0C1BC9F9");

                entity.HasOne(d => d.IdNewsNavigation)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.IdNews)
                    .HasConstraintName("FK__Comments__IdNews__7DCDAAA2");
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.HasKey(e => e.IdNews)
                    .HasName("PK__News__4559C72DE043E053");

                entity.Property(e => e.Content).HasColumnType("text");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Image).HasColumnType("varchar(max)");

                entity.Property(e => e.IsPublished).HasDefaultValueSql("0");

                entity.Property(e => e.PublishDate).HasColumnType("datetime");

                entity.Property(e => e.ShortText).HasColumnType("varchar(255)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.ViewsCount).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PK__tmp_ms_x__B7C926380507B4F3");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("varchar(60)");

                entity.Property(e => e.FirstName).HasColumnType("varchar(40)");

                entity.Property(e => e.LastName).HasColumnType("varchar(40)");

                entity.Property(e => e.UserPassword).HasColumnType("varchar(64)");

                entity.Property(e => e.UserType).HasDefaultValueSql("0");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnType("varchar(40)");
            });
        }
    }
}