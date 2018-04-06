using System;
using DesafioTecGraf.Domain.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DesafioTecGraf.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CalcularDigito()
        {
            //   Arrange     
            const string matricula = "9876";
            var matriculaService = new MatriculaService();

            //  Act
            var matriculas = matriculaService.CalcularDigito(matricula);

            //  Assert
            Assert.IsTrue(!matriculas.MatriculaDigito.Equals(""));
        }

        [TestMethod]
        public void ValidatMatriculasTrue()
        {
            //   Arrange     
            const string matricula = "1653-2";
            var matriculaService = new MatriculaService();

            //  Act
            var validarmatriculas = matriculaService.ValidatMatriculas(matricula);

            //  Assert
            Assert.IsTrue(validarmatriculas);
        }

        [TestMethod]
        public void ValidatMatriculasFalse()
        {
            //   Arrange     
            const string matricula = "1653-3";
            var matriculaService = new MatriculaService();

            //  Act
            var validarmatriculas = matriculaService.ValidatMatriculas(matricula);

            //  Assert
            Assert.IsFalse(validarmatriculas);
        }
    }
}
