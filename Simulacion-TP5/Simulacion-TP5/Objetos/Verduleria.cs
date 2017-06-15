using System.Collections.Generic;

namespace Simulacion_TP5.Objetos
{
    public class Verduleria
    {
        public string Estado { get; set; }
        public Queue<Cliente> cola;
        public Queue<Cliente> Cola { get => cola; set => cola = value; }
        public double tiempoAtencion { get; set; }
        public int finAtencion { get; set; }        

        public Verduleria()
        {
            Estado = "L";
            cola = new Queue<Cliente>();
        }
    }
}
//public Verduleria(string estado, int tiempo, int cola, int tiempoFin)
//{
//    // Estado = estado;
//    tiempoAtencion = tiempo;
//    //Cola = cola;
//    finAtencion = tiempoFin;
//}

//public String estadoCliente()
//{
//    string estadoCliente = "";
//    // if (Estado == "Oc")
//    {
//        estadoCliente = "EAV";
//        // Cola = Cola + 1;
//   // }
//  //  else
//    } {
//        estadoCliente = "SAV";
//        // Estado = "Oc";
//        // if (Cola > 0)
//        {
//            // Cola = Cola - 1;
//        }
//    }
//    return estadoCliente;
//}

//public int calcularFinAtencion(int inicioAtencion)
//{
//    this.generarServicioVerduleria(inicioAtencion);
//    return finAtencion;
//}

//public void generarServicioVerduleria(int reloj)
//{
//    Random RND = new Random();
//    // if (Estado == "L")
//    {
//        double p;
//        double x;
//        double u = 0;
//        double yy = 0;
//        double a = Math.Exp(-lambdaVerduleria);

//        p = 1;
//        x = 0;
//        do
//        {
//            yy = u = RND.NextDouble();
//            RNDVerduleria = Math.Round(yy, 4);
//            p = p * u;
//            x++;
//        } while (p >= a);

//        //tiempoAtencion = Math.Round(x, 2);
//        finAtencion = tiempoAtencion + reloj;

//   // }
//   // else
//    {
//            tiempoAtencion = 0;
//            finAtencion = 0;
//        }
//    }
//}
