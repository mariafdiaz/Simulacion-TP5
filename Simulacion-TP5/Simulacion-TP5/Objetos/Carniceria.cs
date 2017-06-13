using System.Collections.Generic;


namespace Simulacion_TP5.Objetos
{
    public class Carniceria 
    {
        private Queue<Cliente> cola = new Queue<Cliente>();
        private string estado { get; set; }
        public double tiempoAtencion { get; set; }
        public int finAtencion { get; set; }

        public Carniceria()
        {
            estado = "L";
            cola = new Queue<Cliente>();
        }


    }
    
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

//public int calcularFinAtencion(int inicioAtencion)
//{
//    Random RND = new Random();
//    double x = 0;

//  //  if (Estado == "L")
//   // {
//        x = Math.Round(RND.NextDouble() * (B - A) + A, 4);
//    //    finAtencion = tiempoAtencion + inicioAtencion;
//  //  }
//   // else
//   // {
//        tiempoAtencion = 0;
//        finAtencion = 0;
//    }
//    return (int)x;
//}
