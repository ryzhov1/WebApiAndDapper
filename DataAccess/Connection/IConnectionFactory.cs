using System;
using System.Data;

namespace DataAccess.Connection
{
    public interface IConnectionFactory : IDisposable
    {
        IDbConnection GetConnection { get; }
    }
}
