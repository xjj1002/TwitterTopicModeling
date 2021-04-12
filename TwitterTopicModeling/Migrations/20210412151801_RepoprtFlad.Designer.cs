﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TwitterTopicModeling.Database;
using TwitterTopicModeling.Database.Models;

namespace TwitterTopicModeling.Migrations
{
    [DbContext(typeof(TwitterContext))]
    [Migration("20210412151801_RepoprtFlad")]
    partial class RepoprtFlad
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("TwitterTopicModeling.Database.Models.Report", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ReportName")
                        .HasColumnType("text");

                    b.Property<IEnumerable<ReportTopic>>("Topics")
                        .HasColumnType("jsonb");

                    b.Property<int?>("TwitterUserId")
                        .HasColumnType("integer");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.Property<bool>("malFlag")
                        .HasColumnType("boolean");

                    b.HasKey("id");

                    b.HasIndex("TwitterUserId");

                    b.HasIndex("UserId");

                    b.ToTable("Report");
                });

            modelBuilder.Entity("TwitterTopicModeling.Database.Models.Report_tweet", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("Reportid")
                        .HasColumnType("integer");

                    b.Property<long?>("TweetId")
                        .HasColumnType("bigint");

                    b.Property<bool>("flag")
                        .HasColumnType("boolean");

                    b.HasKey("id");

                    b.HasIndex("Reportid");

                    b.HasIndex("TweetId");

                    b.ToTable("Report_Tweet");
                });

            modelBuilder.Entity("TwitterTopicModeling.Database.Models.Tweet", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("ExternalId")
                        .HasColumnType("bigint");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.Property<int?>("TwitterUserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TwitterUserId");

                    b.ToTable("Tweets");
                });

            modelBuilder.Entity("TwitterTopicModeling.Database.Models.TwitterUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("ExternalId")
                        .HasColumnType("bigint");

                    b.Property<string>("ScreenName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("TwitterUsers");
                });

            modelBuilder.Entity("TwitterTopicModeling.Database.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("password")
                        .HasColumnType("text");

                    b.Property<string>("userName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TwitterTopicModeling.Database.Models.Report", b =>
                {
                    b.HasOne("TwitterTopicModeling.Database.Models.TwitterUser", "TwitterUser")
                        .WithMany()
                        .HasForeignKey("TwitterUserId");

                    b.HasOne("TwitterTopicModeling.Database.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("TwitterUser");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TwitterTopicModeling.Database.Models.Report_tweet", b =>
                {
                    b.HasOne("TwitterTopicModeling.Database.Models.Report", "Report")
                        .WithMany("ReportTweets")
                        .HasForeignKey("Reportid");

                    b.HasOne("TwitterTopicModeling.Database.Models.Tweet", "Tweet")
                        .WithMany()
                        .HasForeignKey("TweetId");

                    b.Navigation("Report");

                    b.Navigation("Tweet");
                });

            modelBuilder.Entity("TwitterTopicModeling.Database.Models.Tweet", b =>
                {
                    b.HasOne("TwitterTopicModeling.Database.Models.TwitterUser", "TwitterUser")
                        .WithMany()
                        .HasForeignKey("TwitterUserId");

                    b.Navigation("TwitterUser");
                });

            modelBuilder.Entity("TwitterTopicModeling.Database.Models.Report", b =>
                {
                    b.Navigation("ReportTweets");
                });
#pragma warning restore 612, 618
        }
    }
}
