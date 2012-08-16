using System;
using Bll.Entities;

namespace Protozoo.Core
{
    public interface IBusiness
    {
        event EventHandler SomethingIsHappening;
        Entity DoSomething(int context);
    }
}
