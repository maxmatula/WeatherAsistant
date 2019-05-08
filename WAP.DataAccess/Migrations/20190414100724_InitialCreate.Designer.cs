﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WAP.DataAccess.DAL;

namespace WAP.DataAccess.Migrations
{
    [DbContext(typeof(WapContext))]
    [Migration("20190414100724_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity("WAP.DataAccess.Models.QueryLog", b =>
                {
                    b.Property<int>("QueryLogId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CityName");

                    b.Property<DateTime>("QueryDate");

                    b.HasKey("QueryLogId");

                    b.ToTable("QueryLogs");
                });
#pragma warning restore 612, 618
        }
    }
}
