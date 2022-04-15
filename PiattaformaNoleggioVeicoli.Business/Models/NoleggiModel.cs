using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiattaformaNoleggioVeicoli.Business.Models
{
    public class NoleggiModel
    {
        public int Id { get; set; }
        public int IdVeicolo { get; set; }
        public int IdCliente { get; set; }
        public DateTime DataInizio { get; set; }
        public DateTime DataFine { get; set; }
        public bool IsInCorso { get; set; }
    }
}
