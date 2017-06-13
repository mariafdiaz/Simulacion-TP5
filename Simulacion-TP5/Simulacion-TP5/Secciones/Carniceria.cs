using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM_TP5
{
    class Carniceria : Seccion
    {
      
        public double tiempoAtencion { get; set; }

 
        private double A = 1;
        private double B = 3;

        public double RNDVCarniceria { get; set; }

        public Carniceria()
        {
            string Estado = "L";
            cola = new Cola();
        }

      

        //public String estadoCliente()
        //{
        //    string estadoCliente = "";
        //    if (Estado == "Oc")
        //    {
        //        estadoCliente = "EAV";
        //        Cola = Cola + 1;
        //    }
        //    else
        //    {
        //        estadoCliente = "SAV";
        //        Estado = "Oc";
        //        if (Cola > 0)
        //        {
        //            Cola = Cola - 1;
        //        }
        //    }
        //    return estadoCliente;
        //}
        
        public int calcularFinAtencion(int inicioAtencion)
        {
            Random RND = new Random();
            double x = 0;

            if (Estado == "L")
            {
                x = Math.Round(RND.NextDouble() * (B - A) + A, 4);
                finAtencion = tiempoAtencion + inicioAtencion;
            }
            else
            {
                tiempoAtencion = 0;
                finAtencion = 0;
            }
            return (int)x;
        }


    }
    
}
