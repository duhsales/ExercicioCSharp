namespace Questao5
{
    public interface IRepositoryMovimento<Movimento>
    {
        Task<Movimento> Add(Movimento item);

        Task<string> GetSaldo(string Conta);
    }
}