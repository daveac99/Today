﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using TodayList.Models;

namespace TodayList.Migrations
{
    [DbContext(typeof(TodayContext))]
    [Migration("20180310234943_AddListOrderattribute")]
    partial class AddListOrderattribute
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TodayList.Models.Today", b =>
                {
                    b.Property<int>("TodayId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("Description");

                    b.Property<bool>("Done");

                    b.Property<DateTime>("DoneDate");

                    b.Property<int>("ListOrder");

                    b.HasKey("TodayId");

                    b.ToTable("Today");
                });
#pragma warning restore 612, 618
        }
    }
}
