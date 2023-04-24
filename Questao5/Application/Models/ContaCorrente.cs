using System.ComponentModel.DataAnnotations;

namespace Questao5
{
    public class ContaCorrente : IValidatableObject
    {
        public string IdContaCorrente { get; set; }

        public int Numero { get; set; }

        public string Nome { get; set; }

        public int Ativo { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (string.IsNullOrEmpty(Nome))
            {
                results.Add(new ValidationResult("É necessário digitar um nome."));
            }

            if (Numero <= 0)
            {
                results.Add(new ValidationResult("Conta inválida."));
            }

            return results;
        }
    }
}