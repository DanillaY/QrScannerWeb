using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace copyqr.Models.Entities;

public partial class QrInfoContext : DbContext
{
    public QrInfoContext()
    {
    }

    public QrInfoContext(DbContextOptions<QrInfoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<QrInfo> QrInfos { get; set; }

    public virtual DbSet<User> Users { get; set; }

    private static string connectionString = "Host=localhost;Database=QrInfo;Username=postgres;Password=1337;";

    public static void SetConnectionString(string connection) => connectionString = connection;
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(connectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<QrInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_qr_info");

            entity.ToTable("qr_info");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Classroom).HasColumnName("classroom");
            entity.Property(e => e.ContentEncode).HasColumnName("content_encode");
            entity.Property(e => e.DateCreated)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_created");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.QrInfos)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_qr_user");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_pkey");

            entity.ToTable("user");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Hash).HasColumnName("hash");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
