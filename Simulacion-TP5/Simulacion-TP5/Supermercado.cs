using Simulacion_TP5.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion_TP5
{
    class Supermercado
    {
        //VARIABLES//
        private Random RND = new Random();  //EL MOMENTO DE TIEMPO EN EL QUE OCURRE CADA EVENTO//
        private double reloj; //declaramos el reloj 
        private double aleatorioLlegadaCliente; //es el rnd que se muestra que calcula el tiempo entre llegadas
        private double llegadaCliente; //tiempo en que llega un cliente
        private double proximaLlegadaCliente; //proxima llegada del cliente - Metodo calcular tiempo proxima llegada(parametro reloj actual + cant de minutos para prox llegada de pasajero)
        private double aleatorioRecorrido; 
        private double idRecorrido;




        //GET Y SET//
        public double Reloj { get {return reloj; } set { reloj = value; }} 
        public double AleatorioLlegadaCliente { get { return aleatorioLlegadaCliente; } set { aleatorioLlegadaCliente = value; }}     
        public double LlegadaCliente { get { return llegadaCliente; } set { llegadaCliente = value; }}   
        public double ProximaLlegadaCliente { get { return proximaLlegadaCliente; } set { proximaLlegadaCliente = value; }}
        public double AleatorioRecorrido { get { return aleatorioRecorrido; }    set { aleatorioRecorrido = value; }}    
        public double IDRecorrido { get { return idRecorrido; } set { idRecorrido = value; }}




        //METODOS MATEMATICOS//
        public double generarPoisson(double lambda, double RND)
        {
            double p = 1;
            double x = 0;
            double u = 0;
            double a = Math.Exp(-lambda);
            do
            {
                u = RND;
                p = p * u;
                x++;

            } while (p >= a);
            return x;
        }



        public double generarUniforme(double min, double max)
        {
            return Math.Round(RND.NextDouble() * (max - min) + min, 4);
        }
        public void sigEvento()
        {


        }



        //METODOS PARA LOS RECORRIDOS//
        private int generarRecorrido(double rnd) //para ver en donde cae el aleatorio
        {
            int idRecorrido=0;
            if (rnd >= 0 || rnd < 20) { idRecorrido = 1; } //Verduleria-Panaderia
            if (rnd >= 30 || rnd < 50) { idRecorrido = 2; } //Verduleria-Carniceria-Gondola 
            if (rnd >= 50 || rnd < 60) { idRecorrido = 3; } //Panaderia
            if (rnd >= 60 || rnd < 80) { idRecorrido = 4; }//Carniceria-Panaderia-Gondola-Verduleria
            if (rnd >= 80 || rnd < 100) { idRecorrido = 5; }//Gondola
            return idRecorrido;
        }

        public double generarAleatorio() 
        {
            Random RND = new Random();
            double rnd = Math.Round(RND.NextDouble(), 4);
            return rnd;

        }


}
}
