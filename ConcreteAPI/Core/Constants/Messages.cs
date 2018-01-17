namespace ConcreteAPI.Core.Constants
{
    public static class Messages
    {
        // Mensagens de Error para Login e SignUp
        public const string EmailAlreadyUsed = "Email já existe.";
        public const string UserNotRegistered = "Usuário Não Cadastrado.";
        public const string UserOrPassNotValid = "Usuário e/ou senha inválidos.";

        // Mensagens de Erro para o tratamento de Token
        public const string DifferentTokens = "Não autorizado.";
        public const string ExpiredToken = "Sessão inválida.";
        public const string TokenNotFound = "Não autorizado.";

        // Mensagens de Erro de Comando Inválido
        public const string InvalidCommand = "Comando inválido.";
    }
}