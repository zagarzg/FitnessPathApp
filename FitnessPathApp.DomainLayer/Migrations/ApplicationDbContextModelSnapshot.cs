﻿// <auto-generated />
using System;
using FitnessPathApp.DomainLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FitnessPathApp.DomainLayer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.22")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FitnessPathApp.DomainLayer.Entities.Exercise", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Reps")
                        .HasColumnType("int");

                    b.Property<int>("Sets")
                        .HasColumnType("int");

                    b.Property<Guid>("TrainingLogId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("TrainingLogId");

                    b.ToTable("Exercise");

                    b.HasData(
                        new
                        {
                            Id = new Guid("82a61b04-1cda-4045-abb5-0c1596f9aa36"),
                            Name = "Bench Press",
                            Reps = 5,
                            Sets = 5,
                            TrainingLogId = new Guid("cb31d06e-13da-4ba0-a923-5c062399f3a8"),
                            Weight = 100.0
                        },
                        new
                        {
                            Id = new Guid("46aa1ca5-4670-4a38-a840-96204dd0b3a2"),
                            Name = "Squat",
                            Reps = 5,
                            Sets = 5,
                            TrainingLogId = new Guid("cb31d06e-13da-4ba0-a923-5c062399f3a8"),
                            Weight = 140.0
                        },
                        new
                        {
                            Id = new Guid("853172d8-26ca-4de1-acc0-b28d753c328f"),
                            Name = "Deadlift",
                            Reps = 5,
                            Sets = 3,
                            TrainingLogId = new Guid("cb31d06e-13da-4ba0-a923-5c062399f3a8"),
                            Weight = 180.0
                        },
                        new
                        {
                            Id = new Guid("9d9d6825-98ba-47cf-b137-2f6431a047ca"),
                            Name = "Overhead Press",
                            Reps = 8,
                            Sets = 3,
                            TrainingLogId = new Guid("cb31d06e-13da-4ba0-a923-5c062399f3a8"),
                            Weight = 60.0
                        },
                        new
                        {
                            Id = new Guid("e937cea5-99db-4068-96a2-c75fde51df74"),
                            Name = "Barbell Row",
                            Reps = 8,
                            Sets = 3,
                            TrainingLogId = new Guid("cb31d06e-13da-4ba0-a923-5c062399f3a8"),
                            Weight = 70.0
                        });
                });

            modelBuilder.Entity("FitnessPathApp.DomainLayer.Entities.FoodItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Calories")
                        .HasColumnType("int");

                    b.Property<int>("Carbs")
                        .HasColumnType("int");

                    b.Property<int>("Fat")
                        .HasColumnType("int");

                    b.Property<Guid>("FoodLogId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Protein")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FoodLogId");

                    b.ToTable("FoodItem");

                    b.HasData(
                        new
                        {
                            Id = new Guid("aac86f05-0ed4-43a1-876f-6ba34f605661"),
                            Calories = 170,
                            Carbs = 0,
                            Fat = 7,
                            FoodLogId = new Guid("da789a67-5481-4ac9-b338-0a9b3c069a1f"),
                            Name = "Tuna",
                            Protein = 26
                        },
                        new
                        {
                            Id = new Guid("fe8e839e-c834-4b88-ab3c-e8fe0610a3f1"),
                            Calories = 356,
                            Carbs = 72,
                            Fat = 2,
                            FoodLogId = new Guid("da789a67-5481-4ac9-b338-0a9b3c069a1f"),
                            Name = "Pasta",
                            Protein = 12
                        },
                        new
                        {
                            Id = new Guid("81b43909-4490-42fe-af1d-f0e952cf727a"),
                            Calories = 325,
                            Carbs = 25,
                            Fat = 3,
                            FoodLogId = new Guid("da789a67-5481-4ac9-b338-0a9b3c069a1f"),
                            Name = "Zbregov Protein",
                            Protein = 50
                        });
                });

            modelBuilder.Entity("FitnessPathApp.DomainLayer.Entities.FoodLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("FoodLog");

                    b.HasData(
                        new
                        {
                            Id = new Guid("da789a67-5481-4ac9-b338-0a9b3c069a1f"),
                            Date = new DateTime(2022, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("FitnessPathApp.DomainLayer.Entities.TrainingLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("TrainingLog");

                    b.HasData(
                        new
                        {
                            Id = new Guid("cb31d06e-13da-4ba0-a923-5c062399f3a8"),
                            Date = new DateTime(2022, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("FitnessPathApp.DomainLayer.Entities.WeightLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<double>("Value")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("WeightLogs");

                    b.HasData(
                        new
                        {
                            Id = new Guid("0faee6ac-1772-4bbe-9990-a7d9a22dd559"),
                            Date = new DateTime(2022, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Value = 77.5
                        },
                        new
                        {
                            Id = new Guid("c4714153-23fc-4413-b6dc-7fa230cb8883"),
                            Date = new DateTime(2022, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Value = 78.0
                        },
                        new
                        {
                            Id = new Guid("9d9926f0-ee0b-4cdb-b4bd-c178377d8868"),
                            Date = new DateTime(2022, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Value = 77.299999999999997
                        },
                        new
                        {
                            Id = new Guid("8514a58d-0edc-46bc-a0c8-bd912b5f5742"),
                            Date = new DateTime(2022, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Value = 77.599999999999994
                        },
                        new
                        {
                            Id = new Guid("52fce968-8a43-4dbd-ab26-dcf334c149dd"),
                            Date = new DateTime(2022, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Value = 77.799999999999997
                        });
                });

            modelBuilder.Entity("FitnessPathApp.DomainLayer.Entities.Exercise", b =>
                {
                    b.HasOne("FitnessPathApp.DomainLayer.Entities.TrainingLog", "Log")
                        .WithMany("Exercises")
                        .HasForeignKey("TrainingLogId");
                });

            modelBuilder.Entity("FitnessPathApp.DomainLayer.Entities.FoodItem", b =>
                {
                    b.HasOne("FitnessPathApp.DomainLayer.Entities.FoodLog", "Log")
                        .WithMany("FoodItems")
                        .HasForeignKey("FoodLogId");
                });
#pragma warning restore 612, 618
        }
    }
}
