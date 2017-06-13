using Simulacion_TP5.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion_TP5.Objetos
{
    public class Caja1
    {

        private Queue<Cliente> cola = new Queue<Cliente>();
        private string estado { get; set; }
        public double tiempoAtencion { get; set; }
        public int finAtencion { get; set; }

        public Caja1()
        {
            estado = "L";
            cola = new Queue<Cliente>();
        }



    }
}
