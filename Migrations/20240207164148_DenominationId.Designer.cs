﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SelfCheckoutMachine.Model;

#nullable disable

namespace SelfCheckoutMachine.Migrations
{
    [DbContext(typeof(CurrencyContext))]
    [Migration("20240207164148_DenominationId")]
    partial class DenominationId
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("SelfCheckoutMachine.Model.Denomination", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<uint>("Amount")
                        .HasColumnType("int unsigned");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Denominations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = 0u,
                            Value = "5"
                        },
                        new
                        {
                            Id = 2,
                            Amount = 0u,
                            Value = "10"
                        },
                        new
                        {
                            Id = 3,
                            Amount = 0u,
                            Value = "20"
                        },
                        new
                        {
                            Id = 4,
                            Amount = 0u,
                            Value = "50"
                        },
                        new
                        {
                            Id = 5,
                            Amount = 0u,
                            Value = "100"
                        },
                        new
                        {
                            Id = 6,
                            Amount = 0u,
                            Value = "200"
                        },
                        new
                        {
                            Id = 7,
                            Amount = 0u,
                            Value = "500"
                        },
                        new
                        {
                            Id = 8,
                            Amount = 0u,
                            Value = "1000"
                        },
                        new
                        {
                            Id = 9,
                            Amount = 0u,
                            Value = "2000"
                        },
                        new
                        {
                            Id = 10,
                            Amount = 0u,
                            Value = "5000"
                        },
                        new
                        {
                            Id = 11,
                            Amount = 0u,
                            Value = "10000"
                        },
                        new
                        {
                            Id = 12,
                            Amount = 0u,
                            Value = "20000"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
