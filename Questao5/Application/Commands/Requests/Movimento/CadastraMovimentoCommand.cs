using MediatR;

namespace Questao5
{
    public class CadastraMovimentoCommand : IRequest<string>
    {
        public string IdContaCorrente { get; set; }

        public string TipoMovimento { get; set; }

        public double Valor { get; set; }
    }
}