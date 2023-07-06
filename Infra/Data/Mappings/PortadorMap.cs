using Dock.Domain.Entities.Cliente;
using Dock.Domain.Entities.Conta;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dock.Infra.Data.Mappings
{
    public class PortadorMap : IEntityTypeConfiguration<Portador>
    {
        public void Configure(EntityTypeBuilder<Portador> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .IsRequired()
                .HasConversion(
                    portadorId => portadorId.Value,
                    value => new PortadorId(value)
                 );

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasColumnType("NVARCHAR")
                .HasMaxLength(180);

            builder.Property(p => p.Cpf)
                .HasConversion(
                    cpf => cpf.Digitos + cpf.DigitoVerificador,
                    value => Cpf.Create(value));

            builder.HasOne(p => p.ContaDigital)
                .WithOne(c => c.Portador)
                .HasForeignKey<ContaDigital>(c => c.PortadorId);

            builder.HasIndex(p => p.Cpf, "IX_Portador_Cpf")
                .IsUnique()
                .HasFilter("[Cpf] is not null");
        }
    }
}
