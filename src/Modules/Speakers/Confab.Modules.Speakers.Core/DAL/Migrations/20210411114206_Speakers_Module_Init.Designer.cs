﻿// <auto-generated />
using System;
using Confab.Modules.Speakers.Core.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Confab.Modules.Speakers.Core.DAL.Migrations
{
    [DbContext(typeof(SpeakersDbContext))]
    [Migration("20210411114206_Speakers_Module_Init")]
    partial class Speakers_Module_Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("speakers")
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Confab.Modules.Speakers.Core.Entities.Speaker", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AvatralUrl")
                        .HasColumnType("text");

                    b.Property<string>("Bio")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Speakers");
                });
#pragma warning restore 612, 618
        }
    }
}
