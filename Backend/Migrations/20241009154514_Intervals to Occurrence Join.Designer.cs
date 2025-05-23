﻿// <auto-generated />
using System;
using Backend.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Backend.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241009154514_Intervals to Occurrence Join")]
    partial class IntervalstoOccurrenceJoin
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Backend.Models.Interval", b =>
                {
                    b.Property<int>("IntervalNo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("interval_no")
                        .HasAnnotation("Relational:JsonPropertyName", "interval_no");

                    b.Property<string>("Color")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("color")
                        .HasAnnotation("Relational:JsonPropertyName", "color");

                    b.Property<string>("IntervalName")
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)")
                        .HasColumnName("interval_name")
                        .HasAnnotation("Relational:JsonPropertyName", "interval_name");

                    b.Property<int?>("MaxMya")
                        .HasColumnType("int")
                        .HasColumnName("max_ma")
                        .HasAnnotation("Relational:JsonPropertyName", "max_ma");

                    b.Property<int?>("MinMya")
                        .HasColumnType("int")
                        .HasColumnName("min_ma")
                        .HasAnnotation("Relational:JsonPropertyName", "min_ma");

                    b.Property<string>("ParentNo")
                        .HasColumnType("longtext")
                        .HasColumnName("parent_no");

                    b.Property<string>("RecordType")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("record_type")
                        .HasAnnotation("Relational:JsonPropertyName", "record_type");

                    b.Property<int?>("ReferenceNo")
                        .HasColumnType("int")
                        .HasColumnName("reference_no")
                        .HasAnnotation("Relational:JsonPropertyName", "reference_no");

                    b.Property<int?>("ScaleNo")
                        .HasColumnType("int")
                        .HasColumnName("scale_no")
                        .HasAnnotation("Relational:JsonPropertyName", "scale_no");

                    b.HasKey("IntervalNo");

                    b.ToTable("intervals", "paleo");
                });

            modelBuilder.Entity("Backend.Models.Occurrence", b =>
                {
                    b.Property<int>("OccurrenceNo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("occurrence_no");

                    b.Property<string>("AcceptedName")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("accepted_name");

                    b.Property<int>("AcceptedNo")
                        .HasColumnType("int")
                        .HasColumnName("accepted_no");

                    b.Property<string>("AcceptedRank")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("accepted_rank");

                    b.Property<string>("Class")
                        .HasColumnType("longtext")
                        .HasColumnName("class");

                    b.Property<string>("Composition")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("composition");

                    b.Property<string>("EarlyInterval")
                        .HasColumnType("longtext")
                        .HasColumnName("early_interval");

                    b.Property<int?>("EarlyIntervalNo")
                        .HasColumnType("int")
                        .HasColumnName("early_interval_no");

                    b.Property<string>("Environment")
                        .HasColumnType("longtext")
                        .HasColumnName("environment");

                    b.Property<string>("Family")
                        .HasColumnType("longtext")
                        .HasColumnName("family");

                    b.Property<string>("Genus")
                        .HasColumnType("longtext")
                        .HasColumnName("genus");

                    b.Property<int>("IdentifiedNo")
                        .HasColumnType("int")
                        .HasColumnName("identified_no");

                    b.Property<string>("IdentifiedRank")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("identified_rank");

                    b.Property<double?>("Lat")
                        .HasColumnType("double")
                        .HasColumnName("lat");

                    b.Property<int?>("LateIntervalNo")
                        .HasColumnType("int")
                        .HasColumnName("late_interval_no");

                    b.Property<double?>("Lng")
                        .HasColumnType("double")
                        .HasColumnName("lng");

                    b.Property<double?>("MaxMa")
                        .HasColumnType("double")
                        .HasColumnName("max_ma");

                    b.Property<double?>("MinMa")
                        .HasColumnType("double")
                        .HasColumnName("min_ma");

                    b.Property<string>("Order")
                        .HasColumnType("longtext")
                        .HasColumnName("order");

                    b.Property<double?>("PaleoLat")
                        .HasColumnType("double")
                        .HasColumnName("paleolat");

                    b.Property<double?>("PaleoLng")
                        .HasColumnType("double")
                        .HasColumnName("paleolng");

                    b.Property<string>("Paleoage")
                        .HasColumnType("longtext")
                        .HasColumnName("paleoage");

                    b.Property<string>("Phylum")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("phylum");

                    b.Property<string>("RecordType")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("record_type");

                    b.HasKey("OccurrenceNo");

                    b.HasIndex("EarlyIntervalNo");

                    b.ToTable("occurrences", "paleo");
                });

            modelBuilder.Entity("Backend.Models.Occurrence", b =>
                {
                    b.HasOne("Backend.Models.Interval", "Interval")
                        .WithMany("Occurrences")
                        .HasForeignKey("EarlyIntervalNo");

                    b.Navigation("Interval");
                });

            modelBuilder.Entity("Backend.Models.Interval", b =>
                {
                    b.Navigation("Occurrences");
                });
#pragma warning restore 612, 618
        }
    }
}
