using Core.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public class DBManager : IDBManager, IDisposable
    {
        private bool _done;
        private IUnitOfWork _UnitOfWork;

        public DBManager()
        {
            _UnitOfWork = new UnitOfWork(new InterviewDBContext());
        }

        #region Users       
        public UserModel GetUserById(int id)
        {
            User user = _UnitOfWork.Users.Get(id);
            if (user != null)
            {
                return new UserModel
                {
                    Id = user.id,
                    Address = user.User_Info == null ? null : user.User_Info.address,
                    Name = user.User_Info == null ? null : user.User_Info.name,
                    Password = user.password,
                    UserName = user.user_name,
                };
            }
            return null;
        }
        public UserModel GetUser(string userName, string userPassword)
        {
            User user = _UnitOfWork.Users.GetUser(userName, userPassword);
            if (user != null)
            {
                return new UserModel
                {
                    Id = user.id,
                    Address = user.User_Info == null ? null : user.User_Info.address,
                    Name = user.User_Info == null ? null : user.User_Info.name,
                    Password = user.password,
                    UserName = user.user_name,
                };
            }
            return null;
        }

        public bool AddUser(UserModel model)
        {
            _done = true;
            if (!string.IsNullOrEmpty(model.UserName) && !string.IsNullOrEmpty(model.Password))
            {
                if (!_UnitOfWork.Users.Any(model.UserName))
                {
                    var user = new User
                    {
                        //encrypt username and password and pass to DB
                        user_name = model.UserName,
                        password = model.Password,
                    };
                    try
                    {
                        _UnitOfWork.Users.Add(user);
                        _UnitOfWork.Complete();
                    }
                    catch (Exception)
                    {
                        _done = false;
                    }
                }
            }
            return _done;
        }

        public bool EditUser(UserModel model)
        {
            _done = true;
            if (_UnitOfWork.Users.Any(model.Name))
            {
                var user = _UnitOfWork.Users.Get(model.Id);
                user.User_Info.name = model.Name;
                user.User_Info.address = model.Address;
                user.User_Info.register_date = DateTime.Now;
                user.User_Info.confirmed = true;//After Email confirmation
                try
                {
                    _UnitOfWork.Users.Add(user);
                    _UnitOfWork.Complete();
                }
                catch (Exception)
                {
                    _done = false;
                }
            }
            return _done;
        }
        #endregion

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _UnitOfWork.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
