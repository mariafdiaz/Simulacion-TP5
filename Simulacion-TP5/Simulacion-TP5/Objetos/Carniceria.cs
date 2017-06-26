using System.Collections.Generic;


namespace Simulacion_TP5.Objetos
{
    public class Carniceria 
    {
        
        public string Estado { get; set; }
        public double tiempoAtencion { get; set; }
        public double finAtencion { get; set; }
        public Queue<Cliente> cola;
        public Queue<Cliente> Cola { get => cola; set => cola = value; }
        public Cliente clienteSirviendose;//Cliente actual que esta siendo atendido
       

        public Carniceria()
        {
            Estado = "L";
            Cola = new Queue<Cliente>();
            clienteSirviendose = null;
        }


    }
    
}

