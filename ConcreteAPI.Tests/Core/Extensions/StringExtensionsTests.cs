using System;
using ConcreteAPI.Core.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConcreteAPI.Tests.Core.Extensions
{
    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        public void RetornaSomenteNumeros_SomenteNumerosSaoRetornados()
        {
            // Act
            var numbers = "ABCDEFG0123456hijklmno789".OnlyNumbers();

            // Assert
            Assert.AreEqual("0123456789", numbers);
        }

        [TestMethod]
        public void RetornaEmptySeStringNula_RetornaEmpty()
        {
            // Act
            var numbers = ((string)null).OnlyNumbers();

            // Assert
            Assert.IsTrue(numbers == string.Empty);
        }

        [TestMethod]
        public void RetornaEmptySeStringVazia_RetornaEmpty()
        {
            // Act
            var numbers = "".OnlyNumbers();

            // Assert
            Assert.IsTrue(numbers == string.Empty);
        }

        [TestMethod]
        public void RetornaTrueSeStringsIguais_RetornaTrue()
        {
            // Act
            var value = Guid.NewGuid().ToString("N");

            // Assert
            Assert.IsTrue(value.CaselessCompare(value));
        }

        [TestMethod]
        public void RetornaTrueSeStringsIguaisIndependenteDoCase_RetornaTrue()
        {
            // Arrange
            var guid = Guid.NewGuid();

            // Act
            var value1 = guid.ToString("N");
            var value2 = value1.ToUpper();

            // Assert
            Assert.IsTrue(value1.CaselessCompare(value2));
        }

        [TestMethod]
        public void RetornaFalseSeStringsDiferentes_RetornaFalse()
        {
            // Arrange
            var guid = Guid.NewGuid();

            // Act
            var value1 = guid.ToString();
            var value2 = value1 + " ";

            // Assert
            Assert.IsFalse(value1.CaselessCompare(value2));
        }

        [TestMethod]
        public void RetornaFalseSeCompararStringNula_RetornaFalse()
        {
            // Assert
            Assert.IsFalse(((string)null).CaselessCompare(""));
        }
    }
}
