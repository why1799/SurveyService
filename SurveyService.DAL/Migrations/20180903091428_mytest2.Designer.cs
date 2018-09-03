﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SurveyService.DAL;

namespace SurveyService.DAL.Migrations
{
    [DbContext(typeof(SurveyServiceDbContext))]
    [Migration("20180903091428_mytest2")]
    partial class mytest2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SurveyService.Models.Answer", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("SelectedOptionId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("SelectedOptionId");

                    b.HasIndex("UserId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("SurveyService.Models.Option", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.ToTable("Options");
                });

            modelBuilder.Entity("SurveyService.Models.OptionsForQuestion", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("OptionId");

                    b.Property<string>("QuestionId");

                    b.HasKey("Id");

                    b.HasIndex("OptionId");

                    b.HasIndex("QuestionId");

                    b.ToTable("OptionsForQuestions");
                });

            modelBuilder.Entity("SurveyService.Models.Question", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Text");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("SurveyService.Models.Survey", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BannerUrl");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateExpires");

                    b.Property<DateTime>("DateStart");

                    b.Property<string>("Description");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Surveys");
                });

            modelBuilder.Entity("SurveyService.Models.SurveyQuestion", b =>
                {
                    b.Property<string>("QuestionId");

                    b.Property<string>("SurveyId");

                    b.Property<string>("Id")
                        .IsRequired();

                    b.Property<bool>("IsRequired");

                    b.Property<int>("Order");

                    b.HasKey("QuestionId", "SurveyId");

                    b.HasAlternateKey("Id");

                    b.HasIndex("SurveyId");

                    b.ToTable("SurveyQuestion");
                });

            modelBuilder.Entity("SurveyService.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DisplayName");

                    b.Property<string>("Login");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SurveyService.Models.Answer", b =>
                {
                    b.HasOne("SurveyService.Models.OptionsForQuestion", "OptionsForQuestion")
                        .WithMany()
                        .HasForeignKey("SelectedOptionId");

                    b.HasOne("SurveyService.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("SurveyService.Models.OptionsForQuestion", b =>
                {
                    b.HasOne("SurveyService.Models.Option", "Option")
                        .WithMany("OptionsForQuestions")
                        .HasForeignKey("OptionId");

                    b.HasOne("SurveyService.Models.Question", "Question")
                        .WithMany("OptionsForQuestions")
                        .HasForeignKey("QuestionId");
                });

            modelBuilder.Entity("SurveyService.Models.SurveyQuestion", b =>
                {
                    b.HasOne("SurveyService.Models.Question", "Question")
                        .WithMany("SurveyQuestion")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SurveyService.Models.Survey", "Survey")
                        .WithMany("SurveyQuestion")
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
