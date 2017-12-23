﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace ParentControlApi.Migrations
{
    [DbContext(typeof(ParentControlContext))]
    [Migration("20171223114719_AddedPasswordToUser")]
    partial class AddedPasswordToUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("Device", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DeviceId");

                    b.Property<string>("Name");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("Schedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AllowWithNoTimesheet");

                    b.Property<int?>("DeviceId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("Session", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("DeviceId");

                    b.Property<DateTime?>("EndTime");

                    b.Property<Guid>("SessionId");

                    b.Property<DateTime>("StarTime");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("Timesheet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<DateTime>("DateFrom");

                    b.Property<DateTime?>("DateTo");

                    b.Property<int?>("ScheduleId");

                    b.Property<TimeSpan>("Time");

                    b.HasKey("Id");

                    b.HasIndex("ScheduleId");

                    b.ToTable("Timesheets");
                });

            modelBuilder.Entity("User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Device", b =>
                {
                    b.HasOne("User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Schedule", b =>
                {
                    b.HasOne("Device", "Device")
                        .WithMany()
                        .HasForeignKey("DeviceId");
                });

            modelBuilder.Entity("Session", b =>
                {
                    b.HasOne("Device", "Device")
                        .WithMany()
                        .HasForeignKey("DeviceId");
                });

            modelBuilder.Entity("Timesheet", b =>
                {
                    b.HasOne("Schedule", "Schedule")
                        .WithMany()
                        .HasForeignKey("ScheduleId");
                });
#pragma warning restore 612, 618
        }
    }
}
