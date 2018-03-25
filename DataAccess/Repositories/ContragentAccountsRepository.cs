using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DataAccess.Connection;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public class ContragentAccountsRepository : IContragentAccountsRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public ContragentAccountsRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public IEnumerable<Account> GetAllContragentAccounts(int contragentId)
        {
            var sql = @"select *
                        from Accounts a
                        inner join Banks b ON b.id = a.BankId
                        where a.ContragentId = @id";

            using (IDbConnection db = _connectionFactory.GetConnection)
            {
                var accounts = db.Query<Account, Bank, Account>(
                        sql,
                        (account, bank) =>
                        {
                            account.Bank = bank;
                            return account;
                        },
                    param: new { @id = contragentId },
                    splitOn: "BankId")
                    .Distinct();

                return accounts;
            }
        }
    }
}
