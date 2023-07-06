﻿// <auto-generated />
using System;
using Dock.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Dock.Migrations
{
    [DbContext(typeof(DockContext))]
    [Migration("20230403021918_CriaBancoDeDadosInicial")]
    partial class CriaBancoDeDadosInicial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Dock.Domain.Entities.Cliente.Portador", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(180)
                        .HasColumnType("NVARCHAR");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Cpf" }, "IX_Portador_Cpf")
                        .IsUnique()
                        .HasFilter("[Cpf] is not null");

                    b.ToTable("Portadores");
                });

            modelBuilder.Entity("Dock.Domain.Entities.Conta.ContaDigital", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Agencia")
                        .HasColumnType("int");

                    b.Property<string>("Conta")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("NVARCHAR");

                    b.Property<bool>("IsAtiva")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<bool>("IsBloqueada")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<Guid>("PortadorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PortadorId")
                        .IsUnique();

                    b.ToTable("Contas");
                });

            modelBuilder.Entity("Dock.Domain.Entities.Conta.Transacao", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ContaDigitalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("FK_Transacao_ContaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("RealizadaEm")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("TipoTransacao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FK_Transacao_ContaId");

                    b.ToTable("Transacoes");
                });

            modelBuilder.Entity("Dock.Domain.Entities.Conta.ContaDigital", b =>
                {
                    b.HasOne("Dock.Domain.Entities.Cliente.Portador", "Portador")
                        .WithOne("ContaDigital")
                        .HasForeignKey("Dock.Domain.Entities.Conta.ContaDigital", "PortadorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Dock.Domain.Entities.Conta.Money", "Saldo", b1 =>
                        {
                            b1.Property<Guid>("ContaDigitalId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Moeda")
                                .IsRequired()
                                .ValueGeneratedOnAdd()
                                .HasMaxLength(3)
                                .HasColumnType("nvarchar(3)")
                                .HasDefaultValue("BRL");

                            b1.Property<decimal>("Valor")
                                .HasColumnType("decimal(18,2)");

                            b1.HasKey("ContaDigitalId");

                            b1.ToTable("Contas");

                            b1.WithOwner()
                                .HasForeignKey("ContaDigitalId");
                        });

                    b.Navigation("Portador");

                    b.Navigation("Saldo")
                        .IsRequired();
                });

            modelBuilder.Entity("Dock.Domain.Entities.Conta.Transacao", b =>
                {
                    b.HasOne("Dock.Domain.Entities.Conta.ContaDigital", "ContaDigital")
                        .WithMany("Transacoes")
                        .HasForeignKey("FK_Transacao_ContaId")
                        .HasConstraintName("FK_ContaDigital_TransacaoId");

                    b.OwnsOne("Dock.Domain.Entities.Conta.Money", "Valor", b1 =>
                        {
                            b1.Property<Guid>("TransacaoId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Moeda")
                                .IsRequired()
                                .HasMaxLength(3)
                                .HasColumnType("nvarchar(3)");

                            b1.Property<decimal>("Valor")
                                .HasColumnType("decimal(18,2)");

                            b1.HasKey("TransacaoId");

                            b1.ToTable("Transacoes");

                            b1.WithOwner()
                                .HasForeignKey("TransacaoId");
                        });

                    b.Navigation("ContaDigital");

                    b.Navigation("Valor")
                        .IsRequired();
                });

            modelBuilder.Entity("Dock.Domain.Entities.Cliente.Portador", b =>
                {
                    b.Navigation("ContaDigital");
                });

            modelBuilder.Entity("Dock.Domain.Entities.Conta.ContaDigital", b =>
                {
                    b.Navigation("Transacoes");
                });
#pragma warning restore 612, 618
        }
    }
}
