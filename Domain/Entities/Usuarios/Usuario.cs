namespace Dock.Domain.Entities.Usuarios
{
    public class Usuario
    {
        public UsuarioId Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        private Usuario()
        {

        }

        public static Usuario Create(string nome, string email, string passwordHash)
        {
            return new Usuario { Nome = nome, Email = email, PasswordHash = passwordHash };
        }
    }
}
