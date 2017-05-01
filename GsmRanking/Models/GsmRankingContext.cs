using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GsmRanking.Models
{
    public partial class GsmRankingContext : DbContext
    {
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Phone> Phones { get; set; }
        public virtual DbSet<Producer> Producers { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlServer(@"Data Source=gsmranking.database.windows.net;Initial Catalog=GsmRanking;Integrated Security=False;User ID=gsmdbadmin;Password=1qa2ws#ED;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>(entity =>
            {
                entity.HasKey(e => e.IdArticle)
                    .HasName("PK__Articles__2CC641E4540FEC23");

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

                entity.HasOne(d => d.IdAutorNavigation)
                    .WithMany(p => p.Articles)
                    .HasForeignKey(d => d.IdAutor)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__Articles__IdAuto__7BE56230");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.IdComment)
                    .HasName("PK__Comments__57C9AD58DB2A5A3A");

                entity.Property(e => e.Content).HasColumnType("text");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.HasOne(d => d.IdArticleNavigation)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.IdArticle)
                    .HasConstraintName("FK__Comments__IdArti__7EC1CEDB");

                entity.HasOne(d => d.IdAutorNavigation)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.IdAutor)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__Comments__IdAuto__7CD98669");

                entity.HasOne(d => d.IdNewsNavigation)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.IdNews)
                    .HasConstraintName("FK__Comments__IdNews__7DCDAAA2");

                entity.HasOne(d => d.IdPhoneNavigation)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.IdPhone)
                    .HasConstraintName("FK__Comments__IdPhon__7FB5F314");
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

            modelBuilder.Entity<Phone>(entity =>
            {
                entity.HasKey(e => e.IdPhone)
                    .HasName("PK__Phones__7F4A3AF04891BE6F");

                entity.HasIndex(e => e.Model)
                    .HasName("Indeks_Model");

                entity.Property(e => e.A2dp)
                    .HasColumnName("A2DP")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Bt)
                    .HasColumnName("BT")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.BtInfo).HasColumnType("varchar(10)");

                entity.Property(e => e.Con)
                    .HasColumnName("CON")
                    .HasColumnType("varchar(40)");

                entity.Property(e => e.Cpu)
                    .HasColumnName("CPU")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Dlna)
                    .HasColumnName("DLNA")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.DualSim).HasDefaultValueSql("0");

                entity.Property(e => e.Edge)
                    .HasColumnName("EDGE")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.FastCharge).HasDefaultValueSql("0");

                entity.Property(e => e.FrontCamera).HasColumnType("varchar(100)");

                entity.Property(e => e.Gprs)
                    .HasColumnName("GPRS")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Gps)
                    .HasColumnName("GPS")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.HotSpotWifi).HasDefaultValueSql("1");

                entity.Property(e => e.Hsdpa)
                    .HasColumnName("HSDPA")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Hsdpaplus)
                    .HasColumnName("HSDPAPlus")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.InduCharge).HasDefaultValueSql("0");

                entity.Property(e => e.Ip68)
                    .HasColumnName("IP68")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Irda)
                    .HasColumnName("IRDA")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Kind).HasColumnType("varchar(20)");

                entity.Property(e => e.Lte)
                    .HasColumnName("LTE")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Ltedl).HasColumnName("LTEDL");

                entity.Property(e => e.Lteup).HasColumnName("LTEUP");

                entity.Property(e => e.Model).HasColumnType("varchar(20)");

                entity.Property(e => e.Nfc)
                    .HasColumnName("NFC")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.OsWork).HasColumnType("varchar(20)");

                entity.Property(e => e.PremierDate).HasColumnType("datetime");

                entity.Property(e => e.RearCamera).HasColumnType("varchar(100)");

                entity.Property(e => e.Screen).HasColumnType("varchar(45)");

                entity.Property(e => e.SdCard).HasDefaultValueSql("0");

                entity.Property(e => e.SdCardInfo).HasColumnType("varchar(10)");

                entity.Property(e => e.TouchScreen).HasDefaultValueSql("1");

                entity.Property(e => e.Wifi)
                    .HasColumnName("WIFI")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.WifiInfo).HasColumnType("varchar(40)");

                entity.HasOne(d => d.IdProducerNavigation)
                    .WithMany(p => p.Phones)
                    .HasForeignKey(d => d.IdProducer)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__Phones__IdProduc__7814D14C");
            });

            modelBuilder.Entity<Producer>(entity =>
            {
                entity.HasKey(e => e.IdProducer)
                    .HasName("PK__Producer__09880C664B2E059D");

                entity.Property(e => e.ProducerName).HasColumnType("varchar(40)");
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

                entity.Property(e => e.UserPassword)
                    .IsRequired()
                    .HasColumnType("varchar(64)");

                entity.Property(e => e.UserType).HasDefaultValueSql("0");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnType("varchar(40)");
            });
        }
    }
}