namespace Dock.Domain.Entities.Cliente
{
    public class Cpf
    {
        public string Digitos { get; private set; }
        public string DigitoVerificador { get; private set; }

        public string ValorCompleto => Digitos + DigitoVerificador;

        private Cpf() { }

        public static Cpf Create(string digitos, string digitoVerificador)
        {
            //Todo - fazer validação.
            return new Cpf
            {
                Digitos = digitos,
                DigitoVerificador = digitoVerificador
            };
        }

        public static Cpf Create(string todosDigitos)
        {
            todosDigitos = todosDigitos.Replace(".", "").Replace("-","");
            var digitos = todosDigitos.Substring(0, 9);
            var vf = todosDigitos.Substring(9, 2);
            return new Cpf { Digitos = digitos, DigitoVerificador = vf };

        }
    }
}
