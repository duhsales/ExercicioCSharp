using MediatR;
using Questao5;

namespace Questao5
{
    public class MovimentoCriadoNotification : INotification
    {
        public string IdMovimento { get; set; }

        public string IdContaCorrente { get; set; }

        public string DataMovimento { get; set; }

        public string TipoMovimento { get; set; }

        public double Valor { get; set; }
    }
}