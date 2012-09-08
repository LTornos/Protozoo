using Protozoo.DAL.Core;
using NUnit.Framework;

namespace Protozoo.Test.DAL.Core
{
    [TestFixture]
    public class StoredProcedureFixture
    {
        [StoredProcedure]
        public interface sp {}

        [Ignore]
        [Test]        
        public void Ctor()
        { }

        [Test]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void Ctor_null_command()
        { 
            new StoredProcedure<sp>(null);
        }
    }
}
