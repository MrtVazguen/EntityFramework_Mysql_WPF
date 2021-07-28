using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Exercici4._1.MODEL
{
    public partial class classicmodelsContext : DbContext
    {
        public classicmodelsContext()
        {
        }

        public classicmodelsContext(DbContextOptions<classicmodelsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Offices> Offices { get; set; }
        public virtual DbSet<Orderdetails> Orderdetails { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Payments> Payments { get; set; }
        public virtual DbSet<Productlines> Productlines { get; set; }
        public virtual DbSet<Products> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=localhost;user id=root;database=classicmodels;allowuservariables=True", x => x.ServerVersion("10.4.17-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.CustomerNumber)
                    .HasName("PRIMARY");

                entity.ToTable("customers");

                entity.HasIndex(e => e.SalesRepEmployeeNumber)
                    .HasName("salesRepEmployeeNumber");

                entity.Property(e => e.CustomerNumber)
                    .HasColumnName("customerNumber")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddressLine1)
                    .IsRequired()
                    .HasColumnName("addressLine1")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.AddressLine2)
                    .HasColumnName("addressLine2")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ContactFirstName)
                    .IsRequired()
                    .HasColumnName("contactFirstName")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ContactLastName)
                    .IsRequired()
                    .HasColumnName("contactLastName")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasColumnName("country")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CreditLimit)
                    .HasColumnName("creditLimit")
                    .HasColumnType("decimal(10,2)");

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasColumnName("customerName")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.PostalCode)
                    .HasColumnName("postalCode")
                    .HasColumnType("varchar(15)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.SalesRepEmployeeNumber)
                    .HasColumnName("salesRepEmployeeNumber")
                    .HasColumnType("int(11)");

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.HasOne(d => d.SalesRepEmployeeNumberNavigation)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.SalesRepEmployeeNumber)
                    .HasConstraintName("customers_ibfk_1");
            });

            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasKey(e => e.EmployeeNumber)
                    .HasName("PRIMARY");

                entity.ToTable("employees");

                entity.HasIndex(e => e.OfficeCode)
                    .HasName("officeCode");

                entity.HasIndex(e => e.ReportsTo)
                    .HasName("reportsTo");

                entity.Property(e => e.EmployeeNumber)
                    .HasColumnName("employeeNumber")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Extension)
                    .IsRequired()
                    .HasColumnName("extension")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("firstName")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.JobTitle)
                    .IsRequired()
                    .HasColumnName("jobTitle")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("lastName")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.OfficeCode)
                    .IsRequired()
                    .HasColumnName("officeCode")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ReportsTo)
                    .HasColumnName("reportsTo")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.OfficeCodeNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.OfficeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("employees_ibfk_2");

                entity.HasOne(d => d.ReportsToNavigation)
                    .WithMany(p => p.InverseReportsToNavigation)
                    .HasForeignKey(d => d.ReportsTo)
                    .HasConstraintName("employees_ibfk_1");
            });

            modelBuilder.Entity<Offices>(entity =>
            {
                entity.HasKey(e => e.OfficeCode)
                    .HasName("PRIMARY");

                entity.ToTable("offices");

                entity.Property(e => e.OfficeCode)
                    .HasColumnName("officeCode")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.AddressLine1)
                    .IsRequired()
                    .HasColumnName("addressLine1")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.AddressLine2)
                    .HasColumnName("addressLine2")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasColumnName("country")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasColumnName("postalCode")
                    .HasColumnType("varchar(15)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Territory)
                    .IsRequired()
                    .HasColumnName("territory")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<Orderdetails>(entity =>
            {
                entity.HasKey(e => new { e.OrderNumber, e.ProductCode })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("orderdetails");

                entity.HasIndex(e => e.ProductCode)
                    .HasName("productCode");

                entity.Property(e => e.OrderNumber)
                    .HasColumnName("orderNumber")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ProductCode)
                    .HasColumnName("productCode")
                    .HasColumnType("varchar(15)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.OrderLineNumber)
                    .HasColumnName("orderLineNumber")
                    .HasColumnType("smallint(6)");

                entity.Property(e => e.PriceEach)
                    .HasColumnName("priceEach")
                    .HasColumnType("decimal(10,2)");

                entity.Property(e => e.QuantityOrdered)
                    .HasColumnName("quantityOrdered")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.OrderNumberNavigation)
                    .WithMany(p => p.Orderdetails)
                    .HasForeignKey(d => d.OrderNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("orderdetails_ibfk_1");

                entity.HasOne(d => d.ProductCodeNavigation)
                    .WithMany(p => p.Orderdetails)
                    .HasForeignKey(d => d.ProductCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("orderdetails_ibfk_2");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderNumber)
                    .HasName("PRIMARY");

                entity.ToTable("orders");

                entity.HasIndex(e => e.CustomerNumber)
                    .HasName("customerNumber");

                entity.Property(e => e.OrderNumber)
                    .HasColumnName("orderNumber")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Comments)
                    .HasColumnName("comments")
                    .HasColumnType("text")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CustomerNumber)
                    .HasColumnName("customerNumber")
                    .HasColumnType("int(11)");

                entity.Property(e => e.OrderDate)
                    .HasColumnName("orderDate")
                    .HasColumnType("date");

                entity.Property(e => e.RequiredDate)
                    .HasColumnName("requiredDate")
                    .HasColumnType("date");

                entity.Property(e => e.ShippedDate)
                    .HasColumnName("shippedDate")
                    .HasColumnType("date");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasColumnType("varchar(15)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.HasOne(d => d.CustomerNumberNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("orders_ibfk_1");
            });

            modelBuilder.Entity<Payments>(entity =>
            {
                entity.HasKey(e => new { e.CustomerNumber, e.CheckNumber })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("payments");

                entity.Property(e => e.CustomerNumber)
                    .HasColumnName("customerNumber")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CheckNumber)
                    .HasColumnName("checkNumber")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("decimal(10,2)");

                entity.Property(e => e.PaymentDate)
                    .HasColumnName("paymentDate")
                    .HasColumnType("date");

                entity.HasOne(d => d.CustomerNumberNavigation)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.CustomerNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("payments_ibfk_1");
            });

            modelBuilder.Entity<Productlines>(entity =>
            {
                entity.HasKey(e => e.ProductLine)
                    .HasName("PRIMARY");

                entity.ToTable("productlines");

                entity.Property(e => e.ProductLine)
                    .HasColumnName("productLine")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.HtmlDescription)
                    .HasColumnName("htmlDescription")
                    .HasColumnType("mediumtext")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Image)
                    .HasColumnName("image")
                    .HasColumnType("mediumblob");

                entity.Property(e => e.TextDescription)
                    .HasColumnName("textDescription")
                    .HasColumnType("varchar(4000)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.ProductCode)
                    .HasName("PRIMARY");

                entity.ToTable("products");

                entity.HasIndex(e => e.ProductLine)
                    .HasName("productLine");

                entity.Property(e => e.ProductCode)
                    .HasColumnName("productCode")
                    .HasColumnType("varchar(15)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.BuyPrice)
                    .HasColumnName("buyPrice")
                    .HasColumnType("decimal(10,2)");

                entity.Property(e => e.Msrp)
                    .HasColumnName("MSRP")
                    .HasColumnType("decimal(10,2)");

              

                entity.Property(e => e.ProductLine)
                    .IsRequired()
                    .HasColumnName("productLine")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasColumnName("productName")
                    .HasColumnType("varchar(70)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ProductScale)
                    .IsRequired()
                    .HasColumnName("productScale")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ProductVendor)
                    .IsRequired()
                    .HasColumnName("productVendor")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.QuantityInStock)
                    .HasColumnName("quantityInStock")
                    .HasColumnType("smallint(6)");
 
                entity.HasOne(d => d.ProductLineNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductLine)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("products_ibfk_1");
                entity.Property(e => e.ProductDescription)
                  .IsRequired()
                  .HasColumnName("productDescription")
                  .HasColumnType("text")
                  .HasCharSet("latin1")
                  .HasCollation("latin1_swedish_ci");

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
