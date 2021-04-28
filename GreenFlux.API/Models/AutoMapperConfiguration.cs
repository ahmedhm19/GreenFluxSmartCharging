using AutoMapper;

namespace GreenFlux.API.Models
{
    public class AutoMapperConfiguration
    {

        public static MapperConfiguration Initialize()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new GroupProfile());
                cfg.AddProfile(new ChargeStationProfile());
                cfg.AddProfile(new ConnectorProfile());
                cfg.AddProfile(new SuggestionProfile());
            });

            return config;
        }


    }
}
