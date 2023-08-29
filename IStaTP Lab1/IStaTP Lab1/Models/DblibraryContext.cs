using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace IStaTP_Lab1.Models;

public partial class DblibraryContext : DbContext
{
    public DblibraryContext()
    {
    }

    public DblibraryContext(DbContextOptions<DblibraryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<AuthorBook> AuthorBooks { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Reader> Readers { get; set; }

    public virtual DbSet<ReadersBook> ReadersBooks { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=ZENBOOK\\SQLEXPRESS; Database=DBLibrary; Trusted_Connection=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_AuthorsBooks");

            entity.Property(e => e.Info).HasColumnType("ntext");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<AuthorBook>(entity =>
        {
            entity.HasOne(d => d.Author).WithMany(p => p.AuthorBooks)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AuthorBooks_Authors");

            entity.HasOne(d => d.Book).WithMany(p => p.AuthorBooks)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AuthorBooks_Books");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.Property(e => e.Info).HasColumnType("ntext");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Category).WithMany(p => p.Books)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Books_Categories");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.Info).HasColumnType("text");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Reader>(entity =>
        {
            entity.Property(e => e.Address).HasColumnType("ntext");
            entity.Property(e => e.Info).HasColumnType("ntext");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<ReadersBook>(entity =>
        {
            entity.Property(e => e.FactReturn).HasColumnType("date");
            entity.Property(e => e.Issue).HasColumnType("date");
            entity.Property(e => e.PlanReturn).HasColumnType("date");

            entity.HasOne(d => d.Book).WithMany(p => p.ReadersBooks)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReadersBooks_Books");

            entity.HasOne(d => d.Reader).WithMany(p => p.ReadersBooks)
                .HasForeignKey(d => d.ReaderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReadersBooks_Readers");

            entity.HasOne(d => d.Status).WithMany(p => p.ReadersBooks)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReadersBooks_Statuses");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
