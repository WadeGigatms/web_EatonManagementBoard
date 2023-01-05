using Dapper;
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

        public List<EatonEpcContext> QueryRealTimeEpc()
        {
            try
            {
                var result = _connection.Query<EatonEpcContext>(@"SELECT * FROM 
                (SELECT *, ROW_NUMBER() OVER (PARTITION BY [epc] ORDER BY Sid DESC) AS rowId FROM [scannel].[dbo].[eaton_epc]) AS epcs 
                WHERE epcs.rowId=1 
                ORDER BY Sid DESC;").ToList();
                return result;
            }
            catch
            {
                return null;
            }
        }

        public List<EatonEpcContext> QueryTraceEpc()
        {
            try
            {
                var result = _connection.Query<EatonEpcContext>(@"SELECT * FROM 
                (SELECT *, ROW_NUMBER() OVER (PARTITION BY [epc],[readerId] ORDER BY Sid DESC) AS rowId FROM [scannel].[dbo].[eaton_epc]) AS epcs 
                WHERE epcs.rowId=1 
                ORDER BY Sid;").ToList();
                return result;
            }
            catch
            {
                return null;
            }
        }

        public int QueryDataCount()
        {
            try
            {
                var result = _connection.Query<int>(@"SELECT * FROM [scannel].[dbo].[eaton_epc];").Count();
                return result;
            }
            catch
            {
                return 0;
            }
        }
    }
}
