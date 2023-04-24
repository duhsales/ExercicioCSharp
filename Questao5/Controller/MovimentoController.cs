using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Questao5
{
    [ApiController]
    [Route("[controller]")]
    public class MovimentoController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryMovimento<Movimento> _repository;

        public MovimentoController(IMediator mediator, IRepositoryMovimento<Movimento> repository)
        {
            this._mediator = mediator;
            this._repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CadastraMovimentoCommand command)
        {
            string response = await _mediator.Send(command);

            if (response.Contains("erro"))
            {
                return BadRequest(response);
            }
            else
            {
                return Ok(response);
            }
        }

        [HttpGet("{Conta}")]
        public async Task<IActionResult> GetSaldo(string Conta)
        {
            ConsultaMovimentoCommand consulta = new ConsultaMovimentoCommand();
            consulta.IdContaCorrente = Conta;

            var response = await _mediator.Send(consulta);

            return Ok(response);
        }
    }
}