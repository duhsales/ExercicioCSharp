using MediatR;
using Microsoft.AspNetCore.Http;
using static NSubstitute.Arg;
using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;
using Newtonsoft.Json.Linq;

namespace Questao5
{
    public class ConsultaMovimentoCommandHandler : IRequestHandler<ConsultaMovimentoCommand, SaldoConta>
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryMovimento<Movimento> _repository;
        private readonly IRepositoryContaCorrente<ContaCorrente> _repConta;
        public ConsultaMovimentoCommandHandler(IMediator mediator, IRepositoryMovimento<Movimento> repository, IRepositoryContaCorrente<ContaCorrente> repConta)
        {
            this._mediator = mediator;
            this._repository = repository;
            this._repConta = repConta;
        }

        public async Task<SaldoConta> Handle(ConsultaMovimentoCommand request, CancellationToken cancellationToken)
        {
            var saldo = new SaldoConta();

            try
            {
                var contaCorrente = await _repConta.GetById(request.IdContaCorrente);

                if (string.IsNullOrEmpty(contaCorrente.IdContaCorrente))
                {
                    throw new Exception("Apenas contas correntes cadastradas podem receber movimentação; TIPO: INVALID_ACCOUNT");
                }
                else if (contaCorrente.Ativo == 0)
                {
                    throw new Exception("Apenas contas correntes ativas podem receber movimentação; TIPO: INACTIVE_ACCOUNT");
                }
                else
                {
                    string strSaldo = await _repository.GetSaldo(request.IdContaCorrente);
                    saldo.Numero = contaCorrente.Numero;
                    saldo.Nome = contaCorrente.Nome;
                    saldo.Data = DateTime.Now;
                    saldo.Saldo = Math.Round(Convert.ToDouble(strSaldo), 2);

                    return saldo;
                }
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new ErroNotification { Excecao = ex.Message, PilhaErro = ex.StackTrace });
                return saldo;
            }
        }
    }
}