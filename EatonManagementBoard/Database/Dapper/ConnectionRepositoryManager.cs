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

        public List<EpcContext> QueryAll() => MsSqlConnectionRepository.QueryAll();

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

        public EpcDataContext QueryEpcDataContextByPalletId(string palletId)
             => MsSqlConnectionRepository.QueryEpcDataContextByPalletId(palletId);

        public List<EpcJoinEpcDataContext> QueryEpcJoinEpcDataContextByStartDate(DateTime startDate)
            => MsSqlConnectionRepository.QueryEpcJoinEpcDataContextByStartDate(startDate);

        public List<EpcJoinEpcDataContext> QueryEpcJoinEpcDataContextByStartAndEndDate(DateTime startDate, DateTime endDate)
            => MsSqlConnectionRepository.QueryEpcJoinEpcDataContextByStartAndEndDate(startDate, endDate);

        public List<EpcJoinEpcDataContext> QueryEpcJoinEpcDataContextByWo(string wo)
            => MsSqlConnectionRepository.QueryEpcJoinEpcDataContextByWo(wo);

        public List<EpcJoinEpcDataContext> QueryEpcJoinEpcDataContextByPn(string pn)
            => MsSqlConnectionRepository.QueryEpcJoinEpcDataContextByPn(pn);

        public List<EpcJoinEpcDataContext> QueryEpcJoinEpcDataContextByPalletId(string palletId)
            => MsSqlConnectionRepository.QueryEpcJoinEpcDataContextByPalletId(palletId);

        #endregion

        #region Insert

        public bool InsertEpcContext(string epc, string readerId, string transTime)
            => MsSqlConnectionRepository.InsertEpcContext(epc, readerId, transTime);

        public bool InsertEpcDataContext(int epcId, EpcDataDto epcDataDto)
            => MsSqlConnectionRepository.InsertEpcDataContext(epcId, epcDataDto);

        #endregion

        #region Update

        public bool UpdateEpcDataContext(int epcId, string palletId)
            => MsSqlConnectionRepository.UpdateEpcDataContext(epcId, palletId);

        #endregion

        #region Delete

        public bool DeleteEpcContext(int id)
            => MsSqlConnectionRepository.DeleteEpcContext(id);

        #endregion
    }
}
