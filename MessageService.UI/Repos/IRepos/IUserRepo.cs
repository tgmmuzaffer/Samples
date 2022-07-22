using MessageService.UI.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MessageService.UI.Repos.IRepos
{
    public interface IUserRepo
    {
        Task<ICollection> GetList(Expression<Func<User, bool>> filter = null);
        Task<User> Get(Expression<Func<User, bool>> filter = null);
        Task<bool> Create(User entity);
    }
}
