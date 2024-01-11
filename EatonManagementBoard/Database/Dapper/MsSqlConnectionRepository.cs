using Dapper;
using EatonManagementBoard.Dtos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EatonManagementBoard.Database.Dapper
{
    public class MsSqlConnectionRepository: ConnectionRepositoryBase
    {
        public MsSqlConnectionRepository(string connectionString) : base(DatabaseConnectionName.MsSql, connectionString)
        {
        }

        #region QUERY

        public List<EpcRawContext> QueryRealTimeEpcRawContext()
        {
            try
            {
                var sql = @"SELECT * 
                            FROM (SELECT *, ROW_NUMBER() OVER (PARTITION BY [epc] ORDER BY id DESC) AS rowId FROM [scannel].[dbo].[eaton_epc_raw]) AS epcs 
                            WHERE epcs.rowId=1 
                            ORDER BY id DESC ";
                return _connection.Query<EpcRawContext>(sql, null, _transaction).ToList();
            }
            catch (Exception exp)
            {
                return null;
            }
        }

        public List<EpcRawContext> QueryHistoryEpcRawContext()
        {
            try
            {
                var sql = @"SELECT * 
                            FROM (SELECT *, ROW_NUMBER() OVER (PARTITION BY [epc], [reader_id] ORDER BY id DESC) AS rowId FROM [scannel].[dbo].[eaton_epc_raw]) AS epcs 
                            WHERE epcs.rowId=1
                            ORDER BY id ";
                return _connection.Query<EpcRawContext>(sql, null, _transaction).ToList();
            }
            catch (Exception exp)
            {
                return null;
            }

        }

        public int QueryEpcCount()
        {
            try
            {
                var sql = "SELECT COUNT(*) FROM [scannel].[dbo].[eaton_epc_raw] ";
                return _connection.ExecuteScalar<int>(sql, null, _transaction);
            }
            catch (Exception exp)
            {
                return 0;
            }
        }

        public List<EpcRawContext> QueryEpcRawContexts(string epc, string reader_id)
        {
            try
            {
                var sql = @"SELECT * 
                            FROM [scannel].[dbo].[eaton_epc_raw] 
                            WHERE epc=@epc AND reader_id=@reader_id ";
                return _connection.Query<EpcRawContext>(sql, new 
                { 
                    epc = epc, 
                    reader_id = reader_id 
                }, _transaction).ToList();
            }
            catch (Exception exp)
            {
                return null;
            }
        }

        public EpcRawContext QueryEpcRawContext(EpcPostDto dto)
        {
            try
            {
                var sql = @"SELECT * 
                            FROM [scannel].[dbo].[eaton_epc_raw] 
                            WHERE epc=@epc AND reader_id=@reader_id AND timestamp=@timestamp ";
                return _connection.Query<EpcRawContext>(sql, new 
                { 
                    epc = dto.Epc,
                    reader_id = dto.ReaderId,
                    timestamp = dto.TransTime,
                }, _transaction).FirstOrDefault();
            }
            catch (Exception exp)
            {
                return null;
            }
        }

        public EpcDataContext QueryEpcDataContextByPalletId(string pallet_id)
        {
            try
            {
                var sql = @"SELECT * FROM [scannel].[dbo].[eaton_epc_data] 
                            WHERE pallet_id=@pallet_id ";
                return _connection.Query<EpcDataContext>(sql, new 
                { 
                    pallet_id = pallet_id 
                }, _transaction).FirstOrDefault();
            }
            catch (Exception exp)
            {
                return null;
            }
        }

        public List<EpcRawJoinEpcDataContext> QueryEpcRawJoinEpcDataContextByStartDate(DateTime startDate)
        {
            try
            {
                var sql = @"SELECT 
                            d.id AS epc_data_id, 
                            d.f_epc_raw_ids, 
                            d.wo, 
                            d.pn, 
                            d.qty, 
                            d.line, 
                            d.pallet_id, 
                            e.id AS epc_raw_id, 
                            e.reader_id, 
                            e.timestamp 
                            FROM [scannel].[dbo].[eaton_epc_raw] AS e 
                            INNER JOIN [scannel].[dbo].[eaton_epc_data] AS d 
                            ON e.id IN (SELECT * FROM string_split(d.f_epc_raw_ids, ',')) 
                            WHERE CONVERT(date, e.timestamp) = @startDate
                            ORDER BY d.pallet_id, e.timestamp ";
                return _connection.Query<EpcRawJoinEpcDataContext>(sql, new
                {
                    startDate = startDate
                }, _transaction).ToList();
            }
            catch (Exception exp)
            {
                return null;
            }
        }

        public List<EpcRawJoinEpcDataContext> QueryEpcRawJoinEpcDataContextByStartAndEndDate(DateTime startDate, DateTime endDate)
        {
            try
            {
                var sql = @"SELECT 
                            d.id AS epc_data_id, 
                            d.f_epc_raw_ids, 
                            d.wo, 
                            d.pn, 
                            d.qty, 
                            d.line, 
                            d.pallet_id, 
                            e.id AS epc_raw_id, 
                            e.reader_id, 
                            e.timestamp 
                            FROM [scannel].[dbo].[eaton_epc_raw] AS e 
                            INNER JOIN [scannel].[dbo].[eaton_epc_data] AS d 
                            ON e.id IN (SELECT * FROM string_split(d.f_epc_raw_ids, ',')) 
                            WHERE CONVERT(date, e.timestamp) BETWEEN @startDate AND @endDate 
                            ORDER BY d.pallet_id, e.timestamp ";
                return _connection.Query<EpcRawJoinEpcDataContext>(sql, new
                {
                    startDate = startDate,
                    endDate = endDate
                }, _transaction).ToList();
            }
            catch (Exception exp)
            {
                return null;
            }
        }

        public List<EpcRawJoinEpcDataContext> QueryEpcRawJoinEpcDataContextByWo(string wo)
        {
            try
            {
                var sql = @"SELECT 
                            d.id AS epc_data_id, 
                            d.f_epc_raw_ids, 
                            d.wo, 
                            d.pn, 
                            d.qty, 
                            d.line, 
                            d.pallet_id, 
                            e.id AS epc_raw_id, 
                            e.reader_id, 
                            e.timestamp 
                            FROM [scannel].[dbo].[eaton_epc_raw] AS e 
                            INNER JOIN (SELECT * FROM [scannel].[dbo].[eaton_epc_data] WHERE wo=@wo) AS d 
                            ON e.id IN (SELECT * FROM string_split(d.f_epc_raw_ids, ',')) 
                            ORDER BY d.pallet_id, e.timestamp ";
                return _connection.Query<EpcRawJoinEpcDataContext>(sql, new
                {
                    wo = wo
                }, _transaction).ToList();
            }
            catch (Exception exp)
            {
                return null;
            }
        }

        public List<EpcRawJoinEpcDataContext> QueryEpcRawJoinEpcDataContextByPn(string pn)
        {
            try
            {
                var sql = @"SELECT 
                            d.id AS epc_data_id, 
                            d.f_epc_raw_ids, 
                            d.wo, 
                            d.pn, 
                            d.qty, 
                            d.line, 
                            d.pallet_id, 
                            e.id AS epc_raw_id, 
                            e.reader_id, 
                            e.timestamp 
                            FROM [scannel].[dbo].[eaton_epc_raw] AS e 
                            INNER JOIN (SELECT * FROM [scannel].[dbo].[eaton_epc_data] WHERE pn=@pn) AS d 
                            ON e.id IN (SELECT * FROM string_split(d.f_epc_raw_ids, ',')) 
                            ORDER BY d.pallet_id, e.timestamp ";
                return _connection.Query<EpcRawJoinEpcDataContext>(sql, new
                {
                    pn = pn
                }, _transaction).ToList();
            }
            catch (Exception exp)
            {
                return null;
            }
        }

        public List<EpcRawJoinEpcDataContext> QueryEpcRawJoinEpcDataContextByPalletId(string pallet_id)
        {
            try
            {
                var sql = @"SELECT 
                            d.id AS epc_data_id, 
                            d.f_epc_raw_ids, 
                            d.wo, 
                            d.pn, 
                            d.qty, 
                            d.line, 
                            d.pallet_id, 
                            e.id AS epc_id, 
                            e.reader_id, 
                            e.timestamp 
                            FROM [scannel].[dbo].[eaton_epc_raw] AS e 
                            INNER JOIN (SELECT * FROM [scannel].[dbo].[eaton_epc_data] WHERE pallet_id=@pallet_id) AS d 
                            ON e.id IN (SELECT * FROM string_split(d.f_epc_raw_ids, ',')) 
                            ORDER BY d.pallet_id, e.timestamp ";
                return _connection.Query<EpcRawJoinEpcDataContext>(sql, new
                {
                    pallet_id = pallet_id
                }, _transaction).ToList();
            }
            catch (Exception exp)
            {
                return null;
            }
        }

        #endregion

        #region INSERT

        public bool InsertEpcRawContext(EpcPostDto dto)
        {
            try
            {
                var sql = @"INSERT INTO [scannel].[dbo].[eaton_epc_raw](epc, reader_id, timestamp)
                            VALUES(@epc, @reader_id, @timestamp)";
                return _connection.Execute(sql, new 
                { 
                    epc = dto.Epc,
                    reader_id = dto.ReaderId,
                    timestamp = dto.TransTime,
                }, _transaction) > 0;
            }
            catch (Exception exp)
            {
                return false;
            }
        }

        public bool InsertEpcDataContext(int f_epc_raw_id, EpcDataDto dto)
        {
            try
            {
                var sql = @"INSERT INTO [scannel].[dbo].[eaton_epc_data](f_epc_raw_ids, wo, qty, pn, line, pallet_id)
                            VALUES(@f_epc_raw_ids, @wo, @qty, @pn, @line, @pallet_id) ";
                return _connection.Execute(sql, new 
                {
                    f_epc_raw_ids = f_epc_raw_id,
                    wo = dto.wo,
                    qty = dto.qty,
                    pn = dto.pn,
                    line = dto.line,
                    pallet_id = dto.pallet_id,
                }, _transaction) > 0;
            }
            catch (Exception exp)
            {
                return false;
            }
        }

        #endregion

        #region UPDATE

        public bool UpdateEpcDataContext(int f_epc_raw_id, string pallet_id)
        {
            try
            {
                var sql = @"UPDATE [scannel].[dbo].[eaton_epc_data] 
                            SET f_epc_raw_ids=CONCAT(f_epc_raw_ids, @f_epc_raw_id) 
                            WHERE pallet_id=@pallet_id ";
                return _connection.Execute(sql, new
                {
                    f_epc_raw_id = $",{f_epc_raw_id}",
                    pallet_id = pallet_id
                }, _transaction) > 0;
            }
            catch (Exception exp)
            {
                return false;
            }
        }

        #endregion

        #region DELETE

        public bool DeleteEpcRawContext(int id)
        {
            try
            {
                var sql = @"DELETE FROM [scannel].[dbo].[eaton_epc_raw]
                            WHERE id=@id ";
                return _connection.Execute(sql, new { id = id }, _transaction) > 0;

            }
            catch (Exception exp)
            {
                return false;
            }
        }

        #endregion
    }
}
