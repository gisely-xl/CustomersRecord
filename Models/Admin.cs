using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersRec.APIrest.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
