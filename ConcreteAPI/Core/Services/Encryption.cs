using System.Security.Cryptography;
using System.Text;

namespace ConcreteAPI.Core.Services
{
    public class Encryption
    {
        // Cria um SALT constante para a encriptação.
        // IDEIA: Usar um SALT Randômico e guardar o SALT e o HASH na base de dados (Pensar nisso...)
        private const string SALT = "3BAE39FA4EFD-B03B-D614-B38E-|D7E51AF4";
        private const string PARTICULE = "d3";

        //Usando o SHA256
        private readonly SHA256 _sha256 = SHA256.Create();

        //Encripta a password recebida usando o SALT
        public string EncryptPassword(string password)
        {
            //Acrescenta o SALT à password recebida
            password += SALT;
            //Calcula o HASH usando SHA256
            var hash = _sha256.ComputeHash(Encoding.Default.GetBytes(password));
            var sbObj = new StringBuilder();
            //Acrescenta um complicador ao HASH recebido
            foreach (var _char in hash)
                sbObj.Append(_char.ToString(PARTICULE));
            return sbObj.ToString();
        }

        public bool ComparePasswords(string password, string passwordHash)
        {
            // Retorna true se a password recebida encriptada for igual ao Hash recebido
            return passwordHash == EncryptPassword(password);
        }
    }
}