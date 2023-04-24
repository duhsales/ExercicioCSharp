using System.ComponentModel.DataAnnotations;

namespace Questao5
{
    public class SaldoConta 
    {
        public int Numero { get; set; }

        public string Nome { get; set; }

        public DateTime Data { get; set; }

        public double Saldo { get; set; }
    }
}