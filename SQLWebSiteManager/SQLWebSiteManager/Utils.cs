using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SQLWebSiteManager
{
    public class Utils<T> : IDisposable
    {
        private static object _instance = null;

        /// <summary>
        /// Metodo Genérico para Obtenção da istancia da classe
        /// caso a istancia já exista ele apenas retorna a mesma, caso não ele cria.
        /// </summary>
        /// <returns>retorna a istancia da classe requerida</returns>
        public static T GetInstace()
        {
            if (_instance == null)
            {
                var obj = new Utils<T>();
                _instance = Activator.CreateInstance<T>();
            }

            return (T)_instance;
        }

        bool disposed = false;
        // Instantiate a SafeHandle instance.
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            _instance = null;
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                // Free any other managed objects here.
                //
            }

            disposed = true;
        }
    }
}
