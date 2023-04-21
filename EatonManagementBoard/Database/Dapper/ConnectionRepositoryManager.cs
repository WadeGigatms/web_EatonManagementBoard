using EatonManagementBoard.Dtos;
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

        #region Query

        public List<EpcContext> QueryRealTimeEpcContext()
            => MsSqlConnectionRepository.QueryRealTimeEpcContext();

        public List<EpcContext> QueryHistoryEpcContext()
            => MsSqlConnectionRepository.QueryHistoryEpcContext();

        public List<EpcContext> QueryEpcContexts(string epc, string readerId)
            => MsSqlConnectionRepository.QueryEpcContexts(epc, readerId);

        public EpcContext QueryEpcContext(string epc, string readerId, string transTime)
            => MsSqlConnectionRepository.QueryEpcContext(epc, readerId, transTime);

        public int QueryEpcCount()
            => MsSqlConnectionRepository.QueryEpcCount();

        public EpcDataContext QueryEpcDataContext(string palletId)
             => MsSqlConnectionRepository.QueryEpcDataContext(palletId);

        #endregion

        #region Insert

        public bool InsertEpcContext(string epc, string readerId, string transTime)
            => MsSqlConnectionRepository.InsertEpcContext(epc, readerId, transTime);

        public bool InsertEpcDataContext(int eatonEpcId, EpcDataDto epcDataDto)
            => MsSqlConnectionRepository.InsertEpcDataContext(eatonEpcId, epcDataDto);

        #endregion

        #region Delete

        public bool DeleteEpcContext(int id)
            => MsSqlConnectionRepository.DeleteEpcContext(id);

        #endregion
    }
}
