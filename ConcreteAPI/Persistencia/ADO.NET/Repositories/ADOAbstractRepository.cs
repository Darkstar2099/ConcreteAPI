﻿using ConcreteAPI.Core.Common;

namespace ConcreteAPI.Persistence.ADO.NET.Repositories
{
    public abstract class ADOAbstractRepository
    {
        protected readonly ConcreteAPIDbConnection Connection;

        protected ADOAbstractRepository(ConcreteAPIDbConnection connection)
        {
            Connection = connection;
        }

        protected CommandReturn Success()
        {
            return new CommandReturn();
        }

        protected CommandReturn Error(string errorMessage)
        {
            return new CommandReturn(errorMessage);
        }
    }
}