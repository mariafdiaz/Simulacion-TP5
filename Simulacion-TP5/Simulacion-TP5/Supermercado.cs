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
        private int aleatorioRecorrido; 
        private int idRecorrido;
        private string eventoSiguiente;
        private int contCantClientesAtendidos;


        //GET Y SET//
        public double Reloj { get {return reloj; } set { reloj = value; }} 
        public double AleatorioLlegadaCliente { get { return aleatorioLlegadaCliente; } set { aleatorioLlegadaCliente = value; }}     
        public double LlegadaCliente { get { return llegadaCliente; } set { llegadaCliente = value; }}   
        public double ProximaLlegadaCliente { get { return proximaLlegadaCliente; } set { proximaLlegadaCliente = value; }}
        public int AleatorioRecorrido { get { return aleatorioRecorrido; }    set { aleatorioRecorrido = value; }}    
        public int IDRecorrido { get { return idRecorrido; } set { idRecorrido = value; }}
        public string EventoSiguiente { get { return eventoSiguiente; } set { eventoSiguiente = value; } }
        public int CantClientesAtendidos { get { return contCantClientesAtendidos; } set { contCantClientesAtendidos = value; } }



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



        public double generarUniforme(double RND)
        {
            return Math.Round(RND * (3 - 1) + 1, 4);
        }
        


        //METODOS PARA LOS RECORRIDOS//
        public int generarRecorrido(int rnd) //para ver en donde cae el aleatorio
        {
            int idRecorrido=0;
            if (rnd >= 00 && rnd < 20) { idRecorrido = 1; } //Verduleria-Panaderia
            else if (rnd >= 20 && rnd < 50) { idRecorrido = 2; } //Verduleria-Carniceria-Gondola 
            else if (rnd >= 50 && rnd < 60) { idRecorrido = 3; } //Panaderia
            else if(rnd >= 60 && rnd < 80) { idRecorrido = 4; }//Carniceria-Panaderia-Gondola-Verduleria
                    else { idRecorrido = 5; } //Gondola
            return idRecorrido;
            
        }
        
        public string generarCadenaRecorrido(int r)
        {
            string cadenaRecorrido = "";
            if (r == 1) { cadenaRecorrido = "V-P"; }
            else if (r == 2) { cadenaRecorrido = "V-C-G"; }
            else if (r == 3) { cadenaRecorrido = "P"; }
            else if (r == 4) { cadenaRecorrido = "C-P-G-V"; }
            else { cadenaRecorrido = "G"; }
            return cadenaRecorrido;
        }

        public double generarAleatorio() 
        {
            Random RND = new Random();
            double rnd = Math.Round(RND.NextDouble(), 4);
            return rnd;

        }

        

        


}
}
