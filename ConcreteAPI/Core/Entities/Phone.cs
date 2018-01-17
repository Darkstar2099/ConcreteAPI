using System;
using ConcreteAPI.Core.Extensions;

namespace ConcreteAPI.Core.Entities
{
    public class Phone
    {
        private string _ddd;
        private string _number;

        protected Phone()
        {
            Id = Guid.NewGuid();
        }

        public Phone(Guid userId, string ddd, string number)
            : this()
        {
            UserId = userId;
            Ddd = ddd;
            Number = number;
        }

        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public string Ddd
        {
            get { return _ddd; }
            set { _ddd = value.OnlyNumbers(); }
        }

        public string Number
        {
            get { return _number; }
            set { _number = value.OnlyNumbers(); }
        }
    }
}