﻿using Inspire.Annotator;
using Inspire.Modeller;
using Microsoft.EntityFrameworkCore;

namespace Inspire.Services.Infrastructure.Common
{
   
    public abstract class MakerService<TEntity, TMap, T, TDb, TFilter>(TDb context) : RecordService<TEntity, TMap, T, TDb, TFilter>(context), Application.Common.IMakerService<TEntity, TMap, T, TFilter>
        where TEntity : Maker<T>, new()
        where TMap : MakerDto<T>
        where T : IEquatable<T>
        where TDb : DbContext
        where TFilter : RecordFilter
    {

        
        public override bool ValidateDeleteOnCreator(T id, string user)
        {
            return !Any(s => s.Id.Equals(id) && s.CreatedBy.ToUpper() == user.ToUpper());
        }
        protected override void AppendCreator(TEntity row, string createdBy)
        {
            row.CreatedBy = createdBy.ToUpper();
            row.DateCreated = DateTime.UtcNow.AddHours(2);
        }

    }
}