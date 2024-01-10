using EatonManagementBoard.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EatonManagementBoard.Database.Dapper
{
    public class ConnectionRepositoryManager
    {
        public MsSqlConnectionRepository MsSqlConnectionRepository { get; private set; }

        public ConnectionRepositoryManager(MsSqlConnectionRepository msSqlConnectionRepository)
        {
            MsSqlConnectionRepository = msSqlConnectionRepository;
        }

        #region Query

        public List<EpcRawContext> QueryRealTimeEpcRawContext()
            => MsSqlConnectionRepository.QueryRealTimeEpcRawContext();

        public List<EpcRawContext> QueryHistoryEpcRawContext()
            => MsSqlConnectionRepository.QueryHistoryEpcRawContext();

        public List<EpcRawContext> QueryEpcRawContexts(string epc, string readerId)
            => MsSqlConnectionRepository.QueryEpcRawContexts(epc, readerId);

        public EpcRawContext QueryEpcRawContext(EpcPostDto dto)
            => MsSqlConnectionRepository.QueryEpcRawContext(dto);

        public int QueryEpcCount()
            => MsSqlConnectionRepository.QueryEpcCount();

        public EpcDataContext QueryEpcDataContextByPalletId(string palletId)
             => MsSqlConnectionRepository.QueryEpcDataContextByPalletId(palletId);

        public List<EpcRawJoinEpcDataContext> QueryEpcRawJoinEpcDataContextByStartDate(DateTime startDate)
            => MsSqlConnectionRepository.QueryEpcRawJoinEpcDataContextByStartDate(startDate);

        public List<EpcRawJoinEpcDataContext> QueryEpcRawJoinEpcDataContextByStartAndEndDate(DateTime startDate, DateTime endDate)
            => MsSqlConnectionRepository.QueryEpcRawJoinEpcDataContextByStartAndEndDate(startDate, endDate);

        public List<EpcRawJoinEpcDataContext> QueryEpcRawJoinEpcDataContextByWo(string wo)
            => MsSqlConnectionRepository.QueryEpcRawJoinEpcDataContextByWo(wo);

        public List<EpcRawJoinEpcDataContext> QueryEpcRawJoinEpcDataContextByPn(string pn)
            => MsSqlConnectionRepository.QueryEpcRawJoinEpcDataContextByPn(pn);

        public List<EpcRawJoinEpcDataContext> QueryEpcRawJoinEpcDataContextByPalletId(string palletId)
            => MsSqlConnectionRepository.QueryEpcRawJoinEpcDataContextByPalletId(palletId);

        #endregion

        #region Insert

        public bool InsertEpcRawContext(EpcPostDto dto)
            => MsSqlConnectionRepository.InsertEpcRawContext(dto);

        public bool InsertEpcDataContext(int epcId, EpcDataDto epcDataDto)
            => MsSqlConnectionRepository.InsertEpcDataContext(epcId, epcDataDto);

        #endregion

        #region Update

        public bool UpdateEpcDataContext(int epcId, string palletId)
            => MsSqlConnectionRepository.UpdateEpcDataContext(epcId, palletId);

        #endregion

        #region Delete

        public bool DeleteEpcRawContext(int id)
            => MsSqlConnectionRepository.DeleteEpcRawContext(id);

        #endregion
    }
}
