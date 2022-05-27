using JovemProgramadorMVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JovemProgramadorMVC.Data.Mapeamento
{
    public class EnderecoAlunoMapeamento : IEntityTypeConfiguration<EnderecoModel>
    {
        public void Configure(EntityTypeBuilder<EnderecoModel> builder)
        {
            builder.ToTable("EnderecoAluno");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.IdAluno).HasColumnType("Int");
            builder.Property(t => t.logradouro).HasColumnType("Varchar(200)");
            builder.Property(t => t.complemento).HasColumnType("Varchar(200)");
            builder.Property(t => t.bairro).HasColumnType("Varchar(50)");
            builder.Property(t => t.localidade).HasColumnType("Varchar(50)");
            builder.Property(t => t.uf).HasColumnType("Varchar(2)");
            builder.Property(t => t.ddd).HasColumnType("Varchar(3)");
        }
    }
}
