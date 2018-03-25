using DataAccess.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DataAccess.Repositories
{
    public class DeletingRepository : IDeletingRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public DeletingRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public void DeleteAllData()
        {
            using (IDbConnection db = _connectionFactory.GetConnection)
            {
                var sqlQuery = @"DELETE FROM accounts
                                 DBCC CHECKIDENT ('accounts',RESEED, 0)

                                 DELETE FROM banks
                                 DBCC CHECKIDENT ('banks',RESEED, 0)

                                 DELETE FROM contragents
                                 DBCC CHECKIDENT ('contragents',RESEED, 0)";

                db.Execute(sqlQuery);
            }
        }
    }
}
