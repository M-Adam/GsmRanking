using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GsmRanking.Models
{
    public partial class GsmRankingContext : DbContext
    {
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Phones> Phones { get; set; }
        public virtual DbSet<Producers> Producers { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        public GsmRankingContext(DbContextOptions<GsmRankingContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<News>(entity =>
            {
                entity.HasKey(e => e.IdNews)
                    .HasName("PK__NEWS__A988CD5A261782A0");

                entity.ToTable("NEWS");

                entity.Property(e => e.IdNews).HasColumnName("ID_NEWS");

                entity.Property(e => e.Content)
                    .HasColumnName("CONTENT")
                    .HasColumnType("text");

                entity.Property(e => e.Createdate)
                    .HasColumnName("CREATEDATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdAutor).HasColumnName("ID_AUTOR");

                entity.Property(e => e.Ispublished)
                    .HasColumnName("ISPUBLISHED")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Publishdate)
                    .HasColumnName("PUBLISHDATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.Shorttext)
                    .HasColumnName("SHORTTEXT")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("TITLE")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Viewscount)
                    .HasColumnName("VIEWSCOUNT")
                    .HasDefaultValueSql("0");
            });

            modelBuilder.Entity<Phones>(entity =>
            {
                entity.HasKey(e => e.PhoneId)
                    .HasName("PK__PHONES__F3EE4BB0434079F0");

                entity.ToTable("PHONES");

                entity.HasIndex(e => e.Model)
                    .HasName("Indeks_Model");

                entity.Property(e => e.A2dp)
                    .HasColumnName("A2DP")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Batterycapacity).HasColumnName("BATTERYCAPACITY");

                entity.Property(e => e.Bt)
                    .HasColumnName("BT")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Btinfo)
                    .HasColumnName("BTINFO")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.Con)
                    .HasColumnName("CON")
                    .HasColumnType("varchar(40)");

                entity.Property(e => e.Cpu)
                    .HasColumnName("CPU")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Dlna)
                    .HasColumnName("DLNA")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Dualsim)
                    .HasColumnName("DUALSIM")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Edge)
                    .HasColumnName("EDGE")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Fastcharge)
                    .HasColumnName("FASTCHARGE")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Frontcamera)
                    .HasColumnName("FRONTCAMERA")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Gprs)
                    .HasColumnName("GPRS")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Gps)
                    .HasColumnName("GPS")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Hsdpa)
                    .HasColumnName("HSDPA")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Hsdpaplus)
                    .HasColumnName("HSDPAPlus")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Hswifi)
                    .HasColumnName("HSWIFI")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Inducharge)
                    .HasColumnName("INDUCHARGE")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Ip68)
                    .HasColumnName("IP68")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Irda)
                    .HasColumnName("IRDA")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Kind)
                    .HasColumnName("KIND")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Lte)
                    .HasColumnName("LTE")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Ltedl).HasColumnName("LTEDL");

                entity.Property(e => e.Lteup).HasColumnName("LTEUP");

                entity.Property(e => e.Memory).HasColumnName("MEMORY");

                entity.Property(e => e.Model)
                    .HasColumnName("MODEL")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Nfc)
                    .HasColumnName("NFC")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Oswork)
                    .HasColumnName("OSWORK")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Premierdate)
                    .HasColumnName("PREMIERDATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.Producerid).HasColumnName("PRODUCERID");

                entity.Property(e => e.Rearcamera)
                    .HasColumnName("REARCAMERA")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Screen)
                    .HasColumnName("SCREEN")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Sdcard)
                    .HasColumnName("SDCARD")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Sdcardinfo)
                    .HasColumnName("SDCARDINFO")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.Touchscreen)
                    .HasColumnName("TOUCHSCREEN")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Wifi)
                    .HasColumnName("WIFI")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Wifinfo)
                    .HasColumnName("WIFINFO")
                    .HasColumnType("varchar(40)");

                entity.HasOne(d => d.Producer)
                    .WithMany(p => p.Phones)
                    .HasForeignKey(d => d.Producerid)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__PHONES__PRODUCER__56E8E7AB");
            });

            modelBuilder.Entity<Producers>(entity =>
            {
                entity.HasKey(e => e.Producerid)
                    .HasName("PK__PRODUCER__8D6D9BD453BBF1F9");

                entity.ToTable("PRODUCERS");

                entity.Property(e => e.Producerid).HasColumnName("PRODUCERID");

                entity.Property(e => e.ProducerName)
                    .HasColumnName("PRODUCER_NAME")
                    .HasColumnType("varchar(40)");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.Userid)
                    .HasName("PK__USERS__7B9E7F35D84134F0");

                entity.ToTable("USERS");

                entity.Property(e => e.Userid)
                    .HasColumnName("USERID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("EMAIL")
                    .HasColumnType("varchar(60)");

                entity.Property(e => e.Firstname)
                    .HasColumnName("FIRSTNAME")
                    .HasColumnType("varchar(40)");

                entity.Property(e => e.Isadmin)
                    .HasColumnName("ISADMIN")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Lastname)
                    .HasColumnName("LASTNAME")
                    .HasColumnType("varchar(40)");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("USERNAME")
                    .HasColumnType("varchar(40)");

                entity.Property(e => e.Userpass)
                    .IsRequired()
                    .HasColumnName("USERPASS")
                    .HasColumnType("varchar(25)");
            });
        }
    }
}