﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lombard.DAL.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20190411121138_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity("Lombard.BL.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("Name");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Lombard.BL.Models.Item", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<decimal>("Price");

                    b.Property<int>("Quantity");

                    b.HasKey("ItemId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Lombard.BL.Models.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CustomerId");

                    b.Property<int?>("ItemId");

                    b.Property<decimal>("Price");

                    b.Property<int>("Quantity");

                    b.Property<DateTime>("TransactionDate");

                    b.HasKey("TransactionId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ItemId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Lombard.BL.Models.Transaction", b =>
                {
                    b.HasOne("Lombard.BL.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.HasOne("Lombard.BL.Models.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId");
                });
#pragma warning restore 612, 618
        }
    }
}
