using GreenFlux.Model;
using System.Collections.Generic;

namespace GreenFlux.Service.Base
{
    public interface IConnectorKeyGenerator
    {

        byte GenerateKey(List<Connector> existingConnectors);

 
    }
}
