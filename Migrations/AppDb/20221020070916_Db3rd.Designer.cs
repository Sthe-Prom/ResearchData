﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ResearchData.Models;

namespace ResearchData.Migrations.AppDb
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20221020070916_Db3rd")]
    partial class Db3rd
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4");

            modelBuilder.Entity("ResearchData.Models.EF.Account", b =>
                {
                    b.Property<int>("AccountID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Cell")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Id")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfileImg")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SubunitID")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AccountID");

                    b.HasIndex("Id");

                    b.HasIndex("SubunitID");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("ResearchData.Models.EF.Address", b =>
                {
                    b.Property<int>("AddressID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetNr")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SubunitID")
                        .HasColumnType("int");

                    b.Property<string>("ZipCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AddressID");

                    b.HasIndex("SubunitID");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("ResearchData.Models.EF.Answer", b =>
                {
                    b.Property<int>("AnswerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Answer_")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OptionID")
                        .HasColumnType("int");

                    b.Property<int?>("QuestionID")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("SheetID")
                        .HasColumnType("int");

                    b.HasKey("AnswerID");

                    b.HasIndex("OptionID");

                    b.HasIndex("QuestionID");

                    b.ToTable("Answer");
                });

            modelBuilder.Entity("ResearchData.Models.EF.Question", b =>
                {
                    b.Property<int>("QuestionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("QuestionTypeID")
                        .HasColumnType("int");

                    b.Property<string>("Question_")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SubsectionID")
                        .HasColumnType("int");

                    b.HasKey("QuestionID");

                    b.HasIndex("QuestionTypeID");

                    b.HasIndex("SubsectionID");

                    b.ToTable("Question");
                });

            modelBuilder.Entity("ResearchData.Models.EF.QuestionOption", b =>
                {
                    b.Property<int>("QuestionOptionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Option")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuestionID")
                        .HasColumnType("int");

                    b.HasKey("QuestionOptionID");

                    b.HasIndex("QuestionID");

                    b.ToTable("QuestionOption");
                });

            modelBuilder.Entity("ResearchData.Models.EF.QuestionSubsection", b =>
                {
                    b.Property<int>("QuestionSubsectionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("QuestionID")
                        .HasColumnType("int");

                    b.Property<int>("SubsectionID")
                        .HasColumnType("int");

                    b.HasKey("QuestionSubsectionID");

                    b.HasIndex("QuestionID");

                    b.HasIndex("SubsectionID");

                    b.ToTable("QuestionSubsection");
                });

            modelBuilder.Entity("ResearchData.Models.EF.QuestionType", b =>
                {
                    b.Property<int>("QuestionTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("QuestionTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("QuestionTypeID");

                    b.ToTable("QuestionType");
                });

            modelBuilder.Entity("ResearchData.Models.EF.Section", b =>
                {
                    b.Property<int>("SectionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("SectionName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SectionID");

                    b.ToTable("Section");
                });

            modelBuilder.Entity("ResearchData.Models.EF.Subsection", b =>
                {
                    b.Property<int>("SubsectionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("SectionID")
                        .HasColumnType("int");

                    b.Property<string>("SubsectionName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SubsectionID");

                    b.HasIndex("SectionID");

                    b.ToTable("Subsection");
                });

            modelBuilder.Entity("ResearchData.Models.EF.Subunit", b =>
                {
                    b.Property<int>("SubunitID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("SubunitCell")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SubunitDeptNr")
                        .HasColumnType("int");

                    b.Property<string>("SubunitDesc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubunitEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubunitLogo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubunitName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubunitServerAddress")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SubunitID");

                    b.ToTable("Subunit");
                });

            modelBuilder.Entity("ResearchData.Models.EF.Survey", b =>
                {
                    b.Property<int>("SurveyID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("AccountID")
                        .HasColumnType("int");

                    b.Property<DateTime>("SurveyDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SurveyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SurveyID");

                    b.HasIndex("AccountID");

                    b.ToTable("Survey");
                });

            modelBuilder.Entity("ResearchData.Models.EF.SurveyQuestion", b =>
                {
                    b.Property<int>("SurveyQuestionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("QuestionID")
                        .HasColumnType("int");

                    b.Property<int>("SurveyID")
                        .HasColumnType("int");

                    b.HasKey("SurveyQuestionID");

                    b.HasIndex("QuestionID");

                    b.HasIndex("SurveyID");

                    b.ToTable("SurveyQuestion");
                });

            modelBuilder.Entity("ResearchData.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("ResearchData.Models.EF.Account", b =>
                {
                    b.HasOne("ResearchData.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ResearchData.Models.EF.Subunit", "Subunit")
                        .WithMany("Account")
                        .HasForeignKey("SubunitID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subunit");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ResearchData.Models.EF.Address", b =>
                {
                    b.HasOne("ResearchData.Models.EF.Subunit", "Subunit")
                        .WithMany("Address")
                        .HasForeignKey("SubunitID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subunit");
                });

            modelBuilder.Entity("ResearchData.Models.EF.Answer", b =>
                {
                    b.HasOne("ResearchData.Models.EF.QuestionOption", "QuestionOption")
                        .WithMany("Answer")
                        .HasForeignKey("OptionID");

                    b.HasOne("ResearchData.Models.EF.Question", "Question")
                        .WithMany("Answer")
                        .HasForeignKey("QuestionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");

                    b.Navigation("QuestionOption");
                });

            modelBuilder.Entity("ResearchData.Models.EF.Question", b =>
                {
                    b.HasOne("ResearchData.Models.EF.QuestionType", "QuestionType")
                        .WithMany("Question")
                        .HasForeignKey("QuestionTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ResearchData.Models.EF.Subsection", null)
                        .WithMany("Question")
                        .HasForeignKey("SubsectionID");

                    b.Navigation("QuestionType");
                });

            modelBuilder.Entity("ResearchData.Models.EF.QuestionOption", b =>
                {
                    b.HasOne("ResearchData.Models.EF.Question", "Question")
                        .WithMany("QuestionOption")
                        .HasForeignKey("QuestionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("ResearchData.Models.EF.QuestionSubsection", b =>
                {
                    b.HasOne("ResearchData.Models.EF.Question", "Question")
                        .WithMany("QuestionSubsection")
                        .HasForeignKey("QuestionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ResearchData.Models.EF.Subsection", "Subsection")
                        .WithMany("QuestionSubsection")
                        .HasForeignKey("SubsectionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");

                    b.Navigation("Subsection");
                });

            modelBuilder.Entity("ResearchData.Models.EF.Subsection", b =>
                {
                    b.HasOne("ResearchData.Models.EF.Section", "Section")
                        .WithMany("Subsection")
                        .HasForeignKey("SectionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Section");
                });

            modelBuilder.Entity("ResearchData.Models.EF.Survey", b =>
                {
                    b.HasOne("ResearchData.Models.EF.Account", "Account")
                        .WithMany("Survey")
                        .HasForeignKey("AccountID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("ResearchData.Models.EF.SurveyQuestion", b =>
                {
                    b.HasOne("ResearchData.Models.EF.Question", "Question")
                        .WithMany("SurveyQuestion")
                        .HasForeignKey("QuestionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ResearchData.Models.EF.Survey", "Survey")
                        .WithMany("SurveyQuestion")
                        .HasForeignKey("SurveyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");

                    b.Navigation("Survey");
                });

            modelBuilder.Entity("ResearchData.Models.EF.Account", b =>
                {
                    b.Navigation("Survey");
                });

            modelBuilder.Entity("ResearchData.Models.EF.Question", b =>
                {
                    b.Navigation("Answer");

                    b.Navigation("QuestionOption");

                    b.Navigation("QuestionSubsection");

                    b.Navigation("SurveyQuestion");
                });

            modelBuilder.Entity("ResearchData.Models.EF.QuestionOption", b =>
                {
                    b.Navigation("Answer");
                });

            modelBuilder.Entity("ResearchData.Models.EF.QuestionType", b =>
                {
                    b.Navigation("Question");
                });

            modelBuilder.Entity("ResearchData.Models.EF.Section", b =>
                {
                    b.Navigation("Subsection");
                });

            modelBuilder.Entity("ResearchData.Models.EF.Subsection", b =>
                {
                    b.Navigation("Question");

                    b.Navigation("QuestionSubsection");
                });

            modelBuilder.Entity("ResearchData.Models.EF.Subunit", b =>
                {
                    b.Navigation("Account");

                    b.Navigation("Address");
                });

            modelBuilder.Entity("ResearchData.Models.EF.Survey", b =>
                {
                    b.Navigation("SurveyQuestion");
                });
#pragma warning restore 612, 618
        }
    }
}