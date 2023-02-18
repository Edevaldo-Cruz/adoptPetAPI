﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using adoptPetAPI.Context;

#nullable disable

namespace adoptPetAPI.Migrations
{
    [DbContext(typeof(AdoptPetContext))]
    [Migration("20230218102137_creating-database")]
    partial class creatingdatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("adoptPetAPI.Models.Pet", b =>
                {
                    b.Property<int>("IdPet")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPet"));

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int?>("IdUser")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("IdPet");

                    b.HasIndex("IdUser");

                    b.ToTable("Pets");
                });

            modelBuilder.Entity("adoptPetAPI.Models.User", b =>
                {
                    b.Property<int>("IdUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdUser"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("IdUser");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("adoptPetAPI.Models.Pet", b =>
                {
                    b.HasOne("adoptPetAPI.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("IdUser");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
