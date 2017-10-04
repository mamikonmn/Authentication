using DAL;
using DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {

        public UserRepository(InterviewDBContext context)
            : base(context)
        {
        }

        public void Add(User entity)
        {
            UserContext.Set<User>().Add(entity);
        }



        public void Remove(User entity)
        {
            UserContext.Set<User>().Remove(entity);
        }

        public bool Any(string username)
        {
            return UserContext.User.Any(i => i.user_name == username);
        }

        public User GetUser(string userName, string userPassword)
        {
            return UserContext.User.FirstOrDefault(i => i.user_name == userName &&
            i.password == userPassword);
        }

        public InterviewDBContext UserContext
        {
            get { return Context as InterviewDBContext; }
        }
    }
}
