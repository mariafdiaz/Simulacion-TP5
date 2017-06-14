using System.Collections.Generic;

namespace Simulacion_TP5.Objetos
{
    public class Caja2
    {
        
       
        public double tiempoAtencion { get; set; }
        public int finAtencion { get; set; }
        public string Estado { get; set; }
        public Queue<Cliente> cola;
        public Queue<Cliente> Cola { get => cola; set => cola = value; }

        public Caja2()
        {
            Estado = "L";
            cola = new Queue<Cliente>();
        }
    }
}
