﻿// <auto-generated />
using System;
using DocumentServices.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DocumentServices.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20240225051726_addFilePath")]
    partial class addFilePath
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.23")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DocumentServices.Models.Document", b =>
                {
                    b.Property<int>("documentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("documentID"), 1L, 1);

                    b.Property<string>("creator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("filePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nameDocument")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("typeDocument")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("updateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("documentID");

                    b.ToTable("Documents");

                    b.HasData(
                        new
                        {
                            documentID = 1,
                            creator = "uynnhi",
                            nameDocument = "HelloWorld",
                            typeDocument = "load Sumary",
                            updateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            documentID = 2,
                            creator = "kimenk",
                            nameDocument = "HelloSadari",
                            typeDocument = "load Sumary",
                            updateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
