using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAppDesafio.Dominio.Models;

namespace WebAppDesafio.Infra.Dados.Mapeamentos;

public class ChamadoMap : IEntityTypeConfiguration<Chamado>
{
    public void Configure(EntityTypeBuilder<Chamado> builder)
    {
        builder
            .ToTable("Chamados");

        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasColumnName("ChamadoId");

        builder.Property(c => c.Assunto)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Solicitante)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.DataAbertura)
            .IsRequired();

        builder.HasOne(c => c.Departamento)
            .WithMany()
            .HasForeignKey("DepartamentoId")
            .IsRequired();
    }
}