﻿// <auto-generated />
using System;
using EVoting_backend.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EVoting_backend.DB.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20211219164332_options limit")]
    partial class optionslimit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EVoting_backend.DB.Models.Form", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("From")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("To")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Form");
                });

            modelBuilder.Entity("EVoting_backend.DB.Models.FormOption", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("Count")
                        .HasColumnType("bigint");

                    b.Property<int>("Ident")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SubFormId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SubFormId");

                    b.ToTable("FormOption");
                });

            modelBuilder.Entity("EVoting_backend.DB.Models.SubForm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ChoicesLimit")
                        .HasColumnType("int");

                    b.Property<int>("FormId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FormId");

                    b.ToTable("SubForm");
                });

            modelBuilder.Entity("EVoting_backend.DB.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EVoting_backend.DB.Models.UserVoted", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("FormId")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("FormId");

                    b.HasIndex("UserId");

                    b.ToTable("UserVotes");
                });

            modelBuilder.Entity("EVoting_backend.DB.Models.Vote", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Data")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Vote");
                });

            modelBuilder.Entity("EVoting_backend.DB.Models.FormOption", b =>
                {
                    b.HasOne("EVoting_backend.DB.Models.SubForm", "SubForm")
                        .WithMany("Options")
                        .HasForeignKey("SubFormId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SubForm");
                });

            modelBuilder.Entity("EVoting_backend.DB.Models.SubForm", b =>
                {
                    b.HasOne("EVoting_backend.DB.Models.Form", "Form")
                        .WithMany("SubForms")
                        .HasForeignKey("FormId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Form");
                });

            modelBuilder.Entity("EVoting_backend.DB.Models.UserVoted", b =>
                {
                    b.HasOne("EVoting_backend.DB.Models.Form", "Form")
                        .WithMany()
                        .HasForeignKey("FormId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EVoting_backend.DB.Models.User", "User")
                        .WithMany("Votes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Form");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EVoting_backend.DB.Models.Form", b =>
                {
                    b.Navigation("SubForms");
                });

            modelBuilder.Entity("EVoting_backend.DB.Models.SubForm", b =>
                {
                    b.Navigation("Options");
                });

            modelBuilder.Entity("EVoting_backend.DB.Models.User", b =>
                {
                    b.Navigation("Votes");
                });
#pragma warning restore 612, 618
        }
    }
}
