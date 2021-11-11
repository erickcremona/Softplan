﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Softplan.CalculoPrecoVenda.Data.Contexto;

namespace Softplan.CalculoPrecoVenda.Data.Migrations
{
    [DbContext(typeof(SoftplanDbContext))]
    partial class SoftplanDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Softplan.Domain.Entidades.Categoria", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<double>("Lucro")
                        .HasColumnType("float");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(80)");

                    b.HasKey("Id");

                    b.ToTable("Categoria");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a2e7afac-279c-4dad-97b3-737d03dcc6c0"),
                            DataCriacao = new DateTime(2021, 10, 22, 0, 0, 0, 0, DateTimeKind.Local),
                            Lucro = 25.0,
                            Nome = "Brinquedos"
                        },
                        new
                        {
                            Id = new Guid("2aff7519-c311-45d9-8e0e-69c7e17d4988"),
                            DataCriacao = new DateTime(2021, 10, 22, 0, 0, 0, 0, DateTimeKind.Local),
                            Lucro = 30.0,
                            Nome = "Bebidas"
                        },
                        new
                        {
                            Id = new Guid("88f14a26-5d27-4f02-9551-be98be4d5487"),
                            DataCriacao = new DateTime(2021, 10, 22, 0, 0, 0, 0, DateTimeKind.Local),
                            Lucro = 10.0,
                            Nome = "Informática"
                        },
                        new
                        {
                            Id = new Guid("7a418c5a-0dcf-4328-8db1-832fb091d235"),
                            DataCriacao = new DateTime(2021, 10, 22, 0, 0, 0, 0, DateTimeKind.Local),
                            Lucro = 5.0,
                            Nome = "Softplan"
                        });
                });

            modelBuilder.Entity("Softplan.Domain.Entidades.Produto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Categoria")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<decimal>("PrecoCusto")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PrecoVenda")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Produto");
                });
#pragma warning restore 612, 618
        }
    }
}