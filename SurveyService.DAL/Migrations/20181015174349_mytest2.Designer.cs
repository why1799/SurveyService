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
    [Migration("20181015174349_mytest2")]
    partial class mytest2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SurveyService.Models.Option", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Order");

                    b.Property<string>("QuestionId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Options");
                });

            modelBuilder.Entity("SurveyService.Models.OptionsForAnswer", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("OptionId");

                    b.Property<string>("UserAnswerId");

                    b.HasKey("Id");

                    b.HasIndex("OptionId");

                    b.HasIndex("UserAnswerId");

                    b.ToTable("OptionsForAnswers");
                });

            modelBuilder.Entity("SurveyService.Models.Survey", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BannerUrl");

                    b.Property<string>("CreatedById");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateExpires");

                    b.Property<DateTime>("DateStart");

                    b.Property<string>("Description");

                    b.Property<byte[]>("Image");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.ToTable("Surveys");
                });

            modelBuilder.Entity("SurveyService.Models.SurveyQuestion", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("HasOwnAnswer");

                    b.Property<bool>("IsRequired");

                    b.Property<int>("Order");

                    b.Property<string>("QuestionText");

                    b.Property<string>("SurveyId");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("SurveyId");

                    b.ToTable("SurveyQuestions");
                });

            modelBuilder.Entity("SurveyService.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DisplayName");

                    b.Property<string>("Login");

                    b.Property<string>("Role");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SurveyService.Models.UserAnswer", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("OwnAnswerText");

                    b.Property<string>("QuestionId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.HasIndex("UserId");

                    b.ToTable("UserAnswers");
                });

            modelBuilder.Entity("SurveyService.Models.Option", b =>
                {
                    b.HasOne("SurveyService.Models.SurveyQuestion", "SurveyQuestion")
                        .WithMany("Options")
                        .HasForeignKey("QuestionId");
                });

            modelBuilder.Entity("SurveyService.Models.OptionsForAnswer", b =>
                {
                    b.HasOne("SurveyService.Models.Option", "Option")
                        .WithMany("OptionsForAnswers")
                        .HasForeignKey("OptionId");

                    b.HasOne("SurveyService.Models.UserAnswer", "UserAnswer")
                        .WithMany("OptionsForAnswers")
                        .HasForeignKey("UserAnswerId");
                });

            modelBuilder.Entity("SurveyService.Models.Survey", b =>
                {
                    b.HasOne("SurveyService.Models.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");
                });

            modelBuilder.Entity("SurveyService.Models.SurveyQuestion", b =>
                {
                    b.HasOne("SurveyService.Models.Survey", "Survey")
                        .WithMany("SurveyQuestion")
                        .HasForeignKey("SurveyId");
                });

            modelBuilder.Entity("SurveyService.Models.UserAnswer", b =>
                {
                    b.HasOne("SurveyService.Models.SurveyQuestion", "SurveyQuestion")
                        .WithMany("UserAnswers")
                        .HasForeignKey("QuestionId");

                    b.HasOne("SurveyService.Models.User", "User")
                        .WithMany("UserAnswers")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
