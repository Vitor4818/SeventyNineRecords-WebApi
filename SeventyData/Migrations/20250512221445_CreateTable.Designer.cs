﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;
using SeventyData.Context;

#nullable disable

namespace SeventyData.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250512221445_CreateTable")]
    partial class CreateTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            OracleModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SeventyModel.AlbumModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BandId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int?>("BandModelId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<int>("ReleaseYear")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("Id");

                    b.HasIndex("BandId");

                    b.HasIndex("BandModelId");

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("SeventyModel.BandModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<int>("YearStarted")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("Id");

                    b.ToTable("Bands");
                });

            modelBuilder.Entity("SeventyModel.SongModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AlbumId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("BandId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("INTERVAL DAY(8) TO SECOND(7)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.HasIndex("BandId");

                    b.ToTable("Songs");
                });

            modelBuilder.Entity("SeventyModel.AlbumModel", b =>
                {
                    b.HasOne("SeventyModel.BandModel", null)
                        .WithMany()
                        .HasForeignKey("BandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SeventyModel.BandModel", null)
                        .WithMany("Albuns")
                        .HasForeignKey("BandModelId");
                });

            modelBuilder.Entity("SeventyModel.SongModel", b =>
                {
                    b.HasOne("SeventyModel.AlbumModel", null)
                        .WithMany("Songs")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SeventyModel.BandModel", null)
                        .WithMany()
                        .HasForeignKey("BandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SeventyModel.AlbumModel", b =>
                {
                    b.Navigation("Songs");
                });

            modelBuilder.Entity("SeventyModel.BandModel", b =>
                {
                    b.Navigation("Albuns");
                });
#pragma warning restore 612, 618
        }
    }
}
