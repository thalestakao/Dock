using Dock.Domain.Entities.Cliente;
using Dock.Infra.Data;
using Dock.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dock.Controllers
{
    public class PortadorController : ControllerBase
    {

        [HttpPost("v1/portador")]
        public async Task<IActionResult> Post(
            [FromServices] DockContext context,
            [FromBody] PortadorViewModel portadorVm
            ) {

            var portador = Portador.Create(portadorVm.Nome, Cpf.Create(portadorVm.Cpf));
            try
            {
                await context.AddAsync(portador);
                await context.SaveChangesAsync();

            }
            catch(Exception ex)
            {
                return StatusCode(500, "001 - Falha ao gravar o portador/cliente.");
            }
            
            return Created("",new ResultViewModel<Portador>(portador));
        }

        [HttpGet("v1/portador")]
        public async Task<IActionResult> Get(
                [FromServices] DockContext context
            )
        {

            var portadores = context.Portadores.ToList();
            
            if (portadores is null || portadores.Count == 0)
                return NotFound(new ResultViewModel<string>("Não foi possível encontrar portadores"));

            return Ok(new ResultViewModel<List<Portador>>(portadores));
        }

        [HttpGet("v1/portador/{id:Guid}")]
        public async Task<IActionResult> GetById(
                Guid id,
                [FromServices] DockContext context
            )
        {
            var portador = await context.Portadores.FirstOrDefaultAsync(p => p.Id.Equals(new PortadorId(id)));
            
            if (portador is null)
                return NotFound(new ResultViewModel<string>($"Não foi possível encontrar o portador de id {id}."));
            
            return Ok(new ResultViewModel<Portador>(portador));
        }

        //[HttpGet("v1/portador/{cpf:string}")]
        //public async Task<IActionResult> GetByCpf(
        //    string cpf,
        //    [FromServices] DockContext context
        //    )
        //{
        //    var portador = context.Portadores.SingleOrDefault(p => p.Cpf.ValorCompleto == cpf);
        //    if (portador is null)
        //        return NotFound(new ResultViewModel<string>($"Não foi possível encontrar o portador de cpf {cpf}."));

        //    return Ok(new ResultViewModel<Portador>(portador));
        //}

    }
}
