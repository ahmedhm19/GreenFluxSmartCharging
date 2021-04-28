using GreenFlux.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreenFlux.Service.Tools
{
  public  class MultipleElementSuggestion : ISuggestion
    {
       public List<Connector> Elements { get; set; }
     
    }
}
