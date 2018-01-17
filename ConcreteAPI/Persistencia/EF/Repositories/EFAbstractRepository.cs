﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using ConcreteAPI.Core.Common;
using ConcreteAPI.Persistence.EF.Context;
using FluentValidation.Results;

namespace ConcreteAPI.Persistence.EF.Repositories
{
    public abstract class EFAbstractRepository<TEntity>
        where TEntity : class
    {
        protected readonly ConcreteAPIContext Context;
        protected readonly DbSet<TEntity> Set;

        protected EFAbstractRepository(ConcreteAPIContext context)
        {
            Context = context;
            Set = context.Set<TEntity>();
        }

        protected async Task<CommandReturn> AddAsync(TEntity entity)
        {
            try
            {
                Set.Add(entity);
                await Context.SaveChangesAsync();
                return Success();
            }
            catch(DbEntityValidationException eve)
            {
                return Error(eve.EntityValidationErrors.SelectMany(validation => validation.ValidationErrors));
            }
        }

        protected async Task<CommandReturn> ChangeAsync(TEntity entity)
        {
            try
            {
                Context.Entry(entity).State = EntityState.Modified;
                await Context.SaveChangesAsync();
                return Success();
           }
            catch (DbEntityValidationException eve)
            {
                return Error(eve.EntityValidationErrors.SelectMany(validation => validation.ValidationErrors));
            }
        }

        protected CommandReturn Success()
        {
            return new CommandReturn();
        }

        protected CommandReturn Error(string errorMessage)
        {
            return new CommandReturn(errorMessage);
        }

        protected CommandReturn Error(IEnumerable<DbValidationError> dbValidationErrors)
       {
            var failures = new List<ValidationFailure>();

            foreach (var dbValidationError in dbValidationErrors)
                failures.Add(new ValidationFailure(dbValidationError.PropertyName?.Trim() ?? "", dbValidationError.ErrorMessage?.Trim()));

           return new CommandReturn(failures);
        }
    }
}