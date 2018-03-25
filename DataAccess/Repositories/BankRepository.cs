using System.Collections.Generic;
using System.Linq;
using System.Data;
using DataAccess.Entities;
using DataAccess.Connection;
using Dapper;

namespace DataAccess.Repositories
{
    public class BankRepository : IBankRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public BankRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public IEnumerable<Bank> GetAllBanks()
        {
            using (IDbConnection db = _connectionFactory.GetConnection)
            {
                return db.Query<Bank>("SELECT * FROM Banks");
            }
        }

        public Bank Get(int id)
        {
            using (IDbConnection db = _connectionFactory.GetConnection)
            {
                return db.Query<Bank>("SELECT * FROM Banks WHERE Id = @id", new { id }).FirstOrDefault();
            }
        }

        public Bank GetByBic(string bic)
        {
            using (IDbConnection db = _connectionFactory.GetConnection)
            {
                return db.Query<Bank>("SELECT * FROM Banks WHERE Bic = @Bic", new { bic }).FirstOrDefault();
            }
        }

        public void Create(Bank bank)
        {
            using (IDbConnection db = _connectionFactory.GetConnection)
            {
                var sqlQuery = "INSERT INTO Banks (Name, Bic, CorrespondingAccount, City) VALUES(@Name, @Bic, @CorrespondingAccount, @City); SELECT CAST(SCOPE_IDENTITY() as int)";
                int Id = db.Query<int>(sqlQuery, bank).First();
                bank.Id = Id;
            }
        }

        public void Update(Bank bank)
        {
            using (IDbConnection db = _connectionFactory.GetConnection)
            {
                var sqlQuery = "UPDATE Banks SET Name = @Name, Bic = @Bic, CorrespondingAccount = @CorrespondingAccount, City = @City WHERE Id = @Id";
                db.Execute(sqlQuery, bank);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = _connectionFactory.GetConnection)
            {
                var sqlQuery = "DELETE FROM Banks WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }
    }
}
