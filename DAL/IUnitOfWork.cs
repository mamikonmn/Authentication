using DAL.IRepositories;
using System;

namespace DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        int Complete();
    }
}