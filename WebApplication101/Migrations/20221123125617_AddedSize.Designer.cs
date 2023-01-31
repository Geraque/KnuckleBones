﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebApplication101.EfCore;

#nullable disable

namespace WebApplication101.Migrations
{
    [DbContext(typeof(EF_DataContext))]
    [Migration("20221123125617_AddedSize")]
    partial class AddedSize
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WebApplication101.EfCore.Game", b =>
                {
                    b.Property<int>("IdGame")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdGame"));

                    b.Property<int>("Dice")
                        .HasColumnType("integer");

                    b.Property<string>("Field1")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Field2")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("IdUser1")
                        .HasColumnType("bigint");

                    b.Property<long>("IdUser2")
                        .HasColumnType("bigint");

                    b.Property<int>("Size")
                        .HasColumnType("integer");

                    b.HasKey("IdGame");

                    b.ToTable("game");
                });

            modelBuilder.Entity("WebApplication101.EfCore.Lobby", b =>
                {
                    b.Property<int>("IdGame")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdGame"));

                    b.Property<long>("IdUser1")
                        .HasColumnType("bigint");

                    b.Property<long?>("IdUser2")
                        .HasColumnType("bigint");

                    b.Property<string>("LobbyName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Size")
                        .HasColumnType("integer");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("IdGame");

                    b.ToTable("lobby");
                });

            modelBuilder.Entity("WebApplication101.EfCore.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("DateTime")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Dice")
                        .HasColumnType("integer");

                    b.Property<string>("Field1")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Field2")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("IdGame")
                        .HasColumnType("integer");

                    b.Property<long>("IdUser1")
                        .HasColumnType("bigint");

                    b.Property<long>("IdUser2")
                        .HasColumnType("bigint");

                    b.Property<int>("Move")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("log");
                });

            modelBuilder.Entity("WebApplication101.EfCore.Rating", b =>
                {
                    b.Property<long>("IdUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("IdUser"));

                    b.Property<int>("Draw")
                        .HasColumnType("integer");

                    b.Property<int>("Lose")
                        .HasColumnType("integer");

                    b.Property<int>("Points")
                        .HasColumnType("integer");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Win")
                        .HasColumnType("integer");

                    b.HasKey("IdUser");

                    b.ToTable("rating");
                });
#pragma warning restore 612, 618
        }
    }
}
