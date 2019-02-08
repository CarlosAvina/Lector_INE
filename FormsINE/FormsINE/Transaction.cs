using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsINE
{
    public class Transaction
    {
        public long id { get; set; }
        public Persona persona { get; set; }
        public string date_transaction { get; set; }
        public string hour_transaction { get; set; }
        public bool status { get; set; }

    }
}
