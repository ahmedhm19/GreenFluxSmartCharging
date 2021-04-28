using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GreenFlux.Model.Base
{
  public abstract class Entity 
    {
    
        public abstract object[] GetKey();

    }
}
