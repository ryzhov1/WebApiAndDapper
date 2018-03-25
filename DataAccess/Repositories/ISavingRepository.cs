using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface ISavingRepository
    {
        void SaveAllContragents(IEnumerable<Contragent> contragents);
        void SaveAllContragentsFast(IEnumerable<Contragent> contragents);
    }
}
