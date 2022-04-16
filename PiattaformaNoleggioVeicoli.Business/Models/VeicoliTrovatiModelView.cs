using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiattaformaNoleggioVeicoli.Business.Models
{
    public class VeicoliTrovatiModelView
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modello { get; set; }
        public DateTime DataImmatricolazione { get; set; }
        public bool IsDisponibile { get; set; }
    }
}
