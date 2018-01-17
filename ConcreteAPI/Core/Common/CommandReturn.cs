﻿using System.Collections.Generic;
using System.Linq;
using System.Net;
using ConcreteAPI.Core.Extensions;
using FluentValidation.Results;

namespace ConcreteAPI.Core.Common
{
    public class CommandReturn
    {
        private readonly List<ValidationFailure> _failures = new List<ValidationFailure>();

        // ___/ Constructors \_________

        // Se for chamados sem parâmetros, inicializa ErroCode com (400) BadRequest
        public CommandReturn()
        {
            ErrorCode = HttpStatusCode.BadRequest;
        }

        // Se for chamado passando um objeto, retorna o próprio objeto
        public CommandReturn(object data)
            : this()
        {
            Data = data;
        }

        // Se for chamado passando uma Mensagem de Erro, adiciona ela às Falhas de Validação
        public CommandReturn(string errorMsg)
            : this()
        {
            AddFailure(errorMsg);
        }

        // Se for chamado passando um nome de propriedade e uma Mensagem de Erro, adiciona elas às Falhas de Validação
        public CommandReturn(string propertyName, string errorMessage)
            : this()
        {
            AddFailure(propertyName, errorMessage);
        }

        // Se for chamado passando um objeto ValidationFailure, adiciona ele às Falhas de Validação
        public CommandReturn(ValidationFailure validationFailure)
            : this()
        {
            AddFailure(validationFailure);
        }

        // Se for chamado passando um Enumerable de objetos ValidationFailure, adciona ele às Falhas de Validação
        public CommandReturn(IEnumerable<ValidationFailure> validationFailures)
            : this()
        {
            _failures.AddRange(validationFailures);
        }


        // ___/ Properties \_____
        public IReadOnlyCollection<ValidationFailure> Failures => _failures;
        public bool Success => _failures.IsEmpty();
        public bool Failed => _failures.Any();
        public HttpStatusCode ErrorCode { get; set; }
        public object Data { get; set; }


        // ___/ Methods \_________

        // Method: Adiciona Mensagem de Erro às Falhas de validação
        public void AddFailure(string errorMsg)
        {
            errorMsg = errorMsg?.Trim();

            if (errorMsg.IsEmpty())
                return;

            if (_failures.Any(x => x.PropertyName.IsEmpty() && x.ErrorMessage.CaselessCompare(errorMsg)))
                return;

            _failures.Add(new ValidationFailure(string.Empty, errorMsg));
        }

        // Method: Adiciona Nome da propriedade e Mensagem de Erro às Falhas de validação
        public void AddFailure(string propertyName, string errorMsg)
        {
            propertyName = propertyName?.Trim();
            errorMsg = errorMsg?.Trim();

            if (propertyName.IsEmpty() || errorMsg.IsEmpty())
                return;

            if (_failures.Any(x => x.PropertyName.CaselessCompare(propertyName) && x.ErrorMessage.CaselessCompare(errorMsg)))
                return;

            _failures.Add(new ValidationFailure(propertyName, errorMsg));
        }

        // Method: Adiciona objeto ValidationFailure às falhas de validação
        public void AddFailure(ValidationFailure validationFailure)
        {
            if (_failures.Contains(validationFailure))
                return;

            if (_failures.Any(x =>
                x.PropertyName.CaselessCompare(validationFailure.PropertyName) &&
                x.ErrorMessage.CaselessCompare(validationFailure.ErrorMessage)))
                return;

            _failures.Add(validationFailure);
        }

    }
}