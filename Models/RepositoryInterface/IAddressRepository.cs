using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersRec.APIrest.Models.RepositoryInterface
{
    public interface IAddressRepository
    {
        IEnumerable<Address> GetAddresses();
        Address GetAddress(string cep);
        Task<Address> Create(long cep);
        public Address Update(string oldCep, long newCep);
    }
}
