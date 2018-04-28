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
    public class UserRepositories : BaseRepositories<User>
    {
        public UserRepositories(string connectString) : base(connectString)
        {

        }
        internal override string GetSelectQuery()
        {
            return @"SELECT Id, Login, Name, Email, Password, BirthDate, Sex, Status 
                FROM dbo.[User]";
        }
        internal override string GetInsertQuery()
        {
            return @"INSERT INTO dbo.[User] 
                (Id, Login, Name, Email, Password, BirthDate, Sex, Status) 
                VALUES (@Id, @Login, @Name, @Email, @Password, @BirthDate, @Sex, @Status)";
        }
        internal override string GetUpdateQuery()
        {
            return @"UPDATE dbo.[User] 
                SET Login = @Login, 
                    Name = @Name,
                    Password = @Password,
                    Email = @Email,
                    Sex = @Sex,
                    Status = @Status,
                    BirthDate = @BirthDate
                WHERE Id = @Id";
        }




    }
}