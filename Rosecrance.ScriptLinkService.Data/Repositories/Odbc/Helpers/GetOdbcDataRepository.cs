using NLog;
using Rosecrance.ScriptLinkService.Data.Models;

namespace Rosecrance.ScriptLinkService.Data.Repositories.Odbc
{
    public partial class GetOdbcDataRepository : IGetDataRepository
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly ConnectionStringCollection _connectionStringCollection;

        public GetOdbcDataRepository(ConnectionStringCollection connectionStringCollection)
        {
            _connectionStringCollection = connectionStringCollection;
        }
    }
}
