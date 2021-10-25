using CustomersRec.APIrest.Data;
using CustomersRec.APIrest.Models;
using CustomersRec.APIrest.Models.RepositoryInterface;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersRec.APIrest.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly string _connectionString;
        private readonly IAddressRepository _addressRepository;
        

        public ClientRepository(IConfiguration configuration, IAddressRepository addressRepository, RecordContext context)
        {
            _connectionString = configuration.GetConnectionString("Default");
            _addressRepository = addressRepository;
      
        }
        
        public Client GetClient(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var getByIdQuery = $"SELECT * FROM CLIENTS WHERE Id = {id}";
            var client = connection.QueryFirstOrDefault<Client>(getByIdQuery, new { Id = id });

            var getAddress = $"SELECT A.* FROM ViaCep A JOIN CLIENTS C ON C.Cep_C = A.Cep WHERE C.Name = '{client.Name}'";
            client.Adresses = SqlMapper.Query<Address>(connection, getAddress);

            return client;
        }

        public IEnumerable<Client> GetClients()
        {
            var getQuery = "SELECT * FROM CLIENTS";
            using var connection = new SqlConnection(_connectionString);
            var clients = SqlMapper.Query<Client>(connection, getQuery);


            foreach (var client in clients)
            {
                var getAddress = $"SELECT A.* FROM Adresses A JOIN CLIENTS C ON C.AddressCep = A.Cep WHERE C.Name = '{client.Name}'";
                client.Adresses = SqlMapper.Query<Address>(connection, getAddress);
                client.Cep_C = client.Adresses.ToList()[0].Cep;
            }


            return clients;
        }

    }
}
