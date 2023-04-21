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

        #region QUERY

        public List<EpcContext> QueryRealTimeEpcContext()
        {
            try
            {
                var sql = @"SELECT * 
                            FROM (SELECT *, ROW_NUMBER() OVER (PARTITION BY [epc] ORDER BY id DESC) AS rowId FROM [scannel].[dbo].[eaton_epc]) AS epcs 
                            WHERE epcs.rowId=1 
                            ORDER BY id DESC ";
                return _connection.Query<EpcContext>(sql).ToList();
            }
            catch 
            {
                return null;
            }
        }

        public List<EpcContext> QueryHistoryEpcContext()
        {
            try
            {
                var sql = @"SELECT * 
                            FROM (SELECT *, ROW_NUMBER() OVER (PARTITION BY [epc], [reader_id] ORDER BY id DESC) AS rowId FROM[scannel].[dbo].[eaton_epc]) AS epcs 
                            WHERE epcs.rowId=1
                            ORDER BY id ";
                return _connection.Query<EpcContext>(sql).ToList();
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
                var sql = "SELECT COUNT(*) FROM [scannel].[dbo].[eaton_epc] ";
                return _connection.ExecuteScalar<int>(sql);
            }
            catch
            {
                return 0;
            }
        }

        public List<EpcContext> QueryEpcContexts(string epc, string reader_id)
        {
            try
            {
                var sql = @"SELECT * 
                            FROM [scannel].[dbo].[eaton_epc] 
                            WHERE epc=@epc AND reader_id=@reader_id ";
                return _connection.Query<EpcContext>(sql, new 
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

        public EpcContext QueryEpcContext(string epc, string reader_id, string timestamp)
        {
            try
            {
                var sql = @"SELECT * 
                            FROM [scannel].[dbo].[eaton_epc] 
                            WHERE epc=@epc AND reader_id=@reader_id AND timestamp=@timestamp ";
                return _connection.Query<EpcContext>(sql, new 
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

        public EpcDataContext QueryEpcDataContext(string pallet_id)
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
            catch {
                return null;
            }
        }

        #endregion

        #region INSERT

        public bool InsertEpcContext(string epc, string reader_id, string timestamp)
        {
            try
            {
                var sql = @"INSERT INTO [scannel].[dbo].[eaton_epc](epc, reader_id, timestamp)
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

        public bool InsertEpcDataContext(int f_eaton_epc_id, EpcDataDto epcDataDto)
        {
            try
            {
                var sql = @"INSERT INTO [scannel].[dbo].[eaton_epc_data](f_eaton_epc_id, wo, qty, pn, line, pallet_id)
                            VALUES(@f_eaton_epc_id, @wo, @qty, @pn, @line, @pallet_id) ";
                return _connection.Execute(sql, new 
                {
                    f_eaton_epc_id = f_eaton_epc_id,
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
        #endregion

        #region DELETE

        public bool DeleteEpcContext(int id)
        {
            try
            {
                var sql = @"DELETE FROM [scannel].[dbo].[eaton_epc]
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
