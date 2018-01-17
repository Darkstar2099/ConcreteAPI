using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConcreteAPI.Core.Services;

namespace ConcreteAPI.Tests.Core.Services
{
    [TestClass]
    public class EncryptionTests
    {
        [TestMethod]
        public void RetornaPasswordEncriptada_RetornaPasswordEncriptadaDiferenteDaPassword()
        {
            // Arrange
            var password = Guid.NewGuid().ToString();

            // Act
            var encryptedPass = new Encryption().EncryptPassword(password);

            // Assert
            Assert.AreNotEqual(password, encryptedPass);
        }

        [TestMethod]
        public void GeraHashesIguaisVariasVezes_RetornaEqualParaTodos()
        {
            // Arrange
            var encryption = new Encryption();
            var password = Guid.NewGuid().ToString();

            // Act
            var hash1 = new Encryption().EncryptPassword(password);
            var hash2 = encryption.EncryptPassword(password);
            var hash3 = new Encryption().EncryptPassword(password);
            var hash4 = encryption.EncryptPassword(password);

            // Assert
            //Comparar todos os hash entre si. Deve sempre retornar que são iguais.
            Assert.AreEqual(hash1, hash2, "Hash 1 e 2 são diferentes");
            Assert.AreEqual(hash3, hash4, "Hash 3 e 4 são diferentes");
            Assert.AreEqual(hash1, hash3, "Hash 1 e 3 são diferentes");
            Assert.AreEqual(hash1, hash4, "Hash 1 e 4 são diferentes");
            Assert.AreEqual(hash2, hash3, "Hash 2 e 3 são diferentes");
            Assert.AreEqual(hash2, hash4, "Hash 2 e 4 são diferentes");
        }

        [TestMethod]
        public void VerificaPasswordEncriptada_RetornaTrue()
        {
            // Arrange
            var password = Guid.NewGuid().ToString();
            var encryption = new Encryption();

            // Act
            var hash = encryption.EncryptPassword(password);

            // Assert
            Assert.IsTrue(encryption. ComparePasswords(password, hash));
        }
    }
}
