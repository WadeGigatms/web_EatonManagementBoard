using Dapper;
using EatonManagementBoard.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EatonManagementBoard.Database
{
    public class MsSqlConnectionRepository: ConnectionRepositoryBase
    {
        public MsSqlConnectionRepository(string connectionString) : base(DatabaseConnectionName.MsSql, connectionString)
        {
        }

        public List<EpcRawContext> QueryAll()
        {
            try
            {
                var sql = @"SELECT * FROM [scannel].[dbo].[eaton_epc_raw]  ";
                return _connection.Query<EpcRawContext>(sql).ToList();
            }
            catch
            {
                return null;
            }
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
                return _connection.Query<EpcRawContext>(sql).ToList();
            }
            catch 
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
                return _connection.Query<EpcRawContext>(sql).ToList();
            }
            catch
            {
                return null;
            }

        }

        public int QueryEpcCount()
        {
            try
            {
                var sql = "SELECT COUNT(*) FROM [scannel].[dbo].[eaton_epc_raw] ";
                return _connection.ExecuteScalar<int>(sql);
            }
            catch
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
                }).ToList();
            }
            catch
            {
                return null;
            }
        }

        public EpcRawContext QueryEpcRawContext(string epc, string reader_id, string timestamp)
        {
            try
            {
                var sql = @"SELECT * 
                            FROM [scannel].[dbo].[eaton_epc_raw] 
                            WHERE epc=@epc AND reader_id=@reader_id AND timestamp=@timestamp ";
                return _connection.Query<EpcRawContext>(sql, new 
                { 
                    epc = epc,
                    reader_id = reader_id,
                    timestamp = timestamp
                }).FirstOrDefault();
            }
            catch
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
                }).FirstOrDefault();
            }
            catch 
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
                }).ToList();
            }
            catch
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
                }).ToList();
            }
            catch
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
                }).ToList();
            }
            catch (Exception error)
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
                }).ToList();
            }
            catch
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
                }).ToList();
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region INSERT

        public bool InsertEpcRawContext(string epc, string reader_id, string timestamp)
        {
            try
            {
                var sql = @"INSERT INTO [scannel].[dbo].[eaton_epc_raw](epc, reader_id, timestamp)
                            VALUES(@epc, @reader_id, @timestamp)";
                return _connection.Execute(sql, new 
                { 
                    epc = epc,
                    reader_id = reader_id,
                    timestamp = timestamp
                }) > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool InsertEpcDataContext(int f_epc_raw_id, EpcDataDto epcDataDto)
        {
            try
            {
                var sql = @"INSERT INTO [scannel].[dbo].[eaton_epc_data](f_epc_raw_ids, wo, qty, pn, line, pallet_id)
                            VALUES(@f_epc_raw_ids, @wo, @qty, @pn, @line, @pallet_id) ";
                return _connection.Execute(sql, new 
                {
                    f_epc_raw_ids = f_epc_raw_id,
                    wo = epcDataDto.wo,
                    qty = epcDataDto.qty,
                    pn = epcDataDto.pn,
                    line = epcDataDto.line,
                    pallet_id = epcDataDto.pallet_id,
                }) > 0;
            }
            catch
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
                }) > 0;
            }
            catch
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
                return _connection.Execute(sql, new { id = id }) > 0;

            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}
