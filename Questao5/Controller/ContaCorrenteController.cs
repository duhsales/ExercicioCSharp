using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Questao5
{
    [ApiController]
    [Route("[controller]")]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryContaCorrente<ContaCorrente> _repository;

        public ContaCorrenteController(IMediator mediator, IRepositoryContaCorrente<ContaCorrente> repository)
        {
            this._mediator = mediator;
            this._repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CadastraContaCorrenteCommand command)
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
        public async Task<IActionResult> GetById(string Conta)
        {
            ConsultaContaCorrenteCommand consulta = new ConsultaContaCorrenteCommand();
            consulta.IdContaCorente = Conta;

            var response = await _mediator.Send(consulta);

            return Ok(response);
        }
    }
}