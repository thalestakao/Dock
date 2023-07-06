using Dock.Domain.Entities.Cliente;
using Dock.Domain.Enums;

namespace Dock.Domain.Entities.Conta
{
    public class ContaDigital
    {
        public ContaDigitalId Id { get; private set; }
        public int Agencia { get; private set; }
        public string Conta { get; set; }
        public Money Saldo { get; private set; }
        public bool IsBloqueada { get; private set; } = false;
        public bool IsAtiva { get; private set; } = true;
        public List<Transacao>? Transacoes { get; private set; } = new();
        public PortadorId PortadorId{ get; private set; }
        public Portador Portador { get; private set; }
        private ContaDigital() { }

        public static ContaDigital Create(PortadorId portadorId, Money money)
        {
            return new ContaDigital
            {
                Agencia = 1,
                Conta = new Random().NextInt64(1, 99999999).ToString(),
                PortadorId = portadorId,
                Saldo = money,
                IsBloqueada = false,
                IsAtiva = true
            };

        }
        public void Bloquear() 
        {
            IsBloqueada = true;
        }

        public void Desbloquear()
        {
            IsBloqueada = false;
        }

        public void Desativar()
        {
            IsAtiva = false;
        }

        public Money Sacar(Money money)
        {
            if (money == null)
                throw new ArgumentNullException();
            
            if (!PodeSacar())
                return null;

            if(!money.Moeda.Equals(Saldo.Moeda))
                return null;

            var transacao = Transacao.Create(money, TipoTransacao.SAQUE, DateTimeOffset.Now);

            Saldo = Saldo - transacao.Valor;
            
            return Saldo;            
        }

        public Money Depositar(Money money)
        {
            if (money == null)
                throw new ArgumentNullException();

            if (!money.Moeda.Equals(Saldo.Moeda))
            {
                return null;
            }

            var transacao = Transacao.Create(money, TipoTransacao.DEPISTO, DateTimeOffset.Now);

            Saldo = Saldo + transacao.Valor;

            return Saldo;
        }

        private bool PodeSacar()
        {
            if(Transacoes.Count == 0)
            {
                return true;
            }

            var valorTotalUltimas24Horas = Transacoes
                .Where(
                    transacao => transacao.RealizadaEm > DateTimeOffset.Now.AddHours(-24) && 
                    transacao.TipoTransacao == TipoTransacao.SAQUE)
                .Sum(t => t.Valor.Valor);

            if (valorTotalUltimas24Horas > 2000)
                return false;
            return true;
                
        }

    }
}
