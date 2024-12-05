﻿// <auto-generated />
using System;
using Microservice_RatingService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Microservice_RatingService.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microservice_RatingService.Domain.Entities.Rating", b =>
                {
                    b.Property<Guid>("RatingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("RatingId");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Grade")
                        .HasColumnType("int");

                    b.Property<DateTime>("RatingDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("RatingId");

                    b.ToTable("rating");
                });

            modelBuilder.Entity("Microservice_RatingService.Domain.Entities.Rating", b =>
                {
                    b.OwnsOne("Microservice_RatingService.Domain.Entities.Buyer", "Buyer", b1 =>
                        {
                            b1.Property<Guid>("RatingId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("BuyerEmail")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("BuyerEmail");

                            b1.Property<Guid>("BuyerId")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("BuyerId");

                            b1.Property<string>("BuyerUsername")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("BuyerUsername");

                            b1.HasKey("RatingId");

                            b1.ToTable("rating");

                            b1.WithOwner()
                                .HasForeignKey("RatingId");
                        });

                    b.OwnsOne("Microservice_RatingService.Domain.Entities.Purchase", "Purchase", b1 =>
                        {
                            b1.Property<Guid>("RatingId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime>("PurchaseDate")
                                .HasColumnType("datetime2")
                                .HasColumnName("PurchaseDate");

                            b1.Property<Guid>("PurchaseId")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("PurchaseId");

                            b1.Property<decimal>("PurchasePrice")
                                .HasPrecision(18, 2)
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("PurchasePrice");

                            b1.HasKey("RatingId");

                            b1.ToTable("rating");

                            b1.WithOwner()
                                .HasForeignKey("RatingId");
                        });

                    b.OwnsOne("Microservice_RatingService.Domain.Entities.Seller", "Seller", b1 =>
                        {
                            b1.Property<Guid>("RatingId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("SellerEmail")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("SellerEmail");

                            b1.Property<Guid>("SellerId")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("SellerId");

                            b1.Property<string>("SellerUsername")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("SellerUsername");

                            b1.HasKey("RatingId");

                            b1.ToTable("rating");

                            b1.WithOwner()
                                .HasForeignKey("RatingId");
                        });

                    b.Navigation("Buyer")
                        .IsRequired();

                    b.Navigation("Purchase")
                        .IsRequired();

                    b.Navigation("Seller")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
