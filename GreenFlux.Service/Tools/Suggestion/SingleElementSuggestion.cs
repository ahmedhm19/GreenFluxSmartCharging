using GreenFlux.Model;

namespace GreenFlux.Service.Tools
{
    public class SingleElementSuggestion : ISuggestion
    {

        public SingleElementSuggestion(Connector element)
        {
            Element = element;
        }

        public Connector Element { get; set; }


    }
}
