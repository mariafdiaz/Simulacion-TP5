﻿using Simulacion_TP5.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion_TP5.Objetos
{
    public class Panaderia 
    {


        private Queue<Cliente> cola;
        private string estado { get; set; }
        public int finAtencion { get; set; }

        public Panaderia()
        {
            
            estado = "L";
            cola = new Queue<Cliente>();
        }

      /*  public int calcularFinAtencion(int inicioAtencion)
        {
            return inicioAtencion + 3;
        }
       */ 
    }
}
