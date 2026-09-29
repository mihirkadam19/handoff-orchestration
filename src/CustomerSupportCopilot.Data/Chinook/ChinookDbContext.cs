using System;
using System.Collections.Generic;
using CustomerSupportCopilot.Data.Chinook.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerSupportCopilot.Data.Chinook;

public partial class ChinookDbContext : DbContext
{
    public ChinookDbContext(DbContextOptions<ChinookDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Album> Albums { get; set; }

    public virtual DbSet<Artist> Artists { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceLine> InvoiceLines { get; set; }

    public virtual DbSet<MediaType> MediaTypes { get; set; }

    public virtual DbSet<Track> Tracks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Album>(entity =>
        {
            entity.HasKey(e => e.AlbumId).HasName("pk_album");

            entity.ToTable("album");

            entity.HasIndex(e => e.ArtistId, "ifk_album_artist_id");

            entity.Property(e => e.AlbumId).HasColumnName("album_id");
            entity.Property(e => e.ArtistId).HasColumnName("artist_id");
            entity.Property(e => e.Title)
                .HasMaxLength(160)
                .HasColumnName("title");

            entity.HasOne(d => d.Artist).WithMany(p => p.Albums)
                .HasForeignKey(d => d.ArtistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_album_artist_id");
        });

        modelBuilder.Entity<Artist>(entity =>
        {
            entity.HasKey(e => e.ArtistId).HasName("pk_artist");

            entity.ToTable("artist");

            entity.Property(e => e.ArtistId).HasColumnName("artist_id");
            entity.Property(e => e.Name)
                .HasMaxLength(120)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("pk_customer");

            entity.ToTable("customer");

            entity.HasIndex(e => e.SupportRepId, "ifk_customer_support_rep_id");

            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Address)
                .HasMaxLength(70)
                .HasColumnName("address");
            entity.Property(e => e.City)
                .HasMaxLength(40)
                .HasColumnName("city");
            entity.Property(e => e.Company)
                .HasMaxLength(80)
                .HasColumnName("company");
            entity.Property(e => e.Country)
                .HasMaxLength(40)
                .HasColumnName("country");
            entity.Property(e => e.Email)
                .HasMaxLength(60)
                .HasColumnName("email");
            entity.Property(e => e.Fax)
                .HasMaxLength(24)
                .HasColumnName("fax");
            entity.Property(e => e.FirstName)
                .HasMaxLength(40)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(20)
                .HasColumnName("last_name");
            entity.Property(e => e.Phone)
                .HasMaxLength(24)
                .HasColumnName("phone");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(10)
                .HasColumnName("postal_code");
            entity.Property(e => e.State)
                .HasMaxLength(40)
                .HasColumnName("state");
            entity.Property(e => e.SupportRepId).HasColumnName("support_rep_id");

            entity.HasOne(d => d.SupportRep).WithMany(p => p.Customers)
                .HasForeignKey(d => d.SupportRepId)
                .HasConstraintName("fk_customer_support_rep_id");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("pk_employee");

            entity.ToTable("employee");

            entity.HasIndex(e => e.ReportsTo, "ifk_employee_reports_to");

            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.Address)
                .HasMaxLength(70)
                .HasColumnName("address");
            entity.Property(e => e.BirthDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("birth_date");
            entity.Property(e => e.City)
                .HasMaxLength(40)
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(40)
                .HasColumnName("country");
            entity.Property(e => e.Email)
                .HasMaxLength(60)
                .HasColumnName("email");
            entity.Property(e => e.Fax)
                .HasMaxLength(24)
                .HasColumnName("fax");
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .HasColumnName("first_name");
            entity.Property(e => e.HireDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("hire_date");
            entity.Property(e => e.LastName)
                .HasMaxLength(20)
                .HasColumnName("last_name");
            entity.Property(e => e.Phone)
                .HasMaxLength(24)
                .HasColumnName("phone");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(10)
                .HasColumnName("postal_code");
            entity.Property(e => e.ReportsTo).HasColumnName("reports_to");
            entity.Property(e => e.State)
                .HasMaxLength(40)
                .HasColumnName("state");
            entity.Property(e => e.Title)
                .HasMaxLength(30)
                .HasColumnName("title");

            entity.HasOne(d => d.ReportsToNavigation).WithMany(p => p.InverseReportsToNavigation)
                .HasForeignKey(d => d.ReportsTo)
                .HasConstraintName("fk_employee_reports_to");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("pk_genre");

            entity.ToTable("genre");

            entity.Property(e => e.GenreId).HasColumnName("genre_id");
            entity.Property(e => e.Name)
                .HasMaxLength(120)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("pk_invoice");

            entity.ToTable("invoice");

            entity.HasIndex(e => e.CustomerId, "ifk_invoice_customer_id");

            entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");
            entity.Property(e => e.BillingAddress)
                .HasMaxLength(70)
                .HasColumnName("billing_address");
            entity.Property(e => e.BillingCity)
                .HasMaxLength(40)
                .HasColumnName("billing_city");
            entity.Property(e => e.BillingCountry)
                .HasMaxLength(40)
                .HasColumnName("billing_country");
            entity.Property(e => e.BillingPostalCode)
                .HasMaxLength(10)
                .HasColumnName("billing_postal_code");
            entity.Property(e => e.BillingState)
                .HasMaxLength(40)
                .HasColumnName("billing_state");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.InvoiceDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("invoice_date");
            entity.Property(e => e.Total)
                .HasPrecision(10, 2)
                .HasColumnName("total");

            entity.HasOne(d => d.Customer).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_invoice_customer_id");
        });

        modelBuilder.Entity<InvoiceLine>(entity =>
        {
            entity.HasKey(e => e.InvoiceLineId).HasName("ok_invoice_line");

            entity.ToTable("invoice_line");

            entity.HasIndex(e => e.InvoiceId, "ifk_invoice_line_invoice_id");

            entity.HasIndex(e => e.TrackId, "ifk_invoice_line_track_id");

            entity.Property(e => e.InvoiceLineId)
                .HasDefaultValueSql("nextval('invoiceline_invoiceline_id_seq'::regclass)")
                .HasColumnName("invoice_line_id");
            entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.TrackId).HasColumnName("track_id");
            entity.Property(e => e.UnitPrice)
                .HasPrecision(10, 2)
                .HasColumnName("unit_price");

            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceLines)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_invoice_line_invoice_id");

            entity.HasOne(d => d.Track).WithMany(p => p.InvoiceLines)
                .HasForeignKey(d => d.TrackId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_invoice_line_track_id");
        });

        modelBuilder.Entity<MediaType>(entity =>
        {
            entity.HasKey(e => e.MediaTypeId).HasName("pk_media_type");

            entity.ToTable("media_type");

            entity.Property(e => e.MediaTypeId)
                .HasDefaultValueSql("nextval('mediatype_mediatype_id_seq'::regclass)")
                .HasColumnName("media_type_id");
            entity.Property(e => e.Name)
                .HasMaxLength(120)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Track>(entity =>
        {
            entity.HasKey(e => e.TrackId).HasName("pk_track");

            entity.ToTable("track");

            entity.HasIndex(e => e.AlbumId, "ifk_track_album_id");

            entity.HasIndex(e => e.GenreId, "ifk_track_genre_id");

            entity.HasIndex(e => e.MediaTypeId, "ifk_track_media_type_id");

            entity.Property(e => e.TrackId).HasColumnName("track_id");
            entity.Property(e => e.AlbumId).HasColumnName("album_id");
            entity.Property(e => e.Bytes).HasColumnName("bytes");
            entity.Property(e => e.Composer)
                .HasMaxLength(220)
                .HasColumnName("composer");
            entity.Property(e => e.GenreId).HasColumnName("genre_id");
            entity.Property(e => e.MediaTypeId).HasColumnName("media_type_id");
            entity.Property(e => e.Milliseconds).HasColumnName("milliseconds");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.UnitPrice)
                .HasPrecision(10, 2)
                .HasColumnName("unit_price");

            entity.HasOne(d => d.Album).WithMany(p => p.Tracks)
                .HasForeignKey(d => d.AlbumId)
                .HasConstraintName("fk_track_album_id");

            entity.HasOne(d => d.Genre).WithMany(p => p.Tracks)
                .HasForeignKey(d => d.GenreId)
                .HasConstraintName("fk_track_genre_id");

            entity.HasOne(d => d.MediaType).WithMany(p => p.Tracks)
                .HasForeignKey(d => d.MediaTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_track_media_type_id");
        });
        modelBuilder.HasSequence("actor_actor_id_seq")
            .StartsAt(203L)
            .HasMax(2147483647L);
        modelBuilder.HasSequence("album_album_id_seq")
            .StartsAt(347L)
            .HasMax(2147483647L);
        modelBuilder.HasSequence("artist_artist_id_seq")
            .StartsAt(275L)
            .HasMax(2147483647L);
        modelBuilder.HasSequence("category_category_id_seq")
            .StartsAt(16L)
            .HasMax(2147483647L);
        modelBuilder.HasSequence("customer_customer_id_seq")
            .StartsAt(59L)
            .HasMax(2147483647L);
        modelBuilder.HasSequence("employee_employee_id_seq")
            .StartsAt(8L)
            .HasMax(2147483647L);
        modelBuilder.HasSequence("film_film_id_seq")
            .StartsAt(1000L)
            .HasMax(2147483647L);
        modelBuilder.HasSequence("genre_genre_id_seq")
            .StartsAt(25L)
            .HasMax(2147483647L);
        modelBuilder.HasSequence("invoice_invoice_id_seq")
            .StartsAt(412L)
            .HasMax(2147483647L);
        modelBuilder.HasSequence("invoiceline_invoiceline_id_seq")
            .StartsAt(2240L)
            .HasMax(2147483647L);
        modelBuilder.HasSequence("mediatype_mediatype_id_seq")
            .StartsAt(5L)
            .HasMax(2147483647L);
        modelBuilder.HasSequence("playlist_playlist_id_seq")
            .StartsAt(18L)
            .HasMax(2147483647L);
        modelBuilder.HasSequence("track_track_id_seq")
            .StartsAt(3503L)
            .HasMax(2147483647L);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
