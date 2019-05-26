﻿// <auto-generated />
using System;
using FST.TournamentPlanner.DB.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FST.TournamentPlanner.DB.Migrations
{
    [DbContext(typeof(PlannerContext))]
    [Migration("20190526170939_ReworkDatabase-V1")]
    partial class ReworkDatabaseV1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FST.TournamentPlanner.DB.Models.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int?>("PlayAreaBookingId");

                    b.Property<int>("State");

                    b.Property<int?>("SuccessorId");

                    b.Property<int?>("TeamOneId");

                    b.Property<int?>("TeamTwoId");

                    b.Property<int?>("TournamentId");

                    b.HasKey("Id");

                    b.HasIndex("PlayAreaBookingId");

                    b.HasIndex("SuccessorId");

                    b.HasIndex("TeamOneId");

                    b.HasIndex("TeamTwoId");

                    b.HasIndex("TournamentId");

                    b.ToTable("Match","tp");
                });

            modelBuilder.Entity("FST.TournamentPlanner.DB.Models.MatchResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int?>("MatchId");

                    b.Property<int?>("Score");

                    b.Property<int?>("TeamId");

                    b.HasKey("Id");

                    b.HasIndex("MatchId");

                    b.HasIndex("TeamId");

                    b.ToTable("MatchResult","tp");
                });

            modelBuilder.Entity("FST.TournamentPlanner.DB.Models.PlayArea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<int?>("TournamentId");

                    b.HasKey("Id");

                    b.HasIndex("TournamentId");

                    b.ToTable("PlayArea","tp");
                });

            modelBuilder.Entity("FST.TournamentPlanner.DB.Models.PlayAreaBooking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime>("End");

                    b.Property<int?>("PlayAreaId");

                    b.Property<DateTime>("Start");

                    b.HasKey("Id");

                    b.HasIndex("PlayAreaId");

                    b.ToTable("PlayAreaBooking","tp");
                });

            modelBuilder.Entity("FST.TournamentPlanner.DB.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Name");

                    b.Property<int?>("TournametId");

                    b.HasKey("Id");

                    b.HasIndex("TournametId");

                    b.ToTable("Team","tp");
                });

            modelBuilder.Entity("FST.TournamentPlanner.DB.Models.Tournament", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description");

                    b.Property<int>("MatchDuration");

                    b.Property<string>("Name");

                    b.Property<DateTime>("Start");

                    b.Property<int>("State");

                    b.Property<int>("TeamCount");

                    b.HasKey("Id");

                    b.ToTable("Tournament","tp");
                });

            modelBuilder.Entity("FST.TournamentPlanner.DB.Models.Match", b =>
                {
                    b.HasOne("FST.TournamentPlanner.DB.Models.PlayAreaBooking", "PlayAreaBooking")
                        .WithMany()
                        .HasForeignKey("PlayAreaBookingId");

                    b.HasOne("FST.TournamentPlanner.DB.Models.Match", "Successor")
                        .WithMany("Predecessors")
                        .HasForeignKey("SuccessorId");

                    b.HasOne("FST.TournamentPlanner.DB.Models.MatchResult", "TeamOne")
                        .WithMany()
                        .HasForeignKey("TeamOneId");

                    b.HasOne("FST.TournamentPlanner.DB.Models.MatchResult", "TeamTwo")
                        .WithMany()
                        .HasForeignKey("TeamTwoId");

                    b.HasOne("FST.TournamentPlanner.DB.Models.Tournament")
                        .WithMany("Matches")
                        .HasForeignKey("TournamentId");
                });

            modelBuilder.Entity("FST.TournamentPlanner.DB.Models.MatchResult", b =>
                {
                    b.HasOne("FST.TournamentPlanner.DB.Models.Match", "Match")
                        .WithMany()
                        .HasForeignKey("MatchId");

                    b.HasOne("FST.TournamentPlanner.DB.Models.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId");
                });

            modelBuilder.Entity("FST.TournamentPlanner.DB.Models.PlayArea", b =>
                {
                    b.HasOne("FST.TournamentPlanner.DB.Models.Tournament", "Tournament")
                        .WithMany("PlayAreas")
                        .HasForeignKey("TournamentId");
                });

            modelBuilder.Entity("FST.TournamentPlanner.DB.Models.PlayAreaBooking", b =>
                {
                    b.HasOne("FST.TournamentPlanner.DB.Models.PlayArea", "PlayArea")
                        .WithMany()
                        .HasForeignKey("PlayAreaId");
                });

            modelBuilder.Entity("FST.TournamentPlanner.DB.Models.Team", b =>
                {
                    b.HasOne("FST.TournamentPlanner.DB.Models.Tournament", "Tournamet")
                        .WithMany("Teams")
                        .HasForeignKey("TournametId");
                });
#pragma warning restore 612, 618
        }
    }
}
