using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion_TP5
{
    class Supermercado
    {
        private Random RND = new Random();
        //EL MOMENTO DE TIEMPO EN EL QUE OCURRE CADA EVENTO//
        private double reloj; //declaramos el reloj 

        public double Reloj  //constructor    
        {
            get { return reloj; }
            set { reloj = value; }
        }

        //NUMERO ALEATORIO PARA SABER LA PROXIMA LLEGADA//
        private double aleatorioLlegadaCliente; //es el rnd que se muestra que calcula el tiempo entre llegadas

        public double AleatorioLlegadaCliente //constructor
        {
            get { return aleatorioLlegadaCliente; }
            set { aleatorioLlegadaCliente = value; }

        }
        //TIEMPO HASTA QUE LLEGUE UN CLIENTE
        private double llegadaCliente;

        public double LlegadaCliente  //constructor    
        {
            get { return llegadaCliente; }
            set { llegadaCliente = value; }
        }
        //MOMENTO DE LA PROXIMA LLEGADA DE UN CLIENTE//
        private double proximaLlegadaCliente; //el tiempo en el que llegara el proximo pasajero,que se calcula con el metodo calcular tiempo proxima llegada, pasandole como tiempo actual el reloj de el evento ocurrido y sumandole la cantidad de minutos para proxima llegada de pasajero

        public double ProximaLlegadaCliente  //constructor    
        {
            get { return proximaLlegadaCliente; }
            set { proximaLlegadaCliente = value; }
        }


        private double aleatorioRecorrido; //es el rnd que se muestra que calcula el tiempo entre llegadas

        public double AleatorioRecorrido //constructor
        {
            get { return aleatorioRecorrido; }
            set { aleatorioRecorrido = value; }

        }
        private double idRecorrido; //el tiempo en el que llegara el proximo pasajero,que se calcula con el metodo calcular tiempo proxima llegada, pasandole como tiempo actual el reloj de el evento ocurrido y sumandole la cantidad de minutos para proxima llegada de pasajero

        public double IDRecorrido  //constructor    
        {
            get { return idRecorrido; }
            set { idRecorrido = value; }
        }

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

    }
}
