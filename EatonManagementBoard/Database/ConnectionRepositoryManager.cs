using EatonManagementBoard.Dtos;
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

        public List<EpcContext> QueryRealTimeEpcContext()
            => MsSqlConnectionRepository.QueryRealTimeEpcContext();

        public List<EpcContext> QueryHistoryEpcContext()
            => MsSqlConnectionRepository.QueryHistoryEpcContext();

        public List<EpcContext> QueryEpcContext(string epc, string readerId)
            => MsSqlConnectionRepository.QueryEpcContext(epc, readerId);

        public int QueryEpcCount()
            => MsSqlConnectionRepository.QueryEpcCount();

        public bool InsertEpc(string epc, string readerId, string transTime)
            => MsSqlConnectionRepository.InsertEpc(epc, readerId, transTime);

        public bool DeleteEpc(int Sid)
            => MsSqlConnectionRepository.DeleteEpc(Sid);

        #endregion
    }
}
