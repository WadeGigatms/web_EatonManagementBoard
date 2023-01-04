using EatonManagementBoard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EatonManagementBoard.Database
{
    public class ConnectionRepositoryManager
    {
        public MsSqlConnectionRepository MsSqlConnectionRepository { get; private set; }
        public ConnectionRepositoryManager(MsSqlConnectionRepository msSqlConnectionRepository)
        {
            MsSqlConnectionRepository = msSqlConnectionRepository;
        }

        #region MSSql Connection

        public List<EatonEpcContext> QueryRealTimeEpc()
            => MsSqlConnectionRepository.QueryRealTimeEpc();

        public List<EatonEpcContext> QueryTraceEpc()
            => MsSqlConnectionRepository.QueryTraceEpc();

        public int QueryDataCount() 
            => MsSqlConnectionRepository.QueryDataCount();

        #endregion
    }
}
