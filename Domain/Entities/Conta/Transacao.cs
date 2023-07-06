using Dock.Domain.Enums;

namespace Dock.Domain.Entities.Conta
{
    public class Transacao
    {
        private Transacao()
        {

        }
        public static Transacao Create(Money money, TipoTransacao tipoTransacao, DateTimeOffset realizadaEm)
        {
            return new Transacao
            {
                Valor = money,
                TipoTransacao = tipoTransacao,
                RealizadaEm = realizadaEm
            };
        }
        public TransacaoId Id { get; set; }
        public TipoTransacao TipoTransacao { get; private set; }
        public Money Valor { get; private set; }
        public DateTimeOffset RealizadaEm { get; set; }
        
        public ContaDigital ContaDigital;
        public ContaDigitalId ContaDigitalId { get; set; }

    }
}
