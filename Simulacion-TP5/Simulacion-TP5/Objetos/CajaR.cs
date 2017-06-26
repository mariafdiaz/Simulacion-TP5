using System.Collections.Generic;

namespace Simulacion_TP5.Objetos
{
    public class CajaR
    {
        private Queue<Cliente> cola;
        
        public double tiempoAtencion { get; set; }
        public double finAtencion { get; set; }
        public Queue<Cliente> Cola { get => cola; set => cola = value; }
        public string Estado { get; set; }
        public Cliente clienteSirviendose;//Cliente actual que esta siendo atendido
        

        public CajaR()
        {
            Estado = "L";
            Cola = new Queue<Cliente>();
            clienteSirviendose = null;
        }
    }
}

