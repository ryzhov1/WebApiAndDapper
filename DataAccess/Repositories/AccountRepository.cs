using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;
using DataAccess.Connection;
using Dapper;
using System.Data;

namespace DataAccess.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public AccountRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            using (IDbConnection db = _connectionFactory.GetConnection)
            {
                return db.Query<Account>("SELECT * FROM Accounts");
            }
        }

        public Account Get(int id)
        {
            using (IDbConnection db = _connectionFactory.GetConnection)
            {
                return db.Query<Account>("SELECT * FROM Accounts WHERE Id = @id", new { id }).FirstOrDefault();
            }
        }

        public Account Get(string number, int contragentId, int bankId)
        {
            using (IDbConnection db = _connectionFactory.GetConnection)
            {
                return db.Query<Account>("SELECT * " +
                                         "FROM Accounts " +
                                         "WHERE Number = @Number " +
                                         "AND ContragentId = @ContragentId " +
                                         "AND BankId = @BankId", new { number, contragentId, bankId }).FirstOrDefault();
            }
        }

        public void Create(Account account)
        {
            using (IDbConnection db = _connectionFactory.GetConnection)
            {
                var sqlQuery = "INSERT INTO Accounts (Number, ContragentId, BankId) VALUES(@Number, @ContragentId, @BankId); SELECT CAST(SCOPE_IDENTITY() as int)";
                int Id = db.Query<int>(sqlQuery, account).First();
                account.Id = Id;
            }
        }

        public void Update(Account account)
        {
            using (IDbConnection db = _connectionFactory.GetConnection)
            {
                var sqlQuery = "UPDATE Accounts SET Number = @Number, ContragentId = @ContragentId, BankId = @BankId WHERE Id = @Id";
                db.Execute(sqlQuery, account);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = _connectionFactory.GetConnection)
            {
                var sqlQuery = "DELETE FROM Accounts WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }
    }
}
