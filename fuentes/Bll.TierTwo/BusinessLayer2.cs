using System;
using Protozoo.Core;
using Protozoo.Core.Entities;

namespace Protozoo.Core.Tier2
{
    public class BusinessLayer2: IBusiness
    {
        public Entity DoSomething(int context)
        {
            SomethingIsHappening("Antes de validar reglas");
            if (context!=0)
            {
                SomethingIsHappening("Reglas validadas");
                return new Entity("John Doe","D000001");
            }        
           throw new Exception("Rule broken doing something");
        }

        public event Action<string> SomethingIsHappening = delegate { };
    }
}
