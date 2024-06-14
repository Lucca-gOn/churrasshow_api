﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using apiweb.churras.show.Context;

#nullable disable

namespace apiweb.churras.show.Migrations
{
    [DbContext(typeof(ChurrasShowContext))]
    partial class ChurrasShowContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("apiweb.churras.show.Domains.Comentarios", b =>
                {
                    b.Property<Guid>("IdComentario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DescricaoComentario")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Exibe")
                        .HasColumnType("BIT");

                    b.Property<Guid>("IdEvento")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdUsuario")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Pontuacao")
                        .HasColumnType("INT");

                    b.HasKey("IdComentario");

                    b.HasIndex("IdEvento");

                    b.HasIndex("IdUsuario");

                    b.ToTable("Comentarios");
                });

            modelBuilder.Entity("apiweb.churras.show.Domains.Endereco", b =>
                {
                    b.Property<Guid>("IdEndereco")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Bairro")
                        .HasColumnType("VARCHAR(100)");

                    b.Property<int?>("CEP")
                        .HasColumnType("INT");

                    b.Property<string>("Cidade")
                        .HasColumnType("VARCHAR(50)");

                    b.Property<string>("Complemento")
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("Logradouro")
                        .HasColumnType("VARCHAR(100)");

                    b.Property<int?>("Numero")
                        .HasColumnType("INT");

                    b.Property<string>("UF")
                        .HasColumnType("VARCHAR(2)");

                    b.HasKey("IdEndereco");

                    b.ToTable("Endereco");
                });

            modelBuilder.Entity("apiweb.churras.show.Domains.Evento", b =>
                {
                    b.Property<Guid>("IdEvento")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("Acompanhamentos")
                        .HasColumnType("BIT");

                    b.Property<bool?>("Confirmado")
                        .HasColumnType("BIT");

                    b.Property<DateTime?>("DataDeCriacao")
                        .HasColumnType("DATETIME");

                    b.Property<DateTime?>("DataHoraEvento")
                        .IsRequired()
                        .HasColumnType("DATETIME");

                    b.Property<bool?>("Descartaveis")
                        .HasColumnType("BIT");

                    b.Property<int?>("DuracaoEvento")
                        .IsRequired()
                        .HasColumnType("INT");

                    b.Property<int?>("Garconete")
                        .HasColumnType("INT");

                    b.Property<Guid>("IdEndereco")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdPacotes")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdStatusEvento")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdUsuario")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("QuantidadePessoasEvento")
                        .IsRequired()
                        .HasColumnType("INT");

                    b.Property<decimal?>("ValorTotal")
                        .HasColumnType("DECIMAL(18,2)");

                    b.HasKey("IdEvento");

                    b.HasIndex("IdEndereco");

                    b.HasIndex("IdPacotes");

                    b.HasIndex("IdStatusEvento");

                    b.HasIndex("IdUsuario");

                    b.ToTable("Evento");
                });

            modelBuilder.Entity("apiweb.churras.show.Domains.Pacotes", b =>
                {
                    b.Property<Guid>("IdPacotes")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DescricaoPacote")
                        .HasColumnType("TEXT");

                    b.Property<string>("NomePacote")
                        .HasColumnType("VARCHAR(100)");

                    b.Property<decimal?>("ValorPorPessoa")
                        .HasColumnType("DECIMAL(18,2)");

                    b.HasKey("IdPacotes");

                    b.ToTable("Pacotes");
                });

            modelBuilder.Entity("apiweb.churras.show.Domains.StatusEvento", b =>
                {
                    b.Property<Guid>("IdStatusEvento")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Status")
                        .HasColumnType("VARCHAR(100)");

                    b.HasKey("IdStatusEvento");

                    b.ToTable("StatusEvento");
                });

            modelBuilder.Entity("apiweb.churras.show.Domains.TiposUsuario", b =>
                {
                    b.Property<Guid>("IdTipoUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TituloTipoUsuario")
                        .HasColumnType("VARCHAR(100)");

                    b.HasKey("IdTipoUsuario");

                    b.ToTable("TiposUsuario");
                });

            modelBuilder.Entity("apiweb.churras.show.Domains.Usuario", b =>
                {
                    b.Property<Guid>("IdUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CPF")
                        .HasColumnType("VARCHAR(11)");

                    b.Property<int?>("CodRecupSenha")
                        .HasColumnType("INT");

                    b.Property<string>("Email")
                        .HasColumnType("VARCHAR(255)");

                    b.Property<string>("Foto")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("IdEndereco")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdTipoUsuario")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("RG")
                        .HasColumnType("VARCHAR(9)");

                    b.Property<string>("Senha")
                        .HasColumnType("VARCHAR(255)");

                    b.HasKey("IdUsuario");

                    b.HasIndex("IdEndereco");

                    b.HasIndex("IdTipoUsuario");

                    b.HasIndex(new[] { "CPF" }, "IX_Usuario_CPF")
                        .IsUnique()
                        .HasFilter("[CPF] IS NOT NULL");

                    b.HasIndex(new[] { "Email" }, "IX_Usuario_Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.HasIndex(new[] { "RG" }, "IX_Usuario_RG")
                        .IsUnique()
                        .HasFilter("[RG] IS NOT NULL");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("apiweb.churras.show.Domains.Comentarios", b =>
                {
                    b.HasOne("apiweb.churras.show.Domains.Evento", "Evento")
                        .WithMany()
                        .HasForeignKey("IdEvento")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("apiweb.churras.show.Domains.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Evento");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("apiweb.churras.show.Domains.Evento", b =>
                {
                    b.HasOne("apiweb.churras.show.Domains.Endereco", "Endereco")
                        .WithMany()
                        .HasForeignKey("IdEndereco")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("apiweb.churras.show.Domains.Pacotes", "Pacotes")
                        .WithMany()
                        .HasForeignKey("IdPacotes")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("apiweb.churras.show.Domains.StatusEvento", "StatusEvento")
                        .WithMany()
                        .HasForeignKey("IdStatusEvento")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("apiweb.churras.show.Domains.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Endereco");

                    b.Navigation("Pacotes");

                    b.Navigation("StatusEvento");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("apiweb.churras.show.Domains.Usuario", b =>
                {
                    b.HasOne("apiweb.churras.show.Domains.Endereco", "Endereco")
                        .WithMany()
                        .HasForeignKey("IdEndereco")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("apiweb.churras.show.Domains.TiposUsuario", "TiposUsuario")
                        .WithMany()
                        .HasForeignKey("IdTipoUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Endereco");

                    b.Navigation("TiposUsuario");
                });
#pragma warning restore 612, 618
        }
    }
}
