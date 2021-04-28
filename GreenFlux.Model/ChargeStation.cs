using GreenFlux.Model.Base;
using System.Collections.Generic;

namespace GreenFlux.Model
{

    public class ChargeStation : Entity
    {

        public ChargeStation()
        {
            Connectors = new List<Connector>();
        }

        public override object[] GetKey()
        {
            return new object[] { Id };
        }

        public long Id { get; set; }
        public string Name { get; set; }        
        public long GroupId { get; set; }

        public List<Connector> Connectors { get; set; }
       
    
    }
}
