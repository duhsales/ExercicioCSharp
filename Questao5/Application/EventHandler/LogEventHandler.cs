using MediatR;

namespace Questao5
{
    public class LogEventHandler : INotificationHandler<ContaCorrenteAlteradaNotification>,
                                INotificationHandler<ContaCorrenteCriadaNotification>,
                                INotificationHandler<ErroNotification>
    {
        public Task Handle(ContaCorrenteAlteradaNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"ALTERADA: '{notification.Nome} - {notification.Numero} - {notification.Ativo}'");
            });
        }

        public Task Handle(ContaCorrenteCriadaNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine(string.Format("Conta criada com sucesso! Titular: {0} - Conta: {1}", notification.Nome, notification.Numero));
            });
        }

        public Task Handle(ErroNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"ERRO: '{notification.Excecao} \n {notification.PilhaErro}'");
            });
        }
    }
}