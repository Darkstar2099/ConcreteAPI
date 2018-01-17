namespace ConcreteAPI.Core.Constants
{
    public static class UserProperties
    {
        //Constante com o tamanho mínimo do atributo Name(User)
        public const int NameMinLen = 3;
        //Constante com o tamanho máximo do atributo Name(User)
        public const int NameMaxLen = 50;
        //Constante com o tamanho mínimo do atributo Email(User)
        public const int EmailMinLen = 5;
        //Constante com o tamanho máximo do atributo Email(User)
        public const int EmailMaxLen = 255;
        //Constante com o tamanho máximo do atributo Password(User)
        public const int PasswordMaxLen = 8000;
        //Constante com o tamanho maáximo do atributo Token(User)
        public const int TokenMaxLen = 8000;
    }
}