using Dock.Domain.Entities.Usuarios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dock.Infra.Data.Mappings
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Id)
                .HasConversion(
                   usuarioId => usuarioId.Value,
                   value => new UsuarioId(value)
                );

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(180)
                .HasColumnType("NVARCHAR");

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(250)
                .HasColumnType("NVARCHAR");

            builder.Property(x => x.PasswordHash)
                .IsRequired();

            builder.HasIndex(x => x.Email, "IX_Usuario_Email")
                .IsUnique()
                .HasFilter("[Email] is not null");
        }
    }
}
