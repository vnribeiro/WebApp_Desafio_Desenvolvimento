using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAppDesafio.API.Dominio.Models;

namespace WebAppDesafio.API.Infra.Dados.Mapeamentos;

public class ChamadoMap : IEntityTypeConfiguration<Chamado>
{
    public void Configure(EntityTypeBuilder<Chamado> builder)
    {
        builder
            .ToTable("Chamados");

        builder
            .HasKey(c => c.Id);

        builder
            .Property(x => x.Id)
            .HasColumnName("ChamadoId");

        builder.Property(c => c.Assunto)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.Solicitante)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(c => c.DataAbertura)
            .IsRequired();

        // Relacionamento com Departamento (muitos para um)
        builder.HasOne(c => c.Departamento)
            .WithMany()
            .HasForeignKey("DepartamentoId")
            .IsRequired();
    }
}