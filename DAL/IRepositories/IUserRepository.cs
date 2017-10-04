
using DAL;
using System.Collections.Generic;

namespace DAL.IRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        void Remove(User entity);
        void Add(User entity);
        bool Any(string entity);
        User GetUser(string userName, string userPassword);
    }
}