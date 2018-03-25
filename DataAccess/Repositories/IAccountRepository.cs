using System.Collections.Generic;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public interface IAccountRepository
    {
        void Create(Account account);
        void Delete(int id);
        Account Get(int id);
        Account Get(string number, int contragentId, int bankId);
        IEnumerable<Account> GetAllAccounts();
        void Update(Account account);
    }
}
