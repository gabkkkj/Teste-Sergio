using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustoFuncionarioApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustoFuncionarioApp.Models.Tests
{
    [TestClass()]
    public class CustoTests
    {
        private Custo custo;

        [TestInitialize()]
        public void Inicializar()
        {
            custo = new Custo();
        }

        [TestMethod()]
        [DataRow(-200.00)]  // Salário negativo
        [DataRow(0.00)]     // Salário zero
        public void getCustoTotalTest_Exception(double salario)
        {
            custo.SalarioBruto = (decimal)salario;
            custo.PlanoSaude = 250;
            custo.SeguroVida = 150;
            custo.OutrosBeneficios = 60;

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => custo.getCustoTotal());
        }

        [TestMethod()]
        [DataRow(-500.00)]  // Despesa negativa
        [DataRow(0.00)]     // Custo total zero
        public void getPercentualDespesaTest_Exception(double valorDespesa)
        {
            custo.SalarioBruto = 0;  // Forçando o custo total a ser zero
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => custo.getPercentualDespesa((decimal)valorDespesa));
        }

        [TestMethod()]
        [DataRow(-10.00)]  // Salário negativo
        public void getFeriasTest_Exception(double salario)
        {
            custo.SalarioBruto = (decimal)salario;
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => custo.getFerias());
        }

        [TestMethod()]
        [DataRow(-100.00)]  // Salário negativo
        public void get13oSalarioTest_Exception(double salario)
        {
            custo.SalarioBruto = (decimal)salario;
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => custo.get13oSalario());
        }

        [TestMethod()]
        [DataRow(-25.00)]  // Salário negativo
        public void getFGTSTest_Exception(double salario)
        {
            custo.SalarioBruto = (decimal)salario;
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => custo.getFGTS());
        }

        [TestMethod()]
        [DataRow(-15.00)]  // Salário negativo
        public void getINSS_ValorTest_Exception(double salario)
        {
            custo.SalarioBruto = (decimal)salario;
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => custo.getINSS_Valor());
        }

        [TestMethod()]
        [DataRow(-50.00)]  // Salário negativo
        [DataRow(-500.00)] // Salário negativo
        public void getINSS_AliquotaTest_Exception(double salario)
        {
            custo.SalarioBruto = (decimal)salario;
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => custo.getINSS_Aliquota());
        }

        [TestMethod()]
        [DataRow(1412.00, 5275.53)]
        [DataRow(2666.68, 9692.27)]
        [DataRow(4000.03, 14483.44)]
        [DataRow(7786.02, 28016.32)]
        [DataRow(10000.00, 34483.33)]
        public void getCustoTotalTest(double salario, double esperado)
        {
            custo.SalarioBruto = (decimal)salario;
            // Definindo valores fixos para outros benefícios, plano de saúde e seguro de vida
            custo.PlanoSaude = 200;
            custo.SeguroVida = 100;
            custo.OutrosBeneficios = 50;

            var obtido = custo.getCustoTotal();
            var Esperado = (decimal)esperado;

            Assert.AreEqual(Esperado, obtido, 2);
        }

        [TestMethod()]
        [DataRow(1412.00, 112.96, 2.14)] // FGTS como exemplo de despesa
        [DataRow(2666.68, 213.33, 2.12)]
        [DataRow(4000.03, 320.00, 2.08)]
        [DataRow(7786.02, 622.88, 2.02)]
        [DataRow(10000.00, 800.00, 2.11)]
        public void getPercentualDespesaTest(double salario, double valorDespesa, double esperado)
        {
            custo.SalarioBruto = (decimal)salario;
            custo.PlanoSaude = 200;
            custo.SeguroVida = 100;
            custo.OutrosBeneficios = 50;

            var obtido = custo.getPercentualDespesa((decimal)valorDespesa);
            var Esperado = (decimal)esperado;

            Assert.AreEqual(Esperado, obtido, 2);
        }

        [TestMethod()]
        [DataRow(1412.00, 1882.67)]
        [DataRow(1412.01, 1882.68)]
        [DataRow(2666.68, 3555.57)]
        [DataRow(2666.69, 3555.59)]
        [DataRow(4000.03, 5333.37)]
        [DataRow(4000.04, 5333.39)]
        [DataRow(7786.02, 10381.36)]
        [DataRow(10000.00, 13333.33)]
        public void getFeriasTest(double salario, double esperado)
        {
            custo.SalarioBruto = (decimal)salario;
            var obtido = custo.getFerias();
            var Esperado = (decimal)esperado;

            Assert.AreEqual(Esperado, obtido, 2);
        }

        [TestMethod()]
        [DataRow(1412.00, 1412.00)]
        [DataRow(1412.01, 1412.01)]
        [DataRow(2666.68, 2666.68)]
        [DataRow(2666.69, 2666.69)]
        [DataRow(4000.03, 4000.03)]
        [DataRow(4000.04, 4000.04)]
        [DataRow(7786.02, 7786.02)]
        [DataRow(10000.00, 10000.00)]
        public void get13oSalarioTest(double salario, double esperado)
        {
            custo.SalarioBruto = (decimal)salario;
            var obtido = custo.get13oSalario();
            var Esperado = (decimal)esperado;

            Assert.AreEqual(Esperado, obtido, 2);
        }

        [TestMethod()]
        [DataRow(1412.00, 112.96)]
        [DataRow(1412.01, 112.96)]
        [DataRow(2666.68, 213.33)]
        [DataRow(2666.69, 213.34)]
        [DataRow(4000.03, 320.00)]
        [DataRow(4000.04, 320.00)]
        [DataRow(7786.02, 622.88)]
        [DataRow(10000.00, 800.00)]
        public void getFGTSTest(double salario, double esperado)
        {
            custo.SalarioBruto = (decimal)salario;
            var obtido = custo.getFGTS();
            var Esperado = (decimal)esperado;

            Assert.AreEqual(Esperado, obtido, 2);
        }

        [TestMethod()]
        [DataRow(1412.00, 105.9)]
        [DataRow(1412.01, 127.08)]
        [DataRow(2666.68, 240.00)]
        [DataRow(2666.69, 320.00)]
        [DataRow(4000.03, 480.00)]
        [DataRow(4000.04, 560.01)]
        [DataRow(7786.02, 1089.04)]
        [DataRow(10000.00, 0.0)]
        public void getINSS_ValorTest(double salario, double esperado)
        {
            custo.SalarioBruto = (decimal)salario;
            var obtido = custo.getINSS_Valor();
            var Esperado = (decimal)esperado;

            Assert.AreEqual(Esperado, obtido, 2);
        }

        [TestMethod()]
        [DataRow(1412.00, 7.5)]
        [DataRow(1412.01, 9.0)]
        [DataRow(2666.68, 9.0)]
        [DataRow(2666.69, 12.0)]
        [DataRow(4000.03, 12.0)]
        [DataRow(4000.04, 14.0)]
        [DataRow(7786.02, 14.0)]
        [DataRow(10000.00, 0)]
        public void getINSS_AliquotaTest(double salario, double Esperado)
        {
            custo.SalarioBruto = (decimal)salario;

            var obtido = custo.getINSS_Aliquota();
            var esperado = (decimal)Esperado;

            Assert.AreEqual(esperado, obtido);
        }
    }

}