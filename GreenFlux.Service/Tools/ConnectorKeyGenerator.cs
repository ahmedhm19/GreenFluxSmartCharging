using GreenFlux.Model;
using GreenFlux.Service.Base;
using GreenFlux.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GreenFlux.Service.Tools
{
    public class ConnectorKeyGenerator : IConnectorKeyGenerator
    {
        public byte GenerateKey(List<Connector> existingConnectors)
        {
            for (byte i = 1; i <= 5; i++)
            {
                if (existingConnectors.Any(c => c.CK_Id == i))

                    continue;

                return i;
            }

            throw new ConnectorKeyGenerationException();
        }
    
    }
}
