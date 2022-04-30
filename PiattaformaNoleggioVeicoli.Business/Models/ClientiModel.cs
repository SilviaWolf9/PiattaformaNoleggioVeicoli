using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiattaformaNoleggioVeicoli.Business.Models
{
    [Serializable]
    public class ClientiModel
    {
        public int Id { get; set; }
        public string Cognome { get; set; }
        public string Nome { get; set; }
        public DateTime? DataNascita { get; set; }
        public string CodiceFiscale { get; set; }
        public string Patente { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Indirizzo { get; set; }
        public string NumeroCivico { get; set; }
        public string Cap { get; set; }
        public string Citta { get; set; }
        public string Comune { get; set; }
        public string Provincia { get; set; }
        public string Nazione { get; set; }
        public string Note { get; set; }
    }
}
