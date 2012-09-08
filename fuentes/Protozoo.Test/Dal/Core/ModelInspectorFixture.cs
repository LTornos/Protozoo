using System;
using System.Collections.Generic;
using System.Reflection;
using Protozoo.DAL.Core;
using NUnit.Framework;

namespace Protozoo.Test.DAL.Core
{
    [TestFixture]
    public class ModelInspectorFixture
    {
        [StoredProcedure]
        public interface Foo 
        {
            [Parameter]
            string prop01 { get; set; }

            string prop02 { get; set; }

            [Parameter(Name="MappedParam")]
            string prop03 { get; set; }
        }

        public interface NotFoo { }

        [StoredProcedure(Name="MappedName")]
        public interface FooMapped {}

        [Test]
        public void get_command_name_from_type()
        { 
            Assert.AreEqual("Foo", ModelInspector.GetCommandName(typeof(Foo)));
        }

        [Test]
        public void get_command_name_from_model_is_not_a_stored_procedure()
        {
            Assert.AreEqual(String.Empty, ModelInspector.GetCommandName(typeof(NotFoo)));
        }

        [Test]
        public void get_command_name_from_type_generic_method()
        {
            Assert.AreEqual("Foo", ModelInspector.GetCommandName<Foo>());
        }

        [Test]
        public void get_command_name_from_model_is_not_a_stored_procedure_generic_method()
        {
            Assert.AreEqual(String.Empty, ModelInspector.GetCommandName<NotFoo>());
        }

        [Test]
        public void get_name_from_mapped_model()
        {
            Assert.AreEqual("MappedName", ModelInspector.GetCommandName(typeof(FooMapped)));
        }

        [Test]        
        public void Is_defined_as_parameter() 
        {
            Assert.IsTrue(ModelInspector.IsDefinedAsParameter("prop01", typeof(Foo)));
            Assert.IsFalse(ModelInspector.IsDefinedAsParameter("prop02", typeof(Foo)));            
        }

        
        [Test]
        [ExpectedException(typeof(System.Exception))]
        public void Is_defined_as_parameter_invalid_property()
        {
            ModelInspector.IsDefinedAsParameter("InvalidPropertyName", typeof(Foo));
        }

        [Test]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void Is_defined_as_parameter_null_propertyInfo()
        {
            ModelInspector.IsDefinedAsParameter(null);
        }

        
        [Test]
        public void get_parameter_name() 
        {
            Assert.AreEqual("MappedParam", ModelInspector.GetParameterName(typeof(Foo).GetProperty("prop03")));
            Assert.AreEqual("prop01", ModelInspector.GetParameterName(typeof(Foo).GetProperty("prop01")));
        }
                
        [Test]
        public void get_members_defined_as_parameter() 
        {
            List<PropertyInfo> members = new List<PropertyInfo>(ModelInspector.MembersDefinedAsParameters<Foo>());
            Assert.AreEqual(2, members.Count);
            Assert.AreEqual("prop01", members[0].Name);
        }

        [Test]
        public void get_parameter_definition() 
        {
            // Propiedad marcada como parametro
            Assert.IsNotNull(ModelInspector.ParameterDefinition(typeof(Foo).GetProperty("prop01")));
            // Propiedad no marcada como parametro
            Assert.IsNull(ModelInspector.ParameterDefinition(typeof(Foo).GetProperty("prop02")));
        }
    }
}
