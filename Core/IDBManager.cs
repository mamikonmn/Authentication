using Core.Models;
using System.Threading.Tasks;

namespace Core
{
    public interface IDBManager
    {
        UserModel GetUserById(int id);
        UserModel GetUser(string userName,string userPassword);
        bool EditUser(UserModel model);
        bool AddUser(UserModel model);
    }
}
