using AutoMapper;
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
        private const int MINUTI_AGGIORNAMENTO_LISTA_TIPI_ALIMENTAZIONE = 1440;
        private DateTime LastAggiornamentoListaTipiAlimentazione = DateTime.MinValue;
        private IMapper mapper;
        private List<MarcheVeicoliModel> listMarchePossedute;
        private const int MINUTI_AGGIORNAMENTO_LISTA_MARCHE_POSSEDUTE = 10;
        private DateTime LastAggiornamentoListaMarchePossedute = DateTime.MinValue;

        public IMapper Mapper
        {
            get
            {
                return mapper;
            }
        }

        public List<MarcheVeicoliModel> ListMarche
        {
            get
            {
                if (DateTime.Now > LastAggiornamentoListaMarche.AddMinutes(MINUTI_AGGIORNAMENTO_LISTA_MARCHE))      // mi permette di aggiornare la listaMarche ogni 10 minuti
                {
                    listMarche = veicoliManager.GetMarcheVeicoliList();
                    LastAggiornamentoListaMarche = DateTime.MinValue;
                }
                return listMarche;
            }
        }
        public List<MarcheVeicoliModel> ListMarchePossedute
        {
            get
            {
                if (DateTime.Now > LastAggiornamentoListaMarchePossedute.AddMinutes(MINUTI_AGGIORNAMENTO_LISTA_MARCHE_POSSEDUTE))      // mi permette di aggiornare la listaMarche ogni 10 minuti
                {
                    listMarchePossedute = veicoliManager.GetMarcheVeicoliPossedutiList();
                    LastAggiornamentoListaMarchePossedute = DateTime.MinValue;
                }
                return listMarchePossedute;
            }
        }
        private SingletonManager()
        {
            veicoliManager = new VeicoliManager();
            listMarche = veicoliManager.GetMarcheVeicoliList();
            var configurationAutoMapper = AutoMapperConfigurationManager.GetConfiguration();
            mapper = configurationAutoMapper.CreateMapper();
            listTipoAlimentazione = veicoliManager.GetTipoAlimentazioneList();
            listMarchePossedute = veicoliManager.GetMarcheVeicoliPossedutiList();
        }

        private List<TipoAlimentazioneModel> listTipoAlimentazione;
        public List<TipoAlimentazioneModel> ListTipoAlimentazione
        {
            get {
                if (DateTime.Now > LastAggiornamentoListaTipiAlimentazione.AddMinutes(MINUTI_AGGIORNAMENTO_LISTA_TIPI_ALIMENTAZIONE))       // mi permette di aggiornare la listaTipiAlimentazione ogni 24 ore
                {
                    listTipoAlimentazione = veicoliManager.GetTipoAlimentazioneList();
                    LastAggiornamentoListaTipiAlimentazione = DateTime.MinValue;

                }
                return listTipoAlimentazione; 
            }
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
        public void AggiornamentoListaMarcheVeicoliPosseduti()
        {
            listMarchePossedute = veicoliManager.GetMarcheVeicoliPossedutiList();
            LastAggiornamentoListaMarchePossedute = DateTime.MinValue;
        }
    }
}
