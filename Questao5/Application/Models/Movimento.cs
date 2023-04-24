using FluentAssertions.Equivalency;
using System.ComponentModel.DataAnnotations;

namespace Questao5
{
    public class Movimento : IValidatableObject
    {
        public string IdMovimento { get; set; }

        public string IdContaCorrente { get; set; }

        public string DataMovimento { get; set; }
        
        public string TipoMovimento { get; set; }

        public double Valor { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (TipoMovimento.ToUpper() != "C" && TipoMovimento.ToUpper() != "D")
            {
                results.Add(new ValidationResult("Apenas valores positivos podem ser recebidos; TIPO: INVALID_VALUE"));
            }

            if (Math.Round(Valor, 2) <= 0.00)
            {
                results.Add(new ValidationResult("Apenas os tipos “débito” ou “crédito” podem ser aceitos; TIPO: INVALID_TYPE"));
            }

            return results;
        }
    }
}