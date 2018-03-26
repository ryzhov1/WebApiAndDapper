using System.Collections.Generic;
using System.Linq;
using System.Data;
using DataAccess.Entities;
using DataAccess.Connection;
using Dapper;


namespace DataAccess.Repositories
{
    public class ContragentRepository : IContragentRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public ContragentRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public IEnumerable<Contragent> GetAllContragents()
        {
            using (IDbConnection db = _connectionFactory.GetConnection)
            {
                return db.Query<Contragent>("SELECT * FROM Contragents");
            }
        }

        public Contragent Get(int id)
        {
            using (IDbConnection db = _connectionFactory.GetConnection)
            {
                return db.Query<Contragent>("SELECT * FROM Contragents WHERE Id = @id", new { id }).FirstOrDefault();
            }
        }

        public void Create(Contragent contragent)
        {
            using (IDbConnection db = _connectionFactory.GetConnection)
            {
                var sqlQuery = "INSERT INTO Contragents (INN, KPP, Name) VALUES(@INN, @KPP, @Name); SELECT CAST(SCOPE_IDENTITY() as int)";
                int Id = db.Query<int>(sqlQuery, contragent).First();
                contragent.Id = Id;
            }
        }

        public void Update(Contragent contragent)
        {
            using (IDbConnection db = _connectionFactory.GetConnection)
            {
                var sqlQuery = "UPDATE Contragents SET INN = @INN, KPP = @KPP, Name = @Name WHERE Id = @Id";
                db.Execute(sqlQuery, contragent);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = _connectionFactory.GetConnection)
            {
                var sqlQuery = "DELETE FROM Contragents WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }

        public Contragent GetByInn(string inn)
        {
            using (IDbConnection db = _connectionFactory.GetConnection)
            {
                return db.Query<Contragent>("SELECT * FROM Contragents WHERE Inn = @Inn", new { inn }).FirstOrDefault();
            }
        }

        public IEnumerable<string> GetDuplicatesInn()
        {
            var sqlQuery = @"select inn
                             from Contragents
                             group by inn
                             having count(inn) > 1";

            using (IDbConnection db = _connectionFactory.GetConnection)
            {
                return db.Query<string>(sqlQuery);
            }
        }
    }
}
