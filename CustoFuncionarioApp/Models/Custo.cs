namespace CustoFuncionarioApp.Models
{
    public class Custo
    {
        public decimal SalarioBruto { get; set; }
        public decimal PlanoSaude { get; set; }
        public decimal SeguroVida { get; set; }
        public decimal OutrosBeneficios { get; set; }

        public decimal getINSS_Aliquota()
        {
            if (SalarioBruto < 0.01M)
                throw new ArgumentOutOfRangeException("Salário não pode ser negativo.");

            if (SalarioBruto <= 1412.00M) return 7.5M;
            if (SalarioBruto >= 1412.01M && SalarioBruto <= 2666.68M) return 9M;
            if (SalarioBruto >= 2666.69M && SalarioBruto <= 4000.03M) return 12M;
            if (SalarioBruto >= 4000.04M && SalarioBruto <= 7786.02M) return 14M;
            return 0M;
        }

        public decimal getINSS_Valor()
        {
            if (SalarioBruto < 0)
                throw new ArgumentOutOfRangeException("Salário não pode ser negativo.");

            return this.SalarioBruto * this.getINSS_Aliquota() / 100;
        }

        public decimal getFGTS()
        {
            if (SalarioBruto < 0)
                throw new ArgumentOutOfRangeException("Salário não pode ser negativo.");

            return this.SalarioBruto * 0.08M;
        }

        public decimal get13oSalario()
        {
            if (SalarioBruto < 0)
                throw new ArgumentOutOfRangeException("Salário não pode ser negativo.");

            return this.SalarioBruto;
        }

        public decimal getFerias()
        {
            if (SalarioBruto < 0)
                throw new ArgumentOutOfRangeException("Salário não pode ser negativo.");

            return this.SalarioBruto + this.SalarioBruto / 3;
        }

        public decimal getPercentualDespesa(decimal valorDespesa)
        {
            if (valorDespesa < 0)
                throw new ArgumentOutOfRangeException("Valor da despesa não pode ser negativo.");

            if (this.getCustoTotal() == 0)
                throw new ArgumentOutOfRangeException("Custo total não pode ser zero.");

            return (valorDespesa / this.getCustoTotal()) * 100;
        }

        public decimal getCustoTotal()
        {
            if (SalarioBruto < 0.01M || PlanoSaude < 0 || SeguroVida < 0 || OutrosBeneficios < 0)
                throw new ArgumentOutOfRangeException("Nenhum dos valores pode ser negativo.");

            return this.SalarioBruto +
                   this.getINSS_Valor() +
                   this.getFGTS() +
                   this.get13oSalario() +
                   this.getFerias() +
                   this.PlanoSaude +
                   this.SeguroVida +
                   this.OutrosBeneficios;
        }
    }

}
