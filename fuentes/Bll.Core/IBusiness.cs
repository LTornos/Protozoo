using System;
using Bll.Entities;

namespace Bll.Core
{
    public interface IBusiness
    {
        event EventHandler SomethingIsHappening;
        Entity DoSomething(int context);
    }
}
