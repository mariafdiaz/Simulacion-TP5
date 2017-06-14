using System;
using System.Collections.Generic;

namespace Simulacion_TP5.Objetos
{
    public class Gondola 
    {
        public string Estado { get; set; }
        public Queue<Cliente> cola;
        public Queue<Cliente> Cola { get => cola; set => cola = value; }
        public int finAtencion { get; set; }

        public Gondola()
        {
            Estado = "L";
            cola = new Queue<Cliente>();
        }

        public int generarCantArticulos()
        {
            int cant = 0;
            Random rndAux = new Random();
            double rnd = Math.Round(rndAux.NextDouble(), 4);

            if (rnd >= 0 || rnd < 20) cant = 1;
            if (rnd >= 20 || rnd < 40) cant = 2;
            if (rnd >= 40 || rnd < 50) cant = 3;
            if (rnd >= 50 || rnd < 70) cant = 4;
            if (rnd >= 70 || rnd < 100) cant = 5;

            return cant;
        }

        public double getFinAtencion(int cantArticulos, double hora_llegada)
        {
            // tiempo de atencion = 1' por cada articulo
            return hora_llegada + cantArticulos;
        }
    }
}
