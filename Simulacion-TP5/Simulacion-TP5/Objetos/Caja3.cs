using System.Collections.Generic;

namespace Simulacion_TP5.Objetos
{
    public class Caja3
    {

        
       
        public double tiempoAtencion { get; set; }
        public int finAtencion { get; set; }
        public string Estado { get; set; }
        public Queue<Cliente> cola;
        public Queue<Cliente> Cola { get => cola; set => cola = value; }

        public Caja3()
        {
            Estado = "L";
            Cola = new Queue<Cliente>();
        }



    }
}
