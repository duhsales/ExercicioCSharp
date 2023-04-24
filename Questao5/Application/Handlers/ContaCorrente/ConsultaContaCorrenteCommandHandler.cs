using MediatR;
using Microsoft.AspNetCore.Http;
using static NSubstitute.Arg;
using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

namespace Questao5
{
    public class ConsultaContaCorrenteCommandHandler : IRequestHandler<ConsultaContaCorrenteCommand, ContaCorrente>
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryContaCorrente<ContaCorrente> _repository;
        public ConsultaContaCorrenteCommandHandler(IMediator mediator, IRepositoryContaCorrente<ContaCorrente> repository)
        {
            this._mediator = mediator;
            this._repository = repository;
        }

        public async Task<ContaCorrente> Handle(ConsultaContaCorrenteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _repository.GetById(request.IdContaCorente);
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new ErroNotification { Excecao = ex.Message, PilhaErro = ex.StackTrace });
                return new ContaCorrente();
            }
        }
    }
}