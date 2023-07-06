using System.ComponentModel.DataAnnotations;

namespace Dock.ViewModels
{
    public class PortadorViewModel
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Cpf { get; set; }
    }
}
