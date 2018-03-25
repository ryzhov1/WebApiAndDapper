using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.FileImport
{
    public class FileImporter
    {
        private string _fileName;

        //private SortedDictionary<string, Contragent> _contragents = new SortedDictionary<string, Contragent>();
        private List<Contragent> _contragents = new List<Contragent>();

        private string _lastError;

        public string FileName { get => _fileName; }
        public string LastError { get => _lastError; }

        public FileImporter(string fileName)
        {
            _lastError = "";
            _fileName = fileName;
        }

        public IEnumerable<Contragent> GetAllContragents()
        {

            //return _contragents.Values.ToList();
            return _contragents;
        }

        public Boolean TestImport()
        {
            _lastError = "";

            try
            {
                byte[] fileArray;
                using (System.IO.FileStream fs = new System.IO.FileStream(_fileName, System.IO.FileMode.Open))
                {
                    fileArray = new byte[fs.Length];
                    fs.Read(fileArray, 0, (int)fs.Length);
                }

                List<string> currentSection = new List<string>();

                using (StreamReader sr = new StreamReader(_fileName, System.Text.Encoding.Default))
                {
                    string line;
                    Boolean isSectionData = false;
                    Boolean isFirstLine = true;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (isFirstLine)
                        {
                            if (!line.Equals("1CClientBankExchange"))
                                throw new Exception("Импортируемый файл не является файлом обмена");

                            isFirstLine = false;
                        };

                        if (line.StartsWith("СекцияДокумент"))
                        {
                            isSectionData = true;
                            continue;
                        }

                        if (line.StartsWith("КонецДокумента"))
                        {
                            isSectionData = false;

                            if (currentSection.Count > 0)
                            {
                                ParseSection(currentSection);
                                currentSection.Clear();
                            }
                        }

                        if (isSectionData)
                            currentSection.Add(line);
                    }

                }

            }
            catch (Exception e)
            {
                _lastError = e.Message;
                return false;
            }

            return true;
        }

        private void ParseSection(List<string> section)
        {
            try
            {
                SectionDictionary sectionDictionary = new SectionDictionary(section);
                ParseSection_ContragetnFrom(sectionDictionary);
                ParseSection_ContragetnTo(sectionDictionary);

            }
            catch (Exception e)
            {
                var sectionString = String.Join("\n", section.ToArray());

                _lastError = string.Format("При разборе секции: '{0}'.\n Секция:\r\n{1}", e.Message, sectionString);
                throw new Exception(_lastError);
            }
        }

        private void ParseSection_ContragetnFrom(SectionDictionary dictionary)
        {
            Contragent contragent = null;

            string contragentINN = dictionary.getValue("ПлательщикИНН", "");

            if (contragentINN.Equals(""))
                throw new Exception("значение 'ПлательщикИНН' пустое");

            if (contragentINN.Length > 12)
                throw new Exception(String.Format("значение 'ПлательщикИНН' превышает максимальную длину 12 символов"));
            //if (_contragents.ContainsKey(contragentINN))
            //    contragent = _contragents[contragentINN];

            if (contragent == null)
            {
                contragent = new Contragent { INN = contragentINN };
                //_contragents.Add(contragentINN, contragent);
                _contragents.Add(contragent);
            };

            contragent.Name = dictionary.getValue("Плательщик1", "");
            contragent.KPP = dictionary.getValue("ПлательщикКПП", "0");

            if (contragent.KPP.Length > 9)
                throw new Exception(String.Format("значение 'ПлательщикКПП' превышает максимальную длину 9 символов"));

            string accountNumber = dictionary.getValue("ПлательщикСчет", "");

            if (accountNumber.Equals(""))
                throw new Exception("значение 'ПлательщикСчет' пустое");

            if (accountNumber.Length > 20)
                throw new Exception(String.Format("значение 'ПлательщикСчет' превышает максимальную длину 20 символов"));

            Account account = contragent.Accounts.Find(x => x.Number == accountNumber);

            if (account == null)
            {
                account = new Account();
                account.Number = accountNumber;
                //account.Contragent = contragent;
                contragent.Accounts.Add(account);
            }

            account.Bank.Bic = dictionary.getValue("ПлательщикБИК", "");
            account.Bank.CorrespondingAccount = dictionary.getValue("ПлательщикКорсчет", "");
            account.Bank.Name = dictionary.getValue("ПлательщикБанк1", "");
            account.Bank.City = dictionary.getValue("ПлательщикБанк2", "");

            if (account.Bank.Bic.Length > 9)
                throw new Exception(String.Format("значение 'ПлательщикБИК' превышает максимальную длину 9 символов"));

            if (account.Bank.CorrespondingAccount.Length > 20)
                throw new Exception(String.Format("значение 'ПлательщикКорсчет' превышает максимальную длину 20 символов"));
        }

        private void ParseSection_ContragetnTo(SectionDictionary dictionary)
        {
            Contragent contragent = null;

            string contragentINN = dictionary.getValue("ПолучательИНН", "");

            if (contragentINN.Equals(""))
                throw new Exception("значение 'ПолучательИНН' пустое");

            if (contragentINN.Length > 12)
                throw new Exception(String.Format("значение 'ПолучательИНН' превышает максимальную длину 12 символов"));

            //if (_contragents.ContainsKey(contragentINN))
            //    contragent = _contragents[contragentINN];

            if (contragent == null)
            {
                contragent = new Contragent { INN = contragentINN };
                //_contragents.Add(contragentINN, contragent);
                _contragents.Add(contragent);
            };

            contragent.KPP = dictionary.getValue("ПолучательКПП", "0");
            contragent.Name = dictionary.getValue("Получатель1", "");

            if (contragent.KPP.Length > 9)
                throw new Exception(String.Format("значение 'ПолучательКПП' превышает максимальную длину 9 символов"));

            string accountNumber = dictionary.getValue("ПолучательСчет", "");

            if (accountNumber.Equals(""))
                throw new Exception("значение 'ПолучательСчет' пустое");

            if (accountNumber.Length > 20)
                throw new Exception(String.Format("значение 'ПолучательСчет' превышает максимальную длину 20 символов"));

            Account account = contragent.Accounts.Find(x => x.Number == accountNumber);

            if (account == null)
            {
                account = new Account();
                account.Number = accountNumber;
                //account.Contragent = contragent;
                contragent.Accounts.Add(account);
            }

            account.Bank.Bic = dictionary.getValue("ПолучательБИК", "");
            account.Bank.CorrespondingAccount = dictionary.getValue("ПолучательКорсчет", "");
            account.Bank.Name = dictionary.getValue("ПолучательБанк1", "");
            account.Bank.City = dictionary.getValue("ПолучательБанк2", "");

            if (account.Bank.Bic.Length > 9)
                throw new Exception(String.Format("значение 'ПолучательБИК' превышает максимальную длину 9 символов"));

            if (account.Bank.CorrespondingAccount.Length > 20)
                throw new Exception(String.Format("значение 'ПолучательКорсчет' превышает максимальную длину 20 символов"));
        }
    }


    class SectionDictionary
    {
        private Dictionary<string, string> _dictionary;

        public SectionDictionary(List<string> section)
        {
            _dictionary = new Dictionary<string, string>();

            string sectionLine;
            for (int i = 0; i < section.Count(); i++)
            {
                sectionLine = section[i];
                int indexSymbol = sectionLine.IndexOf("=");

                //string[] sectionLineArr = sectionLine.Split(new char[] { '=' });
                //if (sectionLineArr.Length != 2)
                //    throw new Exception(string.Format("Неверный формат в строке: '{0}' (Номер строки = {1})", sectionLine, i+1));

                //_dictionary.Add(sectionLineArr[0].ToLower(), sectionLineArr[1]);

                string sectionKey = sectionLine.Substring(0, indexSymbol);
                string sectionValue = sectionLine.Substring(indexSymbol + 1);
                sectionValue = sectionValue.Trim();
                _dictionary.Add(sectionKey.ToLower(), sectionValue);
            }
        }

        public Boolean ContainsKey(string key)
        {
            return _dictionary.ContainsKey(key.ToLower());
        }

        public string getValue(string key)
        {
            return _dictionary[key.ToLower()];
        }

        public string getValue(string key, string defValue)
        {
            if (!ContainsKey(key))
                return defValue;

            return getValue(key);
        }
    }
}
