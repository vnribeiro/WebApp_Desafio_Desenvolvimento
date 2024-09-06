using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAppDesafio.Dominio.Models;

namespace WebAppDesafio.Infra.Dados.Mapeamentos;

public class DepartamentoMap : IEntityTypeConfiguration<Departamento>
{
    public void Configure(EntityTypeBuilder<Departamento> builder)
    {
        builder
            .ToTable("Departamentos");

        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasColumnName("DepartamentoId");

        builder.Property(d => d.Descricao)
            .IsRequired()
            .HasMaxLength(100);
    }
}