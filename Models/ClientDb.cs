using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersRec.APIrest.Models
{
    public class ClientDb
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Contact { get; set; }
        public Address Address { get; set; }
    }
}
