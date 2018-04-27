using ITUniver.TeleCalc.Web.Interfaces;
using ITUniver.TeleCalc.Web.Models;
using ITUniver.TeleCalc.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ITUniver.TeleCalc.Web.Repositories
{
    public class OperationRepositories : BaseRepositories<OperationModel>
    {
        public OperationRepositories(string connectString) : base(connectString)
        {

        }
        internal override string GetSelectQuery()
        {
            return @"SELECT Id, Name, Owner 
                FROM dbo.Operation"; 
        }       
        internal override string GetInsertQuery()
        {
            return @"INSERT INTO dbo.Operation (Name, Owner) VALUES (@name, @owner)";
        }

        public OperationModel LoadByName(string name)
        {
            var opers = Find($"[Name] = N'{name}'");

            return opers.FirstOrDefault();
        }


    }
}