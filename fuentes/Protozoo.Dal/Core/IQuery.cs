using System;
using System.Data;

namespace Protozoo.DAL.Core
{
    public interface IQuery
    {
        /// <summary>Evento que se dispara antes de la ejecución contra la base de datos</summary>
        event EventHandler BeforeExecute;

        /// <summary>Evento disparado después de recibir los resultados de la base de datos</summary>
        event EventHandler AfterExecute;

        string Name { get; }
        IDataReader Execute();
        IDataReader ResultSet { get; }
    }
}
