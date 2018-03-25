using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Connection;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public class SavingRepository : ISavingRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly IBankRepository _bankRepository;
        private readonly IContragentRepository _contragentRepository;
        private readonly IAccountRepository _accountRepository;

        public SavingRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;

            _bankRepository = new BankRepository(_connectionFactory);
            _contragentRepository = new ContragentRepository(_connectionFactory);
            _accountRepository = new AccountRepository(_connectionFactory);
        }

        public void DeleteAllData()
        {
            throw new NotImplementedException();
        }

        public void SaveAllContragents(IEnumerable<Contragent> contragents)
        {
            Dictionary<string, Bank> dicBanks = new Dictionary<string, Bank>();

            IDbTransaction trans = _connectionFactory.GetConnection.BeginTransaction();
            try
            {
                foreach (Contragent contragent in contragents)
                {
                    string contrInn = contragent.INN;

                    Contragent contrByInn = _contragentRepository.GetByInn(contrInn);

                    if (contrByInn == null)
                    {
                        _contragentRepository.Create(contragent);
                    }
                    else
                    {
                        contragent.Id = contrByInn.Id;
                        if (!contrByInn.Equals(contragent))
                            _contragentRepository.Update(contragent);
                    }

                    if ((contragent.Accounts != null) && (contragent.Accounts.Count > 0))
                    {
                        foreach (Account account in contragent.Accounts)
                        {
                            string bankBic = account.Bank.Bic;
                            Bank bankByBic = null;
                            Boolean just_now_added = false;

                            if (!dicBanks.ContainsKey(bankBic))
                            {
                                bankByBic = _bankRepository.GetByBic(bankBic);

                                if (bankByBic == null)
                                {
                                    bankByBic = account.Bank;
                                    _bankRepository.Create(bankByBic);
                                }

                                dicBanks.Add(bankBic, bankByBic);
                                just_now_added = true;
                            }
                            else
                            {
                                bankByBic = dicBanks[bankBic];
                            }

                            account.Bank.Id = bankByBic.Id;

                            if (!bankByBic.Equals(account.Bank))
                            {
                                _bankRepository.Update(account.Bank);

                                if (!just_now_added)
                                    dicBanks[bankBic] = account.Bank;
                            }

                            account.ContragentId = contragent.Id;
                            account.BankId = account.Bank.Id;

                            Account accountFromDB = _accountRepository.Get(account.Number, contragent.Id, account.Bank.Id);
                            if (accountFromDB == null)
                            _accountRepository.Create(account);
                        }
                        
                    }
                }

                trans.Commit();
            }
            catch (Exception e)
            {
                trans.Rollback();
                throw new Exception(String.Format("Исключение: {0}", e.Message));
            }
        }

        public void SaveAllContragentsFast(IEnumerable<Contragent> contragents)
        {
            IDbTransaction trans = _connectionFactory.GetConnection.BeginTransaction();
            try
            {
                foreach (Contragent contragent in contragents)
                {
                    _contragentRepository.Create(contragent);

                    if ((contragent.Accounts != null) && (contragent.Accounts.Count > 0))
                    {
                        foreach (Account account in contragent.Accounts)
                        {
                            _bankRepository.Create(account.Bank);

                            account.ContragentId = contragent.Id;
                            account.BankId = account.Bank.Id;

                            _accountRepository.Create(account);
                        }

                    }
                }

                trans.Commit();
            }
            catch (Exception e)
            {
                trans.Rollback();
                throw new Exception(String.Format("Исключение: {0}", e.Message));
            }
        }
    }
}
