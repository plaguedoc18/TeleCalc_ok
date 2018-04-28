using ITUniver.TeleCalc.Web.Interfaces;
using ITUniver.TeleCalc.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ITUniver.TeleCalc.Web.Repositories
{
    public class BaseRepositories<T> : IRepository<T> where T : IEntity
    {
        private string connectionString = "";
        public BaseRepositories(string connectString)
        {
            this.connectionString = connectString;
        }
        public T Clone(T obj)
        {
            throw new NotImplementedException();
        }
        public bool Delete(T obj)
        {
            throw new NotImplementedException();
        }

        internal virtual string GetSelectQuery()
        {
            return "";
        }
        internal virtual T Map(SqlDataReader reader)
        {
            var properties = typeof(T).GetProperties();

            var obj = Activator.CreateInstance<T>();

            foreach (var property in properties)
            {
                var ind = reader.GetOrdinal(property.Name);
                if (!reader.IsDBNull(ind))
                {
                    property.SetValue(obj, reader[property.Name]);
                }

            }

            return obj;
        }
        public IEnumerable<T> Find(string condition)
        {
            var items = new List<T>();

            string queryString = GetSelectQuery();
            if (!string.IsNullOrWhiteSpace(condition))
            {
                queryString += $" WHERE {condition} ";
            }
            using (var connection = new SqlConnection(connectionString))
            {
                SqlCommand command =
                    new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                var temp = reader.HasRows;

                while (reader.Read())
                {
                    items.Add(Map(reader));
                }

                reader.Close();
            }
            return items;
        }

        public T Load(int id)
        {
            return Find($" [Id] = {id}").FirstOrDefault();
        }
        internal virtual string GetInsertQuery()
        {
            return "";
        }

        internal virtual string GetUpdateQuery()
        {
            return "";
        }
        internal SqlParameter[] InverseMap(T obj)
        {
            var parameters = new List<SqlParameter>();

            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                var val = property.GetValue(obj);
                parameters.Add(new SqlParameter($"@{property.Name}", val));
            }

            return parameters.ToArray();
        }

        public bool Save(T obj)
        {
            if (obj.Id == 0)
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    String query = GetInsertQuery();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddRange(InverseMap(obj));
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
            else
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    String query = GetUpdateQuery();

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddRange(InverseMap(obj));

                    connection.Open();

                    return command.ExecuteNonQuery() > 0;
                }
            }
            return false;
        }
    }
}