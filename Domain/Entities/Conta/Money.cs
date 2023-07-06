namespace Dock.Domain.Entities.Conta
{
    public record Money
    {
        public Money(decimal valor, string moeda = "BRL")
        {
            Valor = valor;
            Moeda = moeda;
        }
        public string Moeda { get; set; }
        public decimal Valor { get; set; }

        public static Money operator+ (Money a, Money b) 
        {
            if(!(a.Moeda == b.Moeda))
            {
                return null;
            }

            var resultado = new Money(a.Valor + b.Valor, a.Moeda);

            return resultado;
        }

        public static Money operator- (Money a, Money b)
        {
            if (!(a.Moeda == b.Moeda))
                return null;
            
            var resultado = new Money(a.Valor - b.Valor, a.Moeda);

            return resultado;
        }
    }
}