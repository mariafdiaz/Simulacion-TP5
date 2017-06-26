using System;
using System.Collections.Generic;

namespace Simulacion_TP5.Objetos
{
    public class Gondola 
    {
        public string Estado { get; set; }
        public Queue<Cliente> cola;
        public Queue<Cliente> Cola { get => cola; set => cola = value; }
        public double finAtencion { get; set; }
        public Cliente clienteSirviendose;//Cliente actual que esta siendo atendido
        

        public Gondola()
        {
            Estado = "L";
            cola = new Queue<Cliente>();
            clienteSirviendose = null;
        }

        public int generarCantArticulos( double rnd)
        {
            int cant = 0;
            

            if (rnd >= 0 && rnd < 20) cant = 1;
            else if (rnd >= 20 && rnd < 40) cant = 2;
            else if (rnd >= 40 && rnd < 50) cant = 3;
            else if (rnd >= 50 && rnd < 70) cant = 4;
            else if (rnd >= 70 && rnd < 100) cant = 5;

            return cant;
        }

       
    }
}
