using Dock.Domain.Entities.Conta;
using Dock.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dock.Infra.Data.Mappings
{
    public class TransacaoMap : IEntityTypeConfiguration<Transacao>
    {
        public void Configure(EntityTypeBuilder<Transacao> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasConversion(
                    value => value.Id,
                    id => new TransacaoId(id));
            
            builder.Property(t => t.RealizadaEm)
                .IsRequired();

            builder.Property(t => t.TipoTransacao)
                .IsRequired()
                .HasConversion(
                    (tipoTransacao) => tipoTransacao.ToString(),
                    (value) => (TipoTransacao)Enum.Parse(typeof(TipoTransacao), value));

            builder.OwnsOne(x => x.Valor, valorBuilder =>
            {
                valorBuilder.Property(p => p.Moeda).IsRequired().HasMaxLength(3);
                valorBuilder.Property(p => p.Moeda).IsRequired();
            });

            builder.Property(t => t.ContaDigitalId)
                .HasConversion(
                    contaId => contaId.Value,
                    value => new ContaDigitalId(value)
                    );
            builder.HasOne(t => t.ContaDigital)
                .WithMany(c => c.Transacoes)
                .HasForeignKey(t => t.ContaDigitalId)
                .HasForeignKey("FK_Transacao_ContaId");
        }
    }
}
