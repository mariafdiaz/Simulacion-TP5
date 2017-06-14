using System.Collections.Generic;

namespace Simulacion_TP5.Objetos
{
    public class Cliente
    {
            public int id { get; set; }
            public string Estado { get; set; }      // SA: siendo atendido - EA: esperando atencion
            public int horaLleg { get; set; }
            public int proxLleg { get; set; }
            public int id_recorrido { get; set; }
            public int cantArt { get; set; }
            public Queue <int> recorrido;
            public Queue <int> Recorrido { get => recorrido; set => recorrido = value; }
        public Cliente()
        {
            
            Recorrido = new Queue<int>();
        }
    }
}
