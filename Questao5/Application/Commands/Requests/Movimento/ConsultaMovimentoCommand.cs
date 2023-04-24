using MediatR;

namespace Questao5
{
    public class ConsultaMovimentoCommand : IRequest<SaldoConta>
    {
        public string IdContaCorrente { get; set; }
    }
}