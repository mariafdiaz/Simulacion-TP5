using System.Collections.Generic;

namespace Simulacion_TP5.Objetos
{
    public class Caja
    {
        
       
        public double tiempoAtencion { get; set; }
        public int finAtencion { get; set; }
        public string Estado { get; set; }
        public Cliente atendido { get; set; }
        public Queue<Cliente> cola;
        public Queue<Cliente> Cola { get => cola; set => cola = value; }

        public Caja()
        {
            atendido = null;
            Estado = "L";
            cola = new Queue<Cliente>();
        }
    }
}
