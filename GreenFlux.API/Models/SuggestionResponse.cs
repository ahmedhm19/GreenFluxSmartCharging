using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenFlux.API.Models
{
    public class SuggestionResponse
    {

        public string ErrorMessage { get; set; }
        public string Hint { get { return "Delete some connectors suggested and retry !"; } }
        public int SuggestionsCount
        {
            get
            {
                return Suggestions.Count;
            }
        }

        public List<SuggestionModel> Suggestions { get; set; } = new List<SuggestionModel>();


    }
}
