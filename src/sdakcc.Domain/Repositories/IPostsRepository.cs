using Abp.Domain.Entities;
using sdakcc.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace sdakcc.Repositories
{
    public interface IPostsRepository : IRepository<Posts, Guid>
    {
        Task<IQueryable<Posts>> GetAllFullyLoadedPostsAsync(int page);
        Task<Posts> FindLoadedPostAsync(Guid postId);

    }

}
