using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM_TP5
{
    class Verduleria : Seccion
    {
       

        public int tiempoAtencion { get; set; }

        public int finAtencion { get; set; }

       

        private double lambdaVerduleria = 2.00;

        public double RNDVerduleria { get; set; }

        public Verduleria()
        {
            Estado = "L";
            cola = new Cola();
        }

        public Verduleria(string estado, int tiempo, int cola, int tiempoFin)
        {
            Estado = estado;
            tiempoAtencion = tiempo;
            //Cola = cola;
            finAtencion = tiempoFin;
        }

        public String estadoCliente()
        {
            string estadoCliente = "";
            if (Estado == "Oc")
            {
                estadoCliente = "EAV";
               // Cola = Cola + 1;
            }
            else
            {
                estadoCliente = "SAV";
                Estado = "Oc";
               // if (Cola > 0)
                {
                   // Cola = Cola - 1;
                }
            }
            return estadoCliente;
        }

        public int calcularFinAtencion(int inicioAtencion)
        {
            this.generarServicioVerduleria(inicioAtencion);
            return finAtencion;
        }

        public void generarServicioVerduleria(int reloj)
        {
            Random RND = new Random();
            if (Estado == "L")
            {
                double p;
                double x;
                double u = 0;
                double yy = 0;
                double a = Math.Exp(-lambdaVerduleria);

                p = 1;
                x = 0;
                do
                {
                    yy = u = RND.NextDouble();
                    RNDVerduleria = Math.Round(yy, 4);
                    p = p * u;
                    x++;
                } while (p >= a);

                //tiempoAtencion = Math.Round(x, 2);
                finAtencion = tiempoAtencion + reloj;
            }
            else
            {
                tiempoAtencion = 0;
                finAtencion = 0;
            }
        }
    }
}
