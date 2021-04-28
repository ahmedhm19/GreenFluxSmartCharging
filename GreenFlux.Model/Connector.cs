using GreenFlux.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenFlux.Model
{
    public class Connector : Entity
    {

        public override object[] GetKey()
        {
            return new object[] { CK_Id, ChargeStationId };
        }

        public byte CK_Id { get; set; }
        public long ChargeStationId { get; set; }
        public float MaxCurrent { get; set; }

        [NotMapped]
        public int Index;//TODO to remove


    }

}
