﻿// <auto-generated />
using System;
using CakeExchange.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CakeExchange.Migrations
{
    [DbContext(typeof(CakeContext))]
    [Migration("20180625060559_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("CakeExchange.Models.History", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Count");

                    b.Property<DateTime>("Date");

                    b.Property<DateTime>("DateOffer");

                    b.Property<DateTime>("DatePurchase");

                    b.Property<string>("EmailOffer");

                    b.Property<string>("EmailPurchase");

                    b.Property<decimal>("Price");

                    b.HasKey("Id");

                    b.ToTable("History");
                });

            modelBuilder.Entity("CakeExchange.Models.Offer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Count");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Email");

                    b.Property<decimal>("Price");

                    b.HasKey("Id");

                    b.ToTable("Offers");
                });

            modelBuilder.Entity("CakeExchange.Models.Purchase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Count");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Email");

                    b.Property<decimal>("Price");

                    b.HasKey("Id");

                    b.ToTable("Purchase");
                });
#pragma warning restore 612, 618
        }
    }
}
