using System;
using System.Collections.Generic;
using Bll.Core;
using Bll.Entities;

namespace Bll.Layer2
{
    public class BusinessLayer2: IBusiness
    {
        public Entity DoSomething(int context)
        {
            SomethingIsHappening(this, EventArgs.Empty);
            if (context!=0)
            {
                SomethingIsHappening(this, EventArgs.Empty);
                return new Entity("John Doe","D000001");
            }        
           throw new Exception("Rule broken doing something");
        }

        public event EventHandler SomethingIsHappening = delegate { };
    }
}
