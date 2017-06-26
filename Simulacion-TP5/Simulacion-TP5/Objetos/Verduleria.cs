using System;
using System.Collections.Generic;

namespace Simulacion_TP5.Objetos
{
    public class Verduleria
    {

        public string Estado { get; set; }
        public Queue<Cliente> cola;
        public Queue<Cliente> Cola { get => cola; set => cola = value; }
        public double tiempoAtencion { get; set; }
        public double finAtencion { get; set; }
        public Cliente clienteSirviendose;//Cliente actual que esta siendo atendido
       

        public Verduleria()
        {
            Estado = "L";
            cola = new Queue<Cliente>();
            clienteSirviendose = null;
        }
        
    }
}


