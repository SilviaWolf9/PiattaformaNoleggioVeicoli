using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiattaformaNoleggioVeicoli.Business.Managers
{
    public class AutoMapperConfigurationManager
    {
        public static MapperConfiguration GetConfiguration()        // configurazione dell'auto mapper a(model) da(model)
        {
            var configuration = new MapperConfiguration(cfg => {
                cfg.CreateMap<Models.NoleggiModel, Models.NoleggiModelView>().ReverseMap();
                cfg.CreateMap<Models.NoleggiTrovatiModelView, Models.NoleggiModelView>().ReverseMap();
                cfg.CreateMap<Models.DettaglioVeicoloModelView, Models.VeicoliModel>().ReverseMap();
                cfg.CreateMap<Models.VeicoliTrovatiModelView, Models.VeicoliModel>().ReverseMap();
                cfg.CreateMap<Models.VeicoliTrovatiModelView, Models.DettaglioVeicoloModelView>().ReverseMap();
                cfg.CreateMap<Models.ClientiModel, Models.ClientiTrovatiModelView>().ReverseMap();
            });
            return configuration;
        }
    }
}
