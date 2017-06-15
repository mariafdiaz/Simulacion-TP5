using System.Collections.Generic;

namespace Simulacion_TP5.Objetos
{
    public class Panaderia 
    {


        public string Estado { get; set; }
        public Queue<Cliente> cola;
        public Queue<Cliente> Cola { get => cola; set => cola = value; }
        public int finAtencion { get; set; }

        public Panaderia()
        {            
            Estado = "L";
            cola = new Queue<Cliente>();
        }

        public void atencion(Cliente c)
        {

        }

      /*  public int calcularFinAtencion(int inicioAtencion)
        {
            return inicioAtencion + 3;
        }
       */ 
    }
}
