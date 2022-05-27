using JovemProgramadorMVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JovemProgramadorMVC.Data.Mapeamento
{
    public class AlunoMapeamento : IEntityTypeConfiguration<AlunoModel>
    {
        public void Configure(EntityTypeBuilder<AlunoModel> builder)
        {
            builder.ToTable("Aluno");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Nome).HasColumnType("Varchar(100)");
            builder.Property(t => t.Idade).HasColumnType("Int");
            builder.Property(t => t.Contato).HasColumnType("Varchar(100)");
            builder.Property(t => t.Email).HasColumnType("Varchar(100)");
            builder.Property(t => t.Cep).HasColumnType("Varchar(9)");
        }
    }
}
