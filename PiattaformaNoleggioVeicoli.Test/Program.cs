using PiattaformaNoleggioVeicoli.Business.Managers;
using PiattaformaNoleggioVeicoli.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiattaformaNoleggioVeicoli.Test
{
    public class Program
    {
        static void Main(string[] args)
        {
            var clientiManager = new ClientiManager();
            var noleggiManager = new NoleggiManager();
            var veicoliManager = new VeicoliManager();
            //var clienteModel = new ClientiModel()
            //{
            //    Cognome = "Moretti",
            //    Nome = "Silvia",
            //    DataNascita = DateTime.Parse("09/06/1989"),
            //    CodiceFiscale = "MRTSLV89H49H501A",
            //    Patente = "cxeadsjhvjnh9",
            //    Telefono = "3931270843",
            //    Email = "ciao@gmail.com",
            //    Indirizzo = "via col vento",
            //    NumeroCivico = "9",
            //    Cap = "00100",
            //    Citta = "Lecce",
            //    Comune = "LE",
            //    Nazione = "Italia"
            //};
            //clientiManager.InsertCliente(clienteModel);
            //var clienteModel = clientiManager.GetCliente(1);
            //var veicoloModel = veicoliManager.GetVeicolo(1);
            //var noleggioModel = new NoleggiModelView()
            //{
            //    IdCliente = clienteModel.Id,
            //    IdVeicolo = veicoloModel.Id                
            //};
            //noleggiManager.InserisciNoleggio(noleggioModel);
            //noleggioModel.Id = 4;
            //var noleggio = noleggiManager.TerminaNoleggio(noleggioModel);
        }
    }
}
