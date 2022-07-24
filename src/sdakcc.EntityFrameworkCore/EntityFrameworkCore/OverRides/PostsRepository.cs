using Abp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using sdakcc.Entities;
using sdakcc.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace sdakcc.EntityFrameworkCore.OverRides
{

    public class PostsRepository :
        EfCoreRepository<sdakccDbContext, Posts, Guid>,
            IPostsRepository
    {
        public PostsRepository(
                    IDbContextProvider<sdakccDbContext> dbContextProvider)
                                     : base(dbContextProvider)
        { }

        public async Task<Posts> FindLoadedPostAsync(Guid postId)
        {
            
            var dbContext = await GetDbContextAsync();
            var query = dbContext.posts;
          
            var output = await query
                          .Include(x => x.Likes)
                          .ThenInclude(x => x.Users).FirstOrDefaultAsync(x => x.Id == postId);

            return output;

        }

        public async Task<IQueryable<Posts>> GetAllFullyLoadedPostsAsync(int page)
        {

            var dbContext = await GetDbContextAsync();
            var query = dbContext.posts;
           
            var numberPerPage = 15;
            var skip = numberPerPage * (page - 1);


            return  query
               .OrderByDescending(x => x.CreationTime)
               .Include(x => x.Likes)
               .ThenInclude(x => x.Users)
               .Skip(skip)
               .Take(numberPerPage);
                        
        }

       
    }

   
}
