using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Protozoo.DAL.Core
{
    public static class DalCommandFactory
    {
        // http://msdn.microsoft.com/en-us/library/3y322t50.aspx
        // http://msdn.microsoft.com/en-us/library/4xxf1410%28v=vs.90%29.aspx
        // http://geekswithblogs.net/rgupta/archive/2008/12/01/dynamically-creating-types-using-reflection-and-setting-properties-using-reflection.emit.aspx
        public static T Create<T>()
        {
            Type instanceType = typeof(T);
            // Assembly
            AppDomain domain = AppDomain.CurrentDomain;
            AssemblyName asmName = new AssemblyName(Guid.NewGuid().ToString());
            AssemblyBuilder asmBuilder = domain.DefineDynamicAssembly(asmName, AssemblyBuilderAccess.RunAndSave);
            // modulo
            ModuleBuilder modBuilder = asmBuilder.DefineDynamicModule(asmName.Name, asmName.Name + ".dll");
            // tipos
            TypeBuilder tb = modBuilder.DefineType(instanceType.Name + "_dynProx", TypeAttributes.Public);
            tb.AddInterfaceImplementation(instanceType);
            {
                foreach (PropertyInfo pi in ModelInspector.MembersDefinedAsParameters<T>())
                {
                    string name = pi.Name;
                    string field = "_" + name.ToLower();
                    TypeBuilder t = tb;
                    Type typ = pi.PropertyType;

                    FieldBuilder fieldBldr = t.DefineField(field, typ, FieldAttributes.Private);
                    PropertyBuilder propBldr = t.DefineProperty(name, System.Reflection.PropertyAttributes.HasDefault, typ, null);
                    MethodAttributes getSetAttr = MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig | MethodAttributes.Virtual;
                    MethodBuilder getPropBldr = t.DefineMethod("get_" + name, getSetAttr, typ, Type.EmptyTypes);
                    ILGenerator getIL = getPropBldr.GetILGenerator();
                    getIL.Emit(OpCodes.Ldarg_0);
                    getIL.Emit(OpCodes.Ldfld, fieldBldr);
                    getIL.Emit(OpCodes.Ret);
                    MethodBuilder setPropBldr = t.DefineMethod("set_" + name, getSetAttr, null, new Type[] { typ });
                    ILGenerator setIL = setPropBldr.GetILGenerator();
                    setIL.Emit(OpCodes.Ldarg_0);
                    setIL.Emit(OpCodes.Ldarg_1);
                    setIL.Emit(OpCodes.Stfld, fieldBldr);
                    setIL.Emit(OpCodes.Ret);
                    propBldr.SetGetMethod(getPropBldr);
                    propBldr.SetSetMethod(setPropBldr);
                    t.DefineMethodOverride(setPropBldr, typeof(T).GetProperty(name).GetSetMethod());
                    t.DefineMethodOverride(getPropBldr, typeof(T).GetProperty(name).GetGetMethod());
                }
            }

            Type newType = tb.CreateType();

            return (T)System.Activator.CreateInstance(newType);
        }
    }
}
