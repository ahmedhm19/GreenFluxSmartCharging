using GreenFlux.Model;
using System.Collections.Generic;

namespace GreenFlux.Service.Tools
{
    public interface ISuggester
    {

       List<ISuggestion> GetSuggestions(List<Connector> connectors, float originalTarget);


    }
}
