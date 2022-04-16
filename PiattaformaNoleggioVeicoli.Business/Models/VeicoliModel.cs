using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiattaformaNoleggioVeicoli.Business.Models
{
    public class VeicoliModel
    {
        public int Id { get; set; }
        public int? IdMarca { get; set; }
        public string Modello { get; set; }
        public string Targa { get; set; }
        public DateTime? DataImmatricolazione { get; set; }
        public int? IdTipoAlimentazione { get; set; }
        public string Note { get; set; }
        public bool IsDisponibile { get; set; }
        public int IdTipoStato { get; set; }
    }
}
