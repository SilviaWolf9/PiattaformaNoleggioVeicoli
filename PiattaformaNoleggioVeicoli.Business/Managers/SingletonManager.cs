using PiattaformaNoleggioVeicoli.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiattaformaNoleggioVeicoli.Business.Managers
{
    public class SingletonManager
    {
        private static SingletonManager instance;

        private static VeicoliManager veicoliManager;
        private List<MarcheVeicoliModel> listMarche;
        private const int MINUTI_AGGIORNAMENTO_LISTA_MARCHE = 10;
        private DateTime LastAggiornamentoListaMarche = DateTime.MinValue;

        public List<MarcheVeicoliModel> ListMarche
        {
            get
            {
                if (DateTime.Now > LastAggiornamentoListaMarche.AddMinutes(MINUTI_AGGIORNAMENTO_LISTA_MARCHE))
                {
                    listMarche = veicoliManager.GetMarcheVeicoliList();
                    LastAggiornamentoListaMarche = DateTime.MinValue;
                }
                return listMarche;
            }
        }

        private SingletonManager()
        {
            veicoliManager = new VeicoliManager();
            listMarche = veicoliManager.GetMarcheVeicoliList();
        }
        public static SingletonManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new SingletonManager();
                return instance;
            }
        }

    }
}
