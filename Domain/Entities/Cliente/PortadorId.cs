namespace Dock.Domain.Entities.Cliente;
public record PortadorId
{
    public Guid Value { get; }

    public PortadorId(Guid value)
    {
        Value = value;
    }
}
