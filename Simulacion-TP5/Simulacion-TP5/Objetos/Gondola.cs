using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulacion_TP5.Objetos;

namespace SIM_TP5
{
    class Gondola 
    {
        private Queue<Cliente> cola = new Queue<Cliente>();
        private string estado { get; set; }
        public int finAtencion { get; set; }

        public Gondola()
        {
            estado = "L";
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
