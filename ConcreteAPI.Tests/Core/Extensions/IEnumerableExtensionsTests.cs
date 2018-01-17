using System.Collections.Generic;
using ConcreteAPI.Core.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConcreteAPI.Tests.Core.Extensions
{
    [TestClass]
    public class IEnumerableExtensions
    {
        [TestMethod]
        public void RetornaEmptyParaObjetoNulo_RetornaEmpty()
        {
            // Act
            var isEmpty = ((IEnumerable<object>)null).IsEmpty();

            // Assert
            Assert.IsTrue(isEmpty);
        }

        [TestMethod]
        public void RetornaEmptyCasoNaoTenhaCaracteres_RetornaEmpty()
        {
            // Act
            var isEmpty = string.Empty.IsEmpty();

            // Assert
            Assert.IsTrue(isEmpty);
        }
            
        [TestMethod]
        public void RetornaEmptyCasoListaNaoPopulada_RetornaEmpty()
        {
            // Act
            var isEmpty = new List<object>().IsEmpty();

            // Assert
            Assert.IsTrue(isEmpty);
        }
            
        [TestMethod]
        public void RetornaNotEmptyComListaPopulada_RetornaNotEmpty()
        {
            // Arrange
            var lista = new List<string> { "I´m not empty" };

            // Act
            var isEmpty = lista.IsEmpty();

            // Assert
            Assert.IsFalse(isEmpty);
        }
    }
}
