﻿// <auto-generated />
using System;
using GPTServer.Common.DataAccess.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GPTServer.Common.DataAccess.Migrations
{
    [DbContext(typeof(GPTDbContext))]
    [Migration("20230506144607_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GPTServer.Common.Core.Models.AdminKey", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("Key")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("admin_keys");
                });

            modelBuilder.Entity("GPTServer.Common.Core.Models.ApiKey", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("KeyName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<Guid>("UserId")
                        .HasMaxLength(36)
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("IsDeleted", "Id");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("IsDeleted", "Id"), false);

                    b.HasIndex("IsDeleted", "UserId");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("IsDeleted", "UserId"), false);

                    b.HasIndex("IsDeleted", "UserId", "IsActive");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("IsDeleted", "UserId", "IsActive"), false);

                    b.HasIndex("IsDeleted", "UserId", "Key");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("IsDeleted", "UserId", "Key"), false);

                    b.ToTable("api_keys");
                });

            modelBuilder.Entity("GPTServer.Common.Core.Models.ClientIP", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("IP")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("UserId")
                        .HasMaxLength(36)
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("client_ips");
                });

            modelBuilder.Entity("GPTServer.Common.Core.Models.GPTInteraction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("ResponseMs")
                        .HasColumnType("int");

                    b.Property<bool>("Success")
                        .HasColumnType("bit");

                    b.Property<Guid>("UserId")
                        .HasMaxLength(36)
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("gpt_interactions");
                });

            modelBuilder.Entity("GPTServer.Common.Core.Models.Log", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ClientIP")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ExecutorId")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("LogLevel")
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(8000)
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("logs");
                });

            modelBuilder.Entity("GPTServer.Common.Core.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("HasExtensionPermission")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastAuthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastAuthRoutingEnv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("UniqueId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("UserAgent")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted", "Email");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("IsDeleted", "Email"), false);

                    b.HasIndex("IsDeleted", "Id");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("IsDeleted", "Id"), false);

                    b.ToTable("users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("21a2b3df-0553-4d66-91ec-90cfbb285dd0"),
                            CreationDate = new DateTime(2023, 5, 6, 16, 46, 7, 402, DateTimeKind.Local).AddTicks(1648),
                            Email = "teszt@aichatmester.hu",
                            HasExtensionPermission = true,
                            IsDeleted = false,
                            LastAuthDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastAuthRoutingEnv = "ChatGPTExtension",
                            PasswordHash = "M+QCWZJLLRFCKU0V/PUERW4F21H8V3DTTJTPDDXVLGA=",
                            PasswordSalt = "SDA6HQ+8CYIJ+9OM23GM9KJDVGYOIP+TJ9SSAAN9TWM09PXVPINP/OL38JDIPRQQHAKXWONR1TESEM05XTRLPKRBY2QBSW/1IXIFMGWP91HPIQP0F2A1WHGQHTMCX10W",
                            UniqueId = "c95aff8f-3fff-4b3e-a8f6-08dee0dc7c3a",
                            UserAgent = ""
                        });
                });

            modelBuilder.Entity("GPTServer.Common.Core.Models.ApiKey", b =>
                {
                    b.HasOne("GPTServer.Common.Core.Models.User", "User")
                        .WithMany("ApiKeys")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("GPTServer.Common.Core.Models.ClientIP", b =>
                {
                    b.HasOne("GPTServer.Common.Core.Models.User", "User")
                        .WithMany("ClienIPs")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("GPTServer.Common.Core.Models.GPTInteraction", b =>
                {
                    b.HasOne("GPTServer.Common.Core.Models.User", "User")
                        .WithMany("GPTInteractions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("GPTServer.Common.Core.Models.User", b =>
                {
                    b.Navigation("ApiKeys");

                    b.Navigation("ClienIPs");

                    b.Navigation("GPTInteractions");
                });
#pragma warning restore 612, 618
        }
    }
}
