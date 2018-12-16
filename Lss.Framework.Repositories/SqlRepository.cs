using System.Data;

namespace Lss.Framework.Repositories
{
    /// <summary>
    /// The concrete implementation of a SQL repository
    /// </summary>
    public abstract class SqlRepository
    {
        private string _connectionString;
        private EDbConnectionTypes _dbType;

        public SqlRepository(string connectionString)
        {
            _dbType = EDbConnectionTypes.Sql;
            _connectionString = connectionString;
        }

        public IDbConnection GetOpenConnection()
        {
            return DbConnectionFactory.GetDbConnection(_dbType, _connectionString);
        }
    }
}
