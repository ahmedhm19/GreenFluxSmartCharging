using System;

namespace GreenFlux.DataAccess.Exceptions
{
    [Serializable]
   public class DependentNotFoundException : DataException
    {

        public string DependentKeyName { get; }
        
        public object DependentKeyValue { get; }

        public DependentNotFoundException(string dependentKeyName, object dependentKeyValue)
          : base($"{dependentKeyName} with this value ({dependentKeyValue}) does not exist !")
        {
            DependentKeyName = dependentKeyName;
            DependentKeyValue = dependentKeyValue;
        }

        public DependentNotFoundException() : base()
        {

        }

        public DependentNotFoundException(string message)  : base(message)
        {  }

        public DependentNotFoundException(string message, Exception inner)
            : base(message, inner) { }

    }
}
