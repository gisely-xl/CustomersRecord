using CustomersRec.APIrest.Data;
using CustomersRec.APIrest.Models;
using CustomersRec.APIrest.Models.RepositoryInterface;
using CustomersRec.APIrest.Services;
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

    public class AddressRepository : IAddressRepository
    {
        private readonly string _connectString;
        private readonly ViaCepService _viaCepService;
        private readonly RecordContext _context;

        public AddressRepository(IConfiguration configuration, ViaCepService viaCepService, RecordContext context)
        {
            _connectString = configuration.GetConnectionString("Default");
            _viaCepService = viaCepService;
            _context = context;

        }

        public async Task<Address> Create(long cep)
        {
            var address = _viaCepService.GetAddress(cep).Result;
            _context.Adresses.Add(address);
            await _context.SaveChangesAsync();
            return address;
        }

        public Address GetAddress(string cep)
        {
            using var connection = new SqlConnection(_connectString);
            var getCepQuery = $"SELECT * FROM Adresses WHERE Cep = '{cep}'";
            var address = connection.QueryFirstOrDefault<Address>(getCepQuery);
            return address;
        }

        public IEnumerable<Address> GetAddresses()
        {
            using var connection = new SqlConnection(_connectString);
            var getQuery = $"SELECT * FROM Adresses";
            var adresses = SqlMapper.Query<Address>(connection, getQuery);
            return adresses;
        }

        public Address Update(string oldCep, long newCep)
        {
            var newAddress = _viaCepService.GetAddress(newCep).Result;
            var oldAddress = GetAddress(oldCep);

            _context.Adresses.Remove(oldAddress);
            _context.Adresses.Add(newAddress);

            
            return newAddress;
        }
    }
}

