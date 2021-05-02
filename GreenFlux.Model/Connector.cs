using GreenFlux.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenFlux.Model
{
    public partial class Connector : Entity
    {

        public override object[] GetKey()
        {
            return new object[] { CK_Id, ChargeStationId };
        }

        public byte CK_Id { get; set; }
        public long ChargeStationId { get; set; }
        public float MaxCurrent { get; set; }


    }
}
