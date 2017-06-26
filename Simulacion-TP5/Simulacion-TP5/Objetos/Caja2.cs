using System.Collections.Generic;

namespace Simulacion_TP5.Objetos
{
    public class Caja2
    {
        
       
        public double tiempoAtencion { get; set; }
        public double finAtencion { get; set; }
        public string Estado { get; set; }
        public Queue<Cliente> cola;
        public Queue<Cliente> Cola { get => cola; set => cola = value; }
        public Cliente clienteSirviendose;//Cliente actual que esta siendo atendido
        

        public Caja2()
        {
            Estado = "L";
            cola = new Queue<Cliente>();
            clienteSirviendose = null;
        }
    }
}
