using Dapper;
using EatonManagementBoard.Dtos;
using EatonManagementBoard.Models;
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
                            FROM 
                            (SELECT *, ROW_NUMBER() OVER (PARTITION BY [epc] ORDER BY Sid DESC) AS rowId FROM [scannel].[dbo].[eaton_epc]) AS epcs 
                            WHERE epcs.rowId=1 
                            ORDER BY Sid DESC";
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
                            FROM (SELECT *, ROW_NUMBER() OVER (PARTITION BY [epc], [readerId] ORDER BY Sid DESC) AS rowId FROM[scannel].[dbo].[eaton_epc]) AS epcs 
                            WHERE epcs.rowId=1
                            ORDER BY Sid";
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
                var sql = "SELECT COUNT(*) FROM [scannel].[dbo].[eaton_epc]";
                return _connection.ExecuteScalar<int>(sql);
            }
            catch
            {
                return 0;
            }
        }

        public List<EpcContext> QueryEpcContext(string epc, string readerId)
        {
            try
            {
                var sql = @"SELECT * 
                            FROM [scannel].[dbo].[eaton_epc] 
                            WHERE epc=@epc AND readerId=@readerId";
                return _connection.Query<EpcContext>(sql, new { epc = epc, readerId = readerId }).ToList();
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region INSERT

        public bool InsertEpc(string epc, string readerId, string transTime)
        {
            try
            {
                var sql = @"INSERT INTO [scannel].[dbo].[eaton_epc](epc, readerId, transTime)
                            VALUES(@epc, @readerId, @transTime)";
                return _connection.Execute(sql, new { epc = epc, readerId = readerId, transTime = transTime }) > 0;
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

        public bool DeleteEpc(int Sid)
        {
            try
            {
                var sql = @"DELETE FROM [scannel].[dbo].[eaton_epc]
                            WHERE Sid=@Sid ";
                return _connection.Execute(sql, new { Sid = Sid }) > 0;

            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}
