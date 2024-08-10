using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WEBAPI.Entities;

public partial class ProjectContext : DbContext
{
    public ProjectContext()
    {
    }

    public ProjectContext(DbContextOptions<ProjectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Provider> Providers { get; set; }

    public virtual DbSet<ProviderSupport> ProviderSupports { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Subcategory> Subcategories { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserSupport> UserSupports { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PRIMARY");

            entity.ToTable("address");

            entity.HasIndex(e => e.UserId, "FKda8tuywtf0gb6sedwk7la1pgi");

            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.AddressType)
                .HasMaxLength(255)
                .HasColumnName("address_type");
            entity.Property(e => e.City)
                .HasMaxLength(255)
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(255)
                .HasColumnName("country");
            entity.Property(e => e.HouseNo).HasColumnName("house_no");
            entity.Property(e => e.Pincode).HasColumnName("pincode");
            entity.Property(e => e.State)
                .HasMaxLength(255)
                .HasColumnName("state");
            entity.Property(e => e.Street)
                .HasMaxLength(255)
                .HasColumnName("street");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FKda8tuywtf0gb6sedwk7la1pgi");
        });

        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("admin");

            entity.HasIndex(e => e.Email, "UK_c0r9atamxvbhjjvy5j8da1kam").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreationTime)
                .HasMaxLength(6)
                .HasColumnName("creation_time");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(25)
                .HasColumnName("first_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(255)
                .HasColumnName("gender");
            entity.Property(e => e.LastLoginTime)
                .HasMaxLength(6)
                .HasColumnName("last_login_time");
            entity.Property(e => e.LastName)
                .HasMaxLength(25)
                .HasColumnName("last_name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PRIMARY");

            entity.ToTable("bookings");

            entity.HasIndex(e => e.ProviderId, "FK125uern3l4kdyo6qm25n62i5x");

            entity.HasIndex(e => e.UserId, "FK65bh1tn1y443fxcah5u36e8fy");

            entity.HasIndex(e => e.SubcategoryId, "FKn92eu244xec4ukuan758hhuaq");

            entity.Property(e => e.BookingId).HasColumnName("booking_id");
            entity.Property(e => e.BookingDate)
                .HasColumnType("date")
                .HasColumnName("booking_date");
            entity.Property(e => e.BookingTime)
                .HasColumnType("time")
                .HasColumnName("booking_time");
            entity.Property(e => e.ProviderId).HasColumnName("provider_id");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .HasColumnName("status");
            entity.Property(e => e.SubcategoryId).HasColumnName("subcategory_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Provider).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.ProviderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK125uern3l4kdyo6qm25n62i5x");

            entity.HasOne(d => d.Subcategory).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.SubcategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKn92eu244xec4ukuan758hhuaq");

            entity.HasOne(d => d.User).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK65bh1tn1y443fxcah5u36e8fy");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("category");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("image_url");
            entity.Property(e => e.Name)
                .HasMaxLength(25)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PRIMARY");

            entity.ToTable("orders");

            entity.HasIndex(e => e.SubcategoryId, "FK432wi7oggu6l2mmdtemki9poh");

            entity.HasIndex(e => e.ProviderId, "FKdis6gbaiyohparbwlht36ka4x");

            entity.HasIndex(e => e.UserId, "FKel9kyl84ego2otj2accfd8mr7");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.OrderDate)
                .HasColumnType("date")
                .HasColumnName("order_date");
            entity.Property(e => e.OrderRate)
                .HasPrecision(10)
                .HasColumnName("order_rate");
            entity.Property(e => e.OrderTime)
                .HasColumnType("time")
                .HasColumnName("order_time");
            entity.Property(e => e.ProviderId).HasColumnName("provider_id");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .HasColumnName("status");
            entity.Property(e => e.SubcategoryId).HasColumnName("subcategory_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Provider).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ProviderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKdis6gbaiyohparbwlht36ka4x");

            entity.HasOne(d => d.Subcategory).WithMany(p => p.Orders)
                .HasForeignKey(d => d.SubcategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK432wi7oggu6l2mmdtemki9poh");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKel9kyl84ego2otj2accfd8mr7");
        });

        modelBuilder.Entity<Provider>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("provider");

            entity.HasIndex(e => e.Email, "UK_7pvp08p4hu0e5k4452khlhv78").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.City)
                .HasMaxLength(25)
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(25)
                .HasColumnName("country");
            entity.Property(e => e.CreationTime)
                .HasMaxLength(6)
                .HasColumnName("creation_time");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(25)
                .HasColumnName("first_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(255)
                .HasColumnName("gender");
            entity.Property(e => e.IsDeleted)
                .HasColumnType("bit(1)")
                .HasColumnName("is_deleted");
            entity.Property(e => e.LastLoginTime)
                .HasMaxLength(6)
                .HasColumnName("last_login_time");
            entity.Property(e => e.LastName)
                .HasMaxLength(25)
                .HasColumnName("last_name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");
            entity.Property(e => e.ProviderImagePath)
                .HasMaxLength(255)
                .HasColumnName("provider_image_path");
            entity.Property(e => e.ZipCode).HasColumnName("zip_code");
        });

        modelBuilder.Entity<ProviderSupport>(entity =>
        {
            entity.HasKey(e => e.SupportId).HasName("PRIMARY");

            entity.ToTable("provider_support");

            entity.HasIndex(e => e.BookingId, "FKhntkonem009rlvo9m7r8pm1iv");

            entity.HasIndex(e => e.ProviderId, "FKkvaosqp0bbsf5wpgni3qy5l10");

            entity.Property(e => e.SupportId).HasColumnName("support_id");
            entity.Property(e => e.BookingId).HasColumnName("booking_id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.ProviderId).HasColumnName("provider_id");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .HasColumnName("status");
            entity.Property(e => e.SupportType)
                .HasMaxLength(100)
                .HasColumnName("support_type");

            //entity.HasOne(d => d.Booking).WithMany(p => p.ProviderSupports)
            //    .HasForeignKey(d => d.BookingId)
            //    .HasConstraintName("FKhntkonem009rlvo9m7r8pm1iv");

            //entity.HasOne(d => d.Provider).WithMany(p => p.ProviderSupports)
            //    .HasForeignKey(d => d.ProviderId)
            //    .HasConstraintName("FKkvaosqp0bbsf5wpgni3qy5l10");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PRIMARY");

            entity.ToTable("reviews");

            entity.HasIndex(e => e.SubCategoryId, "FKpc6et4be9m4xlyfo8w76la7th");

            entity.HasIndex(e => e.OrderId, "FKqwgq1lxgahsxdspnwqfac6sv6");

            entity.HasIndex(e => e.UserId, "FKsdlcf7wf8l1k0m00gik0m6b1m");

            entity.Property(e => e.ReviewId).HasColumnName("review_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.Reivew).HasColumnName("reivew");
            entity.Property(e => e.ReviewDate)
                .HasColumnType("date")
                .HasColumnName("review_date");
            entity.Property(e => e.SubCategoryId).HasColumnName("sub_category_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Order).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FKqwgq1lxgahsxdspnwqfac6sv6");

            entity.HasOne(d => d.SubCategory).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.SubCategoryId)
                .HasConstraintName("FKpc6et4be9m4xlyfo8w76la7th");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FKsdlcf7wf8l1k0m00gik0m6b1m");
        });

        modelBuilder.Entity<Subcategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("subcategory");

            entity.HasIndex(e => e.CategoryId, "FKe4hdbsmrx9bs9gpj1fh4mg0ku");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(25)
                .HasColumnName("category_name");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .HasColumnName("image");
            entity.Property(e => e.IsVisible).HasColumnName("is_visible");
            entity.Property(e => e.LastUpdated)
                .HasColumnType("date")
                .HasColumnName("last_updated");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Rating).HasColumnName("rating");

            entity.HasOne(d => d.Category).WithMany(p => p.Subcategories)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKe4hdbsmrx9bs9gpj1fh4mg0ku");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.Email, "UK_ob8kqyqqgmefl0aco34akdtpe").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.CreationTime)
                .HasMaxLength(6)
                .HasColumnName("creation_time");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(25)
                .HasColumnName("first_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(255)
                .HasColumnName("gender");
            entity.Property(e => e.ImagePath)
                .HasMaxLength(255)
                .HasColumnName("image_path");
            entity.Property(e => e.IsDeleted)
                .HasColumnType("bit(1)")
                .HasColumnName("is_deleted");
            entity.Property(e => e.LastLoginTime)
                .HasMaxLength(6)
                .HasColumnName("last_login_time");
            entity.Property(e => e.LastName)
                .HasMaxLength(25)
                .HasColumnName("last_name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");
        });

        modelBuilder.Entity<UserSupport>(entity =>
        {
            entity.HasKey(e => e.SupportId).HasName("PRIMARY");

            entity.ToTable("user_support");

            entity.HasIndex(e => e.BookingId, "FKhe5yorsttr1s2pji8fk5k8575");

            entity.HasIndex(e => e.UserId, "FKkgerjwud4x9lcf2o7lgln01lq");

            entity.Property(e => e.SupportId).HasColumnName("support_id");
            entity.Property(e => e.BookingId).HasColumnName("booking_id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Booking).WithMany(p => p.UserSupports)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FKhe5yorsttr1s2pji8fk5k8575");

            entity.HasOne(d => d.User).WithMany(p => p.UserSupports)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FKkgerjwud4x9lcf2o7lgln01lq");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
