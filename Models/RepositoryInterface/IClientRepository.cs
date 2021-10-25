using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersRec.APIrest.Models.RepositoryInterface
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetClients();
        Client GetClient(int id);
       
    }
}
