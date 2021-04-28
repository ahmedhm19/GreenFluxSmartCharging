using System;
using System.Collections.Generic;
using System.Text;

namespace GreenFlux.Service.Tools
{
   public  class Target
    {
      
        public float Value { get; private set; }
        public TargetSources Source;

        public void SetValue(float value , TargetSources source)//source of value
        {
            //Keep previous values in history before changing
            PreviousTarget = new Target { Value = this.Value, Source = this.Source };

            //set new values
            this.Value = value;
            this.Source = source;
            
        }

        public static Target PreviousTarget { get; private set; }

    }



    public enum TargetSources
    {
        Connectors,
        Calculated
    }


}
