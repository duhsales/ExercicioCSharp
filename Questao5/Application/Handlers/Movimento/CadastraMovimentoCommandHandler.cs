using MediatR;
using Microsoft.AspNetCore.Http;
using static NSubstitute.Arg;
using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

namespace Questao5
{
    public class CadastraMovimentoCommandHandler : IRequestHandler<CadastraMovimentoCommand, string>
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryMovimento<Movimento> _repository;
        private readonly IRepositoryContaCorrente<ContaCorrente> _repConta;
        public CadastraMovimentoCommandHandler(IMediator mediator, IRepositoryMovimento<Movimento> repository, IRepositoryContaCorrente<ContaCorrente> repConta)
        {
            this._mediator = mediator;
            this._repository = repository;
            this._repConta = repConta;
        }

        public async Task<string> Handle(CadastraMovimentoCommand request, CancellationToken cancellationToken)
        {
            var movimento = new Movimento { IdMovimento = Guid.NewGuid().ToString().ToUpper(), DataMovimento = DateTime.Now.Date.ToShortDateString(), IdContaCorrente = request.IdContaCorrente, TipoMovimento = request.TipoMovimento, Valor = request.Valor };
            string strErro = string.Empty;
            try
            {
                ValidationContext context = new ValidationContext(movimento, null, null);
                IEnumerable<ValidationResult> validationResult = movimento.Validate(context);
                
                if (validationResult.Count() > 0)
                {
                    foreach (var item in validationResult)
                    {
                        strErro += item.ErrorMessage + "\r\n ";
                    }

                    if (strErro.Length > 0)
                    {
                        strErro = strErro.Remove(strErro.Length - 3);
                        throw new Exception(strErro);
                    }
                }

                var contaCorrente = await _repConta.GetById(movimento.IdContaCorrente);

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
                    movimento = await _repository.Add(movimento);

                    await _mediator.Publish(new MovimentoCriadoNotification { IdMovimento = movimento.IdMovimento, DataMovimento = movimento.DataMovimento, IdContaCorrente = movimento.IdContaCorrente, TipoMovimento = movimento.TipoMovimento, Valor = movimento.Valor });

                    return await Task.FromResult("Movimentação realizada com sucesso.");
                }
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new ErroNotification { Excecao = ex.Message, PilhaErro = ex.StackTrace });
                return await Task.FromResult("Ocorreu um erro na movimentação.");
            }
        }
    }
}