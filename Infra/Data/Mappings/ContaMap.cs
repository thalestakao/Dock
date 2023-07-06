using Dock.Domain.Entities.Conta;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dock.Infra.Data.Mappings
{
    public class ContaMap : IEntityTypeConfiguration<ContaDigital>
    {
        public void Configure(EntityTypeBuilder<ContaDigital> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasConversion(
                    contaId => contaId.Value,
                    value => new ContaDigitalId(value)
                );

            builder.Property(c => c.Agencia)
                .IsRequired();

            builder.Property(c => c.Conta)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(15)
                .IsRequired();
            
            builder.Property(c => c.IsBloqueada)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(c => c.IsAtiva)
                .IsRequired()
                .HasDefaultValue(true);

            builder.OwnsOne(c => c.Saldo, saldoBuilder =>
            {
                saldoBuilder.Property(m => m.Moeda)
                    .HasMaxLength(3)
                    .HasDefaultValue("BRL");
            });

            builder
                .HasMany(c => c.Transacoes)
                .WithOne(t => t.ContaDigital)
                .HasForeignKey(t => t.ContaDigitalId)
                .HasConstraintName("FK_ContaDigital_TransacaoId");

            builder
                .HasOne(c => c.Portador)
                .WithOne(p => p.ContaDigital);
                
        }
    }
}
