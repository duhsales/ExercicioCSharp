using System.Globalization;

namespace Questao1
{
    class ContaBancaria
    {
        private double _dblSaldo { get; set; }

        private double _dblTaxa = 3.50;

        private int _iNumero = 0;

        private string _strTitular = string.Empty;
    
        public ContaBancaria(int numero, string titular, double depositoInicial)
        {
            _iNumero = numero;
            _strTitular = titular;
            _dblSaldo = depositoInicial;
        }

        public ContaBancaria(int numero, string titular)
        {
            _iNumero = numero;
            _strTitular = titular;
        }

        public void Deposito(double quantia) 
        {
            _dblSaldo += quantia;
        }

        public void Saque(double quantia) 
        {
            _dblSaldo -= quantia + _dblTaxa;
        }

        public string getSaldo()
        {
            return string.Format("Conta: {0}, Titular: {1}, Saldo: {2:C2}", _iNumero, _strTitular, _dblSaldo);
        }
    }
}
