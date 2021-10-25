using CustomersRec.APIrest.Models;
using CustomersRec.APIrest.Models.RepositoryInterface;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersRec.APIrest.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly string _connectString;

        public AdminRepository(IConfiguration configuration)
        {
            _connectString = configuration.GetConnectionString("Default");
        }

        public Admin GetAdmin(string name, string password)
        {
            using var connection = new SqlConnection(_connectString);
            var getQuery = $"SELECT A.* FROM Admins A Where A.Name = '{name}' and A.Password = '{password}'";
            var admin = connection.QueryFirstOrDefault<Admin>(getQuery, new { Name = name, Password = password });
            return admin;
        }
    }
}
