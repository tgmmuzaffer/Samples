using MessageService.UI.Data;
using MessageService.UI.Models;
using MessageService.UI.Repos.IRepos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MessageService.UI.Repos
{
    public class UserRepo : IUserRepo
    {
        private readonly AppDbContext _appDbContext;

        public UserRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<bool> Create(User entity)
        {
            await _appDbContext.Users.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<User> Get(Expression<Func<User, bool>> filter)
        {
            return await _appDbContext.Users.AsNoTracking().FirstOrDefaultAsync(filter);  
        }

        public async Task<ICollection> GetList(Expression<Func<User, bool>> filter)
        {
            return filter == null ? await _appDbContext.Users.AsNoTracking().ToListAsync()
                : await _appDbContext.Users.AsNoTracking().Where(filter).ToListAsync();
        }
    }
}
