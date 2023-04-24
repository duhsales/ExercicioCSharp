using MediatR;

namespace Questao5
{
    public class ContaCorrenteCriadaNotification : INotification
    {
        public string IdContaCorrente { get; set; }

        public int Numero { get; set; }

        public string Nome { get; set; }

        public int Ativo { get; set; }
    }
}