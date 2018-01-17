namespace ConcreteAPI.Core.Common
{
    public class Email
    {
        public Email(string email)
        {
            Value = email;
        }

        public string Value { get; }
    }
}