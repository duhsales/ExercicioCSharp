using MediatR;

namespace Questao5
{
    public class AlteraContaCorrenteCommand : IRequest<string>
    {
        public string IdContaCorrente { get; set; }

        public string Nome { get; set; }

        public int Ativo { get; set; }
    }
}