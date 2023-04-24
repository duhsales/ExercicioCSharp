using MediatR;

namespace Questao5
{
    public class CadastraContaCorrenteCommand : IRequest<string>
    {
        public int Numero { get; set; }

        public string Nome { get; set; }

        public int Ativo { get; set; }
    }
}