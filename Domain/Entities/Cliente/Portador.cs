using Dock.Domain.Entities.Conta;

namespace Dock.Domain.Entities.Cliente
{
    public class Portador
    {
        public PortadorId Id { get; set; }
        public string Nome { get; set; }
        public Cpf Cpf { get; set; }
        public ContaDigital? ContaDigital { get; set; }

        private Portador() { }

        public static Portador Create(string nome, Cpf cpf)
        {
            return new Portador
            {
                Id = new PortadorId(Guid.NewGuid()),
                Nome = nome,
                Cpf = cpf
            }; 
        }
    }
}
