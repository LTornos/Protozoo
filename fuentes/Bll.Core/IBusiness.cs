using System;
using Protozoo.Core.Entities;

namespace Protozoo.Core
{
    
    public interface IBusiness
    {
        event Action<string> SomethingIsHappening;
        Entity DoSomething(int context);
    }
}
