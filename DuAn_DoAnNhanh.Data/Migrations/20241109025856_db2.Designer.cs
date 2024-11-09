﻿// <auto-generated />
using System;
using DuAn_DoAnNhanh.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DuAn_DoAnNhanh.Data.Migrations
{
    [DbContext(typeof(MyDBContext))]
    [Migration("20241109025856_db2")]
    partial class db2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-rc.2.24474.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DuAn_DoAnNhanh.Data.Entities.Address", b =>
                {
                    b.Property<Guid>("AddressID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AddressName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumberPhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AddressID");

                    b.ToTable("Address", (string)null);
                });

            modelBuilder.Entity("DuAn_DoAnNhanh.Data.Entities.Bill", b =>
                {
                    b.Property<Guid>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("OrderID");

                    b.HasIndex("UserID");

                    b.ToTable("Orders", (string)null);
                });

            modelBuilder.Entity("DuAn_DoAnNhanh.Data.Entities.BillDetails", b =>
                {
                    b.Property<Guid>("OrderDetailsID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ComboID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrderID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("ProductID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderDetailsID");

                    b.HasIndex("ComboID");

                    b.HasIndex("OrderID");

                    b.HasIndex("ProductID");

                    b.ToTable("OrderDetails", (string)null);
                });

            modelBuilder.Entity("DuAn_DoAnNhanh.Data.Entities.Cart", b =>
                {
                    b.Property<Guid>("CartID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CartID");

                    b.ToTable("Carts", (string)null);
                });

            modelBuilder.Entity("DuAn_DoAnNhanh.Data.Entities.CartItem", b =>
                {
                    b.Property<Guid>("CartItemID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CartID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ComboID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("CartItemID");

                    b.HasIndex("CartID");

                    b.HasIndex("ComboID");

                    b.HasIndex("ProductID");

                    b.ToTable("CartItems", (string)null);
                });

            modelBuilder.Entity("DuAn_DoAnNhanh.Data.Entities.Combo", b =>
                {
                    b.Property<Guid>("ComboID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ComboName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreteDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("ComboID");

                    b.ToTable("Combos", (string)null);
                });

            modelBuilder.Entity("DuAn_DoAnNhanh.Data.Entities.Product", b =>
                {
                    b.Property<Guid>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreteDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("ProductID");

                    b.ToTable("Products", (string)null);
                });

            modelBuilder.Entity("DuAn_DoAnNhanh.Data.Entities.ProductCombo", b =>
                {
                    b.Property<Guid>("ProductComboID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ComboID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("ProductComboID");

                    b.HasIndex("ComboID");

                    b.HasIndex("ProductID");

                    b.ToTable("ProductCombos", (string)null);
                });

            modelBuilder.Entity("DuAn_DoAnNhanh.Data.Entities.User", b =>
                {
                    b.Property<Guid>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AddressID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("DuAn_DoAnNhanh.Data.Entities.Address", b =>
                {
                    b.HasOne("DuAn_DoAnNhanh.Data.Entities.User", "User")
                        .WithMany("Addresses")
                        .HasForeignKey("AddressID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DuAn_DoAnNhanh.Data.Entities.Bill", b =>
                {
                    b.HasOne("DuAn_DoAnNhanh.Data.Entities.User", "User")
                        .WithMany("Orderes")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DuAn_DoAnNhanh.Data.Entities.BillDetails", b =>
                {
                    b.HasOne("DuAn_DoAnNhanh.Data.Entities.Combo", "Combo")
                        .WithMany("BillDetails")
                        .HasForeignKey("ComboID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DuAn_DoAnNhanh.Data.Entities.Bill", "Order")
                        .WithMany("BillDetails")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DuAn_DoAnNhanh.Data.Entities.Product", "Product")
                        .WithMany("BillDetails")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Combo");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("DuAn_DoAnNhanh.Data.Entities.CartItem", b =>
                {
                    b.HasOne("DuAn_DoAnNhanh.Data.Entities.Cart", "Cart")
                        .WithMany("CartItems")
                        .HasForeignKey("CartID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DuAn_DoAnNhanh.Data.Entities.Combo", "Combo")
                        .WithMany("CartItemes")
                        .HasForeignKey("ComboID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DuAn_DoAnNhanh.Data.Entities.Product", "Product")
                        .WithMany("CartItemes")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("Combo");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("DuAn_DoAnNhanh.Data.Entities.ProductCombo", b =>
                {
                    b.HasOne("DuAn_DoAnNhanh.Data.Entities.Combo", "Combo")
                        .WithMany("ProductComboes")
                        .HasForeignKey("ComboID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DuAn_DoAnNhanh.Data.Entities.Product", "Product")
                        .WithMany("ProductComboes")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Combo");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("DuAn_DoAnNhanh.Data.Entities.User", b =>
                {
                    b.HasOne("DuAn_DoAnNhanh.Data.Entities.Cart", "Cart")
                        .WithOne("User")
                        .HasForeignKey("DuAn_DoAnNhanh.Data.Entities.User", "UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");
                });

            modelBuilder.Entity("DuAn_DoAnNhanh.Data.Entities.Bill", b =>
                {
                    b.Navigation("BillDetails");
                });

            modelBuilder.Entity("DuAn_DoAnNhanh.Data.Entities.Cart", b =>
                {
                    b.Navigation("CartItems");

                    b.Navigation("User")
                        .IsRequired();
                });

            modelBuilder.Entity("DuAn_DoAnNhanh.Data.Entities.Combo", b =>
                {
                    b.Navigation("BillDetails");

                    b.Navigation("CartItemes");

                    b.Navigation("ProductComboes");
                });

            modelBuilder.Entity("DuAn_DoAnNhanh.Data.Entities.Product", b =>
                {
                    b.Navigation("BillDetails");

                    b.Navigation("CartItemes");

                    b.Navigation("ProductComboes");
                });

            modelBuilder.Entity("DuAn_DoAnNhanh.Data.Entities.User", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("Orderes");
                });
#pragma warning restore 612, 618
        }
    }
}
