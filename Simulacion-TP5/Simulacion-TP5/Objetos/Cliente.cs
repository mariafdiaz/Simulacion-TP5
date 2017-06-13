using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion_TP5.Objetos
{
    class Cliente
    {
            public int id { get; set; }
            public string Estado { get; set; }      // SA: siendo atendido - EA: esperando atencion
            public int horaLleg { get; set; }
            public int proxLleg { get; set; }
            public int id_recorrido { get; set; }
            public int cantArt { get; set; }


    }
}
