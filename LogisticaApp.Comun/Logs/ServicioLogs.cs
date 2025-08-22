using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticaApp.Comun.Logs
{
    public interface IServicioLogs
    {
        void Informacion(string mensaje);
        void Advertencia(string mensaje);
        void Error(string mensaje, Exception ex = null);
    }

    public class ServicioLogs : IServicioLogs
    {
        private readonly string _carpetaLogs;

        public ServicioLogs(string rutaBase = null)
        {
            // Si no se pasa ruta, se crea en la carpeta de la app
            _carpetaLogs = rutaBase ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");

            // Crear carpeta si no existe
            if (!Directory.Exists(_carpetaLogs))
                Directory.CreateDirectory(_carpetaLogs);
        }

        // Nombre del archivo de log por día
        private string ArchivoLog => Path.Combine(_carpetaLogs, $"Log_{DateTime.Now:yyyy-MM-dd}.txt");

        public void Informacion(string mensaje) => Escribir("INFO", mensaje);
        public void Advertencia(string mensaje) => Escribir("WARN", mensaje);
        public void Error(string mensaje, Exception ex = null)
            => Escribir("ERROR", ex != null ? $"{mensaje} - EXCEPCION: {ex.Message}" : mensaje);

        private void Escribir(string nivel, string mensaje)
        {
            var texto = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{nivel}] {mensaje}";
            File.AppendAllText(ArchivoLog, texto + Environment.NewLine);
        }
    }
}
