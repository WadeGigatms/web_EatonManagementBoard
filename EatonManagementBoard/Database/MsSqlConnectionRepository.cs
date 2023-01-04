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
            var result = _connection.Query<EatonEpcContext>(@"SELECT * FROM 
                (SELECT *, ROW_NUMBER() OVER (PARTITION BY [epc] ORDER BY Sid DESC) AS rowId FROM [scannel].[dbo].[eaton_epc]) AS epcs 
                WHERE epcs.rowId=1 
                ORDER BY Sid DESC;").ToList();
            return result;
        }

        public List<EatonEpcContext> QueryTraceEpc()
        {
            var result = _connection.Query<EatonEpcContext>(@"SELECT * FROM 
                (SELECT *, ROW_NUMBER() OVER (PARTITION BY [epc],[readerId] ORDER BY Sid DESC) AS rowId FROM [scannel].[dbo].[eaton_epc]) AS epcs 
                WHERE epcs.rowId=1 
                ORDER BY Sid;").ToList();
            return result;
        }

        public int QueryDataCount()
        {
            var result = _connection.Query<int>(@"SELECT * FROM [scannel].[dbo].[eaton_epc];").Count();
            return result;
        }
    }
}
