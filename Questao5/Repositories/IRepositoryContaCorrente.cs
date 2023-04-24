namespace Questao5
{
    public interface IRepositoryContaCorrente<ContaCorrente>
    {
        Task<ContaCorrente> Add(ContaCorrente item);

        Task<ContaCorrente> GetById(string Conta);
    }
}