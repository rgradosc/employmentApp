using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;

namespace Employment.WebApi.Repository
{
    using Models;
    using Configuration;
    using Dapper;

    public class DepartmentRepository
    {
        private const string connectionName = "DefaultConnection";
        private string connectionString = DatabaseConfig.ConnectionString(connectionName);

        public ICollection<Department> GetAll()
        {
            ICollection<Department> departments = new List<Department>();
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                departments = connection.Query<Department>("dbo.spDepartments_GetAll").ToList();

                return departments;
            }
        }

        public Department GetById(int id)
        {
            Department department = null;
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var p = new DynamicParameters();
                p.Add("@Id", id);
                department = connection.Query<Department>("dbo.spDepartments_GetById", p, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return department;
            }
        }

        public int Create(Department model)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var p = new DynamicParameters();
                p.Add("@Name", model.Name);
                p.Add("@id", model.Id, DbType.Int32, ParameterDirection.Output);

                try
                {
                    connection.Execute("dbo.spDepartments_Insert", p, commandType: CommandType.StoredProcedure);
                    model.Id = p.Get<int>("@id");
                }
                catch
                {
                    model.Id = 0;
                }

                return model.Id;
            }
        }

        public int Update(Department model)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var p = new DynamicParameters();
                p.Add("@Name", model.Name);
                p.Add("@Id", model.Id);

                try
                {
                    connection.Execute("dbo.spDepartments_Update", p, commandType: CommandType.StoredProcedure);
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
                    id = connection.Execute("dbo.spDepartments_Delete", p, commandType: CommandType.StoredProcedure);
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