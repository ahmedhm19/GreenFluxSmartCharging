using AutoMapper;
using GreenFlux.Model;
using GreenFlux.Service.Tools;
using System.Collections.Generic;

namespace GreenFlux.API.Models
{
    public class SuggestionModel
    {

       public List<ConnectorModel> Connectors { get; set; }

    }

    public class SuggestionProfile : Profile
    {

        public SuggestionProfile()
        {
            CreateMap<ISuggestion, SuggestionModel>()
               .ConvertUsing<ISuggestionToSuggestionModelConverter>();

            CreateMap<SingleElementSuggestion, SuggestionModel>()
            .ReverseMap();

            CreateMap<MultipleElementSuggestion, SuggestionModel>()
              .ReverseMap();


        }

    }

    public class ISuggestionToSuggestionModelConverter : ITypeConverter<ISuggestion, SuggestionModel>
    {
        public SuggestionModel Convert(ISuggestion source, SuggestionModel destination, ResolutionContext context)
        {

            SuggestionModel suggestionModel = null;

            if (source is SingleElementSuggestion)
            {
                var ses = (SingleElementSuggestion)source;

                //map other properties if exist
                suggestionModel = context.Mapper.Map<SingleElementSuggestion, SuggestionModel>(ses);
                //map connector
                var connectorModel = context.Mapper.Map<Connector, ConnectorModel>(ses.Element);
                //put element inside collection
                suggestionModel.Connectors = new List<ConnectorModel> { connectorModel };

            }
            else if (source is MultipleElementSuggestion)
            {

                var mes = (MultipleElementSuggestion)source;

                //map other properties if exist
                suggestionModel = context.Mapper.Map<MultipleElementSuggestion, SuggestionModel>(mes);
                //map connector
                var connectorModels = context.Mapper.Map<List<Connector>, List<ConnectorModel>>(mes.Elements);
                //put element inside collection
                suggestionModel.Connectors = connectorModels;

            }


            return suggestionModel;
        }
    }


}
