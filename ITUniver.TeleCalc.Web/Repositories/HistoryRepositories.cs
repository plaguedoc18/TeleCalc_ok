using ITUniver.TeleCalc.Web.Interfaces;
using ITUniver.TeleCalc.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ITUniver.TeleCalc.Web.Repositories
{
    public class HistoryRepositories : BaseRepositories<HistoryItemModel>
    {
        private string connectionString = "";
        public HistoryRepositories(string connectionString)
                : base(connectionString)
        {
            //this.connectionString = connectString;
        }
        internal override string GetSelectQuery()
        {
            return @"SELECT Id, Operation, Initiator, Args, Result, CalcDate, Time FROM dbo.History";
        }

        internal override string GetInsertQuery()
        {
            return @"INSERT INTO dbo.History (Operation, Initiator, Args, Result, CalcDate, Time) VALUES (@Operation, @Initiator, @Args, @Result, @CalcDate, @Time)";
        }
    }
}