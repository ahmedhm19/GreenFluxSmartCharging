using GreenFlux.Model.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace GreenFlux.Model
{
    public class Group : Entity
    {

        public override object[] GetKey()
        {
                return new object[] { Id };     
        }
        public long Id { get; set; }
        public string Name { get; set; } 
        public float Capacity { get; set; }
   
    }
}
