using Abp.Domain.Entities;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdakcc.EntityFrameworkCore
{
    public class sdakccSystemRepositoryBase<TEntity, TPrimaryKey> : EfCoreRepositoryBase<sdakccDbContext, TEntity, TPrimaryKey>
    where TEntity : class, IEntity<TPrimaryKey>
    {
        public sdakccSystemRepositoryBase(IDbContextProvider<sdakccDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        //add common methods for all repositories


    }
}
