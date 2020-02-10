#region Referencias
using System;
using System.Collections;
using System.Linq;
using Spring.Context;
using Spring.Context.Support;
#endregion

namespace GAP.Appointments.Commo.ProveedoresDependencias
{
    /// <summary>
    /// Clase que provee métodos para localizar y retornar un servicio específico desde el 
    /// contenedor de instancias
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LocalizadorServicio<T>
    {
        /// <summary>
        /// Retorna la instancia del servicio llamado nombreServicio
        /// en el contexto
        /// </summary>
        /// <returns>Instancia del servicio solicitado</returns>
        public static T GetService()
        {
            var result = LocalizadorServicio.GetService(typeof(T));

            if (result != null)
                return (T)result;
            return default(T);
        }
    }

    /// <summary>
    /// Clase utilizada para obtener la referencia a los servicios disponibles
    /// </summary>
    public class LocalizadorServicio
    {
        /// <summary>
        /// Palabra clave para identificar un objeto en el contenedor como servicio
        /// </summary>
        private const string ServiceKey = "Service";

        /// <summary>
        /// Retorna la instancia del servicio acorde al tipo
        /// </summary>
        /// <param name="serviceType">Tipo a buscar</param>
        /// <returns>Instancia del servicio solicitado</returns>
        public static object GetService(Type serviceType)
        {
            return GetService(serviceType, ContextRegistry.GetContext(), true);
        }

        /// <summary>
        /// Retorna la instancia del servicio acorde al tipo
        /// </summary>
        /// <param name="serviceType">Tipo a buscar</param>
        /// <param name="context">Contexto a utilizar</param>
        /// <returns>Instancia del servicio solicitado</returns>
        public static object GetService(Type serviceType, IApplicationContext context)
        {
            return GetService(serviceType, context, true);
        }

        /// <summary>
        /// Retorna la instancia del servicio acorde al tipo
        /// </summary>
        /// <param name="serviceType">Tipo a buscar</param>
        /// <param name="context">Contexto a utilizar</param>
        /// <param name="throwException">Indica si debe arrojar excepción en caso de no encontrar el servicio</param>
        /// <returns>Instancia del servicio solicitado</returns>
        public static object GetService(Type serviceType, IApplicationContext context, bool throwException)
        {
            var dictionary = context.GetObjectsOfType(serviceType);
            if (dictionary.Count == 1)
            {
                //retorna el primero de los objetos de ese tipo ya que no se da el caso una misma interface registrada dos veces
                IEnumerator enumerator = dictionary.Values.GetEnumerator();
                enumerator.MoveNext();
                return enumerator.Current;
            }

            if (!throwException)
                return null;

            //no se ha encontrado un servicio en el contenedor
            //throw new Exception("Error_ServiceLocatorException");
            return null;
        }
    }
}
