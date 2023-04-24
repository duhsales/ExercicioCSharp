using MediatR;

namespace Questao5
{
    public class ConsultaContaCorrenteCommand : IRequest<ContaCorrente>
    {
        public string IdContaCorente { get; set; }
    }
}