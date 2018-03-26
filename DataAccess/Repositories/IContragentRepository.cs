using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public interface IContragentRepository
    {
        void Create(Contragent contragent);
        void Delete(int id);
        Contragent Get(int id);
        Contragent GetByInn(string inn);
        IEnumerable<Contragent> GetAllContragents();
        void Update(Contragent contragent);
        IEnumerable<string> GetDuplicatesInn();
    }
}
