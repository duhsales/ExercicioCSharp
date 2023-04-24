using MediatR;
using Microsoft.AspNetCore.Http;
using static NSubstitute.Arg;
using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

namespace Questao5
{
    public class CadastraContaCorrenteCommandHandler : IRequestHandler<CadastraContaCorrenteCommand, string>
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryContaCorrente<ContaCorrente> _repository;
        public CadastraContaCorrenteCommandHandler(IMediator mediator, IRepositoryContaCorrente<ContaCorrente> repository)
        {
            this._mediator = mediator;
            this._repository = repository;
        }

        public async Task<string> Handle(CadastraContaCorrenteCommand request, CancellationToken cancellationToken)
        {
            var contaCorrente = new ContaCorrente { IdContaCorrente = Guid.NewGuid().ToString().ToUpper(), Numero = request.Numero, Nome = request.Nome, Ativo = request.Ativo };
            string strErro = string.Empty;

            try
            {
                ValidationContext context = new ValidationContext(contaCorrente, null, null);
                IEnumerable<ValidationResult> validationResult = contaCorrente.Validate(context);
                
                if (validationResult.Count() > 0)
                {
                    foreach (var item in validationResult)
                    {
                        strErro += item.ErrorMessage + "\r\n ";
                    }

                    if (strErro.Length > 0)
                    {
                        strErro = strErro.Remove(strErro.Length - 4);
                        throw new Exception(strErro);
                    }
                }

                contaCorrente = await _repository.Add(contaCorrente);

                await _mediator.Publish(new ContaCorrenteCriadaNotification { IdContaCorrente = contaCorrente.IdContaCorrente, Numero = contaCorrente.Numero, Nome = contaCorrente.Nome, Ativo = contaCorrente.Ativo });

                return await Task.FromResult("Conta Criada com sucesso.");
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new ErroNotification { Excecao = ex.Message, PilhaErro = ex.StackTrace });
                return await Task.FromResult("Ocorreu um erro no momento da criação");
            }
        }
    }
}