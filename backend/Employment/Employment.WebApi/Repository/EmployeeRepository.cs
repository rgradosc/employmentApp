using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Employment.WebApi.Repository
{
    using Dapper;
    using Models;
    using Configuration;

    public class EmployeeRepository
    {
        private const string connectionName = "DefaultConnection";
        private string connectionString = DatabaseConfig.ConnectionString(connectionName);

        public ICollection<Employee> GetAll()
        {
            ICollection<Employee> departments = new List<Employee>();
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                departments = connection.Query<Employee>("dbo.spEmployees_GetAll").ToList();

                return departments;
            }
        }

        public Employee GetById(int id)
        {
            Employee department = null;
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var p = new DynamicParameters();
                p.Add("@Id", id);
                department = connection.Query<Employee>("dbo.spEmployees_GetById", p, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return department;
            }
        }

        public int Create(Employee model)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var p = new DynamicParameters();
                p.Add("@Name", model.Name);
                p.Add("@Department", model.Department);
                p.Add("@DateOfJoining", model.DateOfJoining);
                p.Add("@PhotoFileName", model.PhotoFileName);
                p.Add("@id", model.Id, DbType.Int32, ParameterDirection.Output);

                try
                {
                    connection.Execute("dbo.spEmployees_Insert", p, commandType: CommandType.StoredProcedure);
                    model.Id = p.Get<int>("@id");
                }
                catch
                {
                    model.Id = 0;
                }

                return model.Id;
            }
        }

        public int Update(Employee model)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var p = new DynamicParameters();
                p.Add("@Name", model.Name);
                p.Add("@Department", model.Department);
                p.Add("@DateOfJoining", model.DateOfJoining);
                p.Add("@PhotoFileName", model.PhotoFileName);
                p.Add("@Id", model.Id);

                try
                {
                    connection.Execute("dbo.spEmployees_Update", p, commandType: CommandType.StoredProcedure);
                }
                catch
                {
                    model.Id = 0;
                }

                return model.Id;
            }
        }

        public int Delete(int id)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var p = new DynamicParameters();
                p.Add("@Id", id);

                try
                {
                    id = connection.Execute("dbo.spEmployees_Delete", p, commandType: CommandType.StoredProcedure);
                }
                catch
                {
                    // TODO: Implements logging
                    id = 0;
                }

                return id;
            }
        }
    }
}