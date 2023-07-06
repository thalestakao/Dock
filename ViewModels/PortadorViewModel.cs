using System.ComponentModel.DataAnnotations;

namespace Dock.ViewModels
{
    public class PortadorViewModel
    {
        [Required]
        public string Nome { get; set; } = string.Empty;
        [Required]
        public string Cpf { get; set; } = string.Empty;
    }
}
