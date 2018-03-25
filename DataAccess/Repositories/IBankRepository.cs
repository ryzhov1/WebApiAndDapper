using System.Collections.Generic;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public interface IBankRepository
    {
        void Create(Bank bank);
        void Delete(int id);
        Bank Get(int id);
        Bank GetByBic(string bic);
        IEnumerable<Bank> GetAllBanks();
        void Update(Bank bank);
    }
}
