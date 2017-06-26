
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Simulacion_TP5.Objetos;
using System.Collections.Generic;

namespace Simulacion_TP5
{
    public partial class PantallaSimulacion : Form
    {
        //ATRIBUTOS//
        private DataTable dt;
        private Supermercado super = new Supermercado();
        private static Random RND = new Random();
        //Instaciación de clases
        private Verduleria verduleria;
        private Carniceria carniceria;
        private Panaderia panaderia;
        private Gondola gondola;
        private Caja3 caja3;
        private Caja2 caja2;
        private CajaR cajaR;
        private int contClientesEntrantes; // sirve para ir numerando los clientes
        


        //FUNCIONALIDAD//
        private void btn_generar_Click_1(object sender, EventArgs e)
        {
            //instanciacion de clases

             
        //Instaciación de clases
        verduleria = new Verduleria();
       carniceria = new Carniceria();
       panaderia = new Panaderia();
        gondola = new Gondola();
        caja3 = new Caja3();
        caja2 = new Caja2();
        cajaR = new CajaR();
        dt = new DataTable();
            contClientesEntrantes = 0;
            if (validar_campos())
            {
                inicializarColumnas(); //Todas las columnas menos los clientes           
                iniciarPrimeraFila();//iniciarPrimeraFila
               
                //agregarColumnaNuevoCliente(); //empezar a funcionar

                while (super.Reloj < horas)
                {

                    switch (super.EventoSiguiente) //Que tipo de Evento es?
                    {
                        case "LLC"://llegada Cliente 


                            DataRow dr1 = dt.NewRow();

                            dr1["Evento"] = "Llegada Cliente";
                            super.Reloj = super.ProximaLlegadaCliente;
                            super.AleatorioLlegadaCliente = Math.Round(RND.NextDouble(), 4);
                            super.LlegadaCliente = super.generarPoisson(0.5, super.AleatorioLlegadaCliente);
                            
                            super.ProximaLlegadaCliente = super.Reloj + super.LlegadaCliente;
                            dr1["Reloj"] = super.Reloj;
                            dr1["*Llegada cliente* RND"] = super.AleatorioLlegadaCliente;
                            dr1["TiempoLleg"] = super.LlegadaCliente;
                            dr1["ProxLleg"] = super.ProximaLlegadaCliente;

                            //Generar recorrido
                            super.AleatorioRecorrido = RND.Next(100);
                            super.IDRecorrido = super.generarRecorrido(super.AleatorioRecorrido);
                            string cadena = super.generarCadenaRecorrido(super.IDRecorrido);
                            dr1["*Recorrido* RND"] = super.AleatorioRecorrido;
                            dr1["Recorrido"] = cadena;
                            dr1["ContClientes Atendidos"] = super.CantClientesAtendidos;

                           


                            switch (super.IDRecorrido)
                            {
                                case 1://Verduleria-Panaderia
                                    Cliente nuevo1 = new Cliente();
                                    contClientesEntrantes = contClientesEntrantes + 1;
                                    int i1 = contClientesEntrantes;
                                    nuevo1.id = i1;
                                    nuevo1.Recorrido.Enqueue("V"); nuevo1.Recorrido.Enqueue("P"); nuevo1.Recorrido.Enqueue("C");
                                    
                                    if (verduleria.Estado == "L")
                                    {

                                        nuevo1.Estado = "SAV";
                                        nuevo1.cantArt = 1;

                                        verduleria.Estado = "Oc";
                                       
                                        double rndV = Math.Round(RND.NextDouble(), 4);
                                        verduleria.tiempoAtencion = super.generarPoisson(2, rndV);
                                        verduleria.finAtencion = verduleria.tiempoAtencion + super.Reloj;

                                        dr1["*FinAtencion Verduleria* RND"] = rndV;
                                        dr1["Tiempo AtencionV"] = verduleria.tiempoAtencion;
                                        dr1["FinTiempo AtencionV"] = verduleria.finAtencion;

                                        verduleria.clienteSirviendose = nuevo1;

                                    }
                                    else {

                                        nuevo1.Estado = "EAV";
                                        verduleria.Cola.Enqueue(nuevo1);

                                    }



                                    //Clientes

                                    dt.Columns.Add("*C " + i1 + "* Estado", typeof(string));
                                    dt.Columns.Add("*C " + i1 + "* Prox seccion", typeof(string));
                                    dt.Columns.Add("Cont CantArticulos C " + i1, typeof(Int32));

                                    dr1["*C " + i1 + "* Estado"] = nuevo1.Estado;
                                    dr1["*C " + i1 + "* Prox seccion"] = nuevo1.Recorrido.Dequeue();
                                    dr1["Cont CantArticulos C " + i1] = nuevo1.cantArt;


                                    break;

                                case 2: //Verduleria-Carniceria-Gondola 
                                    Cliente nuevo2 = new Cliente();
                                    contClientesEntrantes = contClientesEntrantes + 1;
                                    int i2 = contClientesEntrantes;
                                    nuevo2.id = i2;
                                    nuevo2.Recorrido.Enqueue("V"); nuevo2.Recorrido.Enqueue("Ca"); nuevo2.Recorrido.Enqueue("G"); nuevo2.Recorrido.Enqueue("C");
                                    

                                    if (verduleria.Estado == "L")
                                    {
                                        nuevo2.Estado = "SAV";
                                        nuevo2.cantArt = 1;

                                        verduleria.Estado = "Oc";

                                        double rndV = Math.Round(RND.NextDouble(), 4);
                                        verduleria.tiempoAtencion = super.generarPoisson(2, rndV); 
                                        verduleria.finAtencion = verduleria.tiempoAtencion + super.Reloj;
                                        dr1["*FinAtencion Verduleria* RND"] = rndV;
                                        dr1["Tiempo AtencionV"] = verduleria.tiempoAtencion;
                                        dr1["FinTiempo AtencionV"] = verduleria.finAtencion;
                                        verduleria.clienteSirviendose = nuevo2;
                                    }
                                    else {

                                        nuevo2.Estado = "EAV";
                                        verduleria.Cola.Enqueue(nuevo2);

                                    }



                                    //Clientes

                                    dt.Columns.Add("*C " + i2 + "* Estado", typeof(string));
                                    dt.Columns.Add("*C " + i2 + "* Prox seccion", typeof(string));
                                    dt.Columns.Add("Cont CantArticulos C " + i2, typeof(Int32));

                                    dr1["*C " + i2 + "* Estado"] = nuevo2.Estado;
                                    dr1["*C " + i2 + "* Prox seccion"] = nuevo2.Recorrido.Dequeue();
                                    dr1["Cont CantArticulos C " + i2] = nuevo2.cantArt;

                                    break;
                                case 3: //Panaderia
                                    Cliente nuevo3 = new Cliente();
                                    contClientesEntrantes = contClientesEntrantes + 1;
                                    int i3 = contClientesEntrantes;
                                    nuevo3.id = i3;
                                    nuevo3.Recorrido.Enqueue("P"); nuevo3.Recorrido.Enqueue("C");
                                    

                                    if (panaderia.Estado == "L")
                                    {
                                        nuevo3.Estado = "SAP";
                                        nuevo3.cantArt = 1;

                                        panaderia.Estado = "Oc";
                                        panaderia.finAtencion = 3 + super.Reloj;
                                        dr1["*FinAtencion Panadería*"] = panaderia.finAtencion;

                                        panaderia.clienteSirviendose = nuevo3;
                                    }
                                    else
                                    {

                                        nuevo3.Estado = "EAP";
                                        panaderia.Cola.Enqueue(nuevo3);

                                    }


                                    //Clientes

                                    dt.Columns.Add("*C " + i3 + "* Estado", typeof(string));
                                    dt.Columns.Add("*C " + i3 + "* Prox seccion", typeof(string));
                                    dt.Columns.Add("Cont CantArticulos C " + i3, typeof(Int32));

                                    dr1["*C " + i3 + "* Estado"] = nuevo3.Estado;
                                    dr1["*C " + i3 + "* Prox seccion"] = nuevo3.Recorrido.Dequeue();
                                    dr1["Cont CantArticulos C " + i3] = nuevo3.cantArt;


                                    break;

                                case 4://Carniceria-Panaderia-Gondola-Verduleria
                                    Cliente nuevo4 = new Cliente();
                                    contClientesEntrantes = contClientesEntrantes + 1;
                                    int i4 = contClientesEntrantes;
                                    nuevo4.id = i4;
                                    nuevo4.Recorrido.Enqueue("Ca"); nuevo4.Recorrido.Enqueue("P"); nuevo4.Recorrido.Enqueue("G"); nuevo4.Recorrido.Enqueue("V"); nuevo4.Recorrido.Enqueue("C");//C de que va a caja
                                    


                                    if (carniceria.Estado == "L")
                                    {
                                        nuevo4.Estado = "SAC";
                                        nuevo4.cantArt = 1;

                                        carniceria.Estado = "Oc";
                                        double rndC = Math.Round(RND.NextDouble(), 4);
                                        carniceria.tiempoAtencion = Math.Round(super.generarUniforme(rndC), 2);
                                        carniceria.finAtencion = carniceria.tiempoAtencion + super.Reloj;

                                        dr1["*FinAtencion Carniceria* RND"] = rndC;
                                        dr1["Tiempo AtencionC"] = carniceria.tiempoAtencion;
                                        dr1["FinTiempo AtencionC"] = carniceria.finAtencion;

                                        carniceria.clienteSirviendose = nuevo4;//Asigno cliente nuevo a sirviendose
                                    }
                                    else
                                    {

                                        nuevo4.Estado = "EAC";
                                        carniceria.Cola.Enqueue(nuevo4);

                                    }



                                    //Clientes

                                    dt.Columns.Add("*C " + i4 + "* Estado", typeof(string));
                                    dt.Columns.Add("*C " + i4 + "* Prox seccion", typeof(string));
                                    dt.Columns.Add("Cont CantArticulos C " + i4, typeof(Int32));

                                    dr1["*C " + i4 + "* Estado"] = nuevo4.Estado;
                                    dr1["*C " + i4 + "* Prox seccion"] = nuevo4.Recorrido.Dequeue();
                                    dr1["Cont CantArticulos C " + i4] = nuevo4.cantArt;
                                    break;
                                case 5://Gondola
                                    Cliente nuevo5 = new Cliente();
                                    contClientesEntrantes = contClientesEntrantes + 1;
                                    int i5 = contClientesEntrantes;
                                    nuevo5.id = i5;
                                    nuevo5.Recorrido.Enqueue("G"); nuevo5.Recorrido.Enqueue("C");
                                    


                                    if (gondola.Estado == "L")
                                    {
                                        nuevo5.Estado = "SAG";


                                        gondola.Estado = "Oc";
                                        double rndG = RND.Next(100);
                                        nuevo5.cantArt = gondola.generarCantArticulos(rndG);
                                        gondola.finAtencion = nuevo5.cantArt + super.Reloj;
                                        dr1["*FinAtencion Gondola* RND"] = rndG;
                                        dr1["Cant Articulos Gondola"] = nuevo5.cantArt;
                                        dr1["FinTiempo Atencion Gondola"] = gondola.finAtencion;




                                        gondola.clienteSirviendose = nuevo5;//Asigno cliente nuevo a sirviendose

                                    }
                                    else
                                    {

                                        nuevo5.Estado = "EAG";
                                        gondola.Cola.Enqueue(nuevo5);

                                    }



                                    //Clientes

                                    dt.Columns.Add("*C " + i5 + "* Estado", typeof(string));
                                    dt.Columns.Add("*C " + i5 + "* Prox seccion", typeof(string));
                                    dt.Columns.Add("Cont CantArticulos C " + i5, typeof(Int32));

                                    dr1["*C " + i5 + "* Estado"] = nuevo5.Estado;
                                    dr1["*C " + i5 + "* Prox seccion"] = nuevo5.Recorrido.Dequeue();
                                    dr1["Cont CantArticulos C " + i5] = nuevo5.cantArt;
                                    break;
                                default:
                                    MessageBox.Show("Hubo un error en el switch de recorrido");
                                    break;
                            }


                            //Agregar en alguna Cola

                            //verduleria.Cola.Enqueue(nuevo);


                            //Escribo en la Fila los tiempos que no se cambiaron
                            //Los fin atencion de los eventos se setean al principio con -1 , Si nunca se escribió un fin Atencion de un server no debe poner nada en Fin tiempo de la fila siguiente
                            if (carniceria.finAtencion != -1) { dr1["FinTiempo AtencionC"] = carniceria.finAtencion; }
                            if (verduleria.finAtencion != -1) { dr1["FinTiempo AtencionV"] = verduleria.finAtencion; }
                            if (panaderia.finAtencion != -1) { dr1["*FinAtencion Panadería*"] = panaderia.finAtencion; }
                            if (gondola.finAtencion != -1) { dr1["FinTiempo Atencion Gondola"] = gondola.finAtencion; }
                            if (cajaR.finAtencion != -1) { dr1["FinTiempo AtencíonCR"] = cajaR.finAtencion; }
                            if (caja2.finAtencion != -1) { dr1["FinTiempo AtencionC2"] = caja2.finAtencion; }
                            if (caja3.finAtencion != -1) { dr1["\nFinTiempoAC3"] = caja3.finAtencion; }
                            //Servers

                            dr1["*Verduleria* Estado"] = verduleria.Estado;
                            dr1["*Carniceria* Estado"] = carniceria.Estado;
                            dr1["*Panaderia* Estado"] = panaderia.Estado;
                            dr1["*Gondola* Estado"] = gondola.Estado;
                            dr1["*CajaRapida* Estado"] = cajaR.Estado;
                            dr1["*Caja2* Estado"] = caja2.Estado;
                            dr1["*Caja3* Estado"] = caja3.Estado;

                            dr1["\nColaV"] = verduleria.Cola.Count;
                            dr1["\nColaC"] = carniceria.Cola.Count;
                            dr1["\nColaP"] = panaderia.Cola.Count;
                            dr1["\nColaG"] = gondola.Cola.Count;
                            dr1["\nColaCR"] = cajaR.Cola.Count;
                            dr1["\nColaC2"] = caja2.Cola.Count;
                            dr1["\nColaC3"] = caja3.Cola.Count;



                            dt.Rows.Add(dr1);
                            break;


                        case "FAP"://Fin Atencion Panaderia 
                            {
                                //PASO UNO
                                DataRow dr2 = dt.NewRow();
                                dr2["Evento"] = "Fin atenc Panaderia";
                                dr2["Reloj"] = Math.Round(super.Reloj,4);
                                int id = panaderia.clienteSirviendose.id;
                                string proxRecorridoClienteSirviendose = panaderia.clienteSirviendose.Recorrido.Dequeue();

                                
                                //PASO DOS : Mandar Cliente Sirviendose a seguir el circuito
                                switch (proxRecorridoClienteSirviendose)
                                {
                                    case "C"://CASO CAJA VER CUANTOS PRODUCTOS TRAE
                                        {
                                            if (panaderia.clienteSirviendose.cantArt > 3)
                                            {
                                                bool queCaja = false;
                                                if (RND.Next(100) >= 50) { queCaja = true; }//DEFINIR PARA QUE CAJA VAN A IR (CAJA 2 o CAJA 3)
                                                //SI mayor que 50 va a caja 2
                                                if (caja2.Estado == "L" && queCaja == true)
                                                {

                                                    panaderia.clienteSirviendose.Estado = "SAC2";
                                                    caja2.Estado = "Oc";
                                                    //Generar Tiempo para Caja2
                                                    caja2.clienteSirviendose = panaderia.clienteSirviendose;
                                                    caja2.finAtencion = caja2.clienteSirviendose.cantArt * 0.333 + 1 + super.Reloj;
                                                    dr2["*Atencion  Caja2* CantArticulos"] = caja2.clienteSirviendose.cantArt;
                                                    dr2["FinTiempo AtencionC2"] = caja2.finAtencion;

                                                    //PARTE DEL CLIENTE
                                                    dr2["*C " + id + "* Estado"] = caja2.clienteSirviendose.Estado;
                                                    dr2["*C " + id + "* Prox seccion"] = proxRecorridoClienteSirviendose;
                                                    dr2["Cont CantArticulos C " + id] = caja2.clienteSirviendose.cantArt;

                                                }
                                                else if (caja2.Estado == "Oc" && queCaja == true)
                                                {

                                                    panaderia.clienteSirviendose.Estado = "EAC2";
                                                    caja2.Cola.Enqueue(panaderia.clienteSirviendose);
                                                   
                                                    //PARTE DEL CLIENTE
                                                    dr2["*C " + id + "* Estado"] = caja2.clienteSirviendose.Estado;
                                                    dr2["*C " + id + "* Prox seccion"] = proxRecorridoClienteSirviendose;
                                                    dr2["Cont CantArticulos C " + id] = caja2.clienteSirviendose.cantArt;
                                                }
                                                else if (caja3.Estado == "L" && queCaja == false)
                                                {
                                                    panaderia.clienteSirviendose.Estado = "SAC3";
                                                    caja3.Estado = "Oc";
                                                    //Generar Tiempo para Caja3
                                                    caja3.clienteSirviendose = panaderia.clienteSirviendose;
                                                    caja3.finAtencion = caja3.clienteSirviendose.cantArt * 0.333 + 1 + super.Reloj;
                                                    dr2["*AtencionCaja3* CantArticulos"] = caja3.clienteSirviendose.cantArt;
                                                    dr2["\nFinTiempoAC3"] = caja3.finAtencion;
                                                   
                                                    panaderia.clienteSirviendose = null;

                                                    //PARTE DEL CLIENTE
                                                    dr2["*C " + id + "* Estado"] = caja3.clienteSirviendose.Estado;
                                                    dr2["*C " + id + "* Prox seccion"] = proxRecorridoClienteSirviendose;
                                                    dr2["Cont CantArticulos C " + id] = caja3.clienteSirviendose.cantArt;


                                                }
                                                else if (caja3.Estado == "Oc" && queCaja == false)
                                                {
                                                    panaderia.clienteSirviendose.Estado = "EAC3";
                                                    caja3.Cola.Enqueue(panaderia.clienteSirviendose);
                                                    
                                                    //PARTE DEL CLIENTE
                                                    dr2["*C " + id + "* Estado"] = caja3.clienteSirviendose.Estado;
                                                    dr2["*C " + id + "* Prox seccion"] = proxRecorridoClienteSirviendose;
                                                    dr2["Cont CantArticulos C " + id] = caja3.clienteSirviendose.cantArt;

                                                }

                                            }
                                            else//CAJA RAPIDA 
                                            {
                                                if (cajaR.Estado == "L")
                                                {

                                                    panaderia.clienteSirviendose.Estado = "SACR";
                                                    cajaR.Estado = "Oc";
                                                    //Generar Tiempo para CajaR
                                                    cajaR.clienteSirviendose = panaderia.clienteSirviendose;
                                                    cajaR.finAtencion = cajaR.clienteSirviendose.cantArt * 0.333 + 1 + super.Reloj;
                                                    dr2["*Atencion CajaRapida* CantArticulos"] = cajaR.clienteSirviendose.cantArt;
                                                    dr2["FinTiempo AtencíonCR"] = cajaR.finAtencion;



                                                    //PARTE DEL CLIENTE
                                                    dr2["*C " + id + "* Estado"] = cajaR.clienteSirviendose.Estado;
                                                    dr2["*C " + id + "* Prox seccion"] = proxRecorridoClienteSirviendose;
                                                    dr2["Cont CantArticulos C " + id] = cajaR.clienteSirviendose.cantArt;

                                                }
                                                else if (cajaR.Estado == "Oc")
                                                {

                                                    panaderia.clienteSirviendose.Estado = "EACR";
                                                    cajaR.Cola.Enqueue(panaderia.clienteSirviendose);
                                                   
                                                    
                                                    //PARTE DEL CLIENTE
                                                    dr2["*C " + id + "* Estado"] = cajaR.clienteSirviendose.Estado;
                                                    dr2["*C " + id + "* Prox seccion"] = proxRecorridoClienteSirviendose;
                                                    dr2["Cont CantArticulos C " + id] = cajaR.clienteSirviendose.cantArt;
                                                }
                                            }


                                            break;
                                        }
                                    case "G":
                                        {
                                            if (gondola.Estado == "L")
                                            {

                                                panaderia.clienteSirviendose.Estado = "SAG";
                                                panaderia.Estado = "Oc";
                                                double rndG = RND.Next(100);
                                                int articulosQueSacoDeGondola = gondola.generarCantArticulos(rndG);
                                                dr2["*FinAtencion Gondola* RND"] = rndG;
                                                
                                                dr2["Cant Articulos Gondola"] = articulosQueSacoDeGondola;
                                                //Generar Tiempo para Panaderia
                                                gondola.finAtencion = articulosQueSacoDeGondola + super.Reloj;
                                                dr2["FinTiempo Atencion Gondola"] = gondola.finAtencion;
                                                
                                                panaderia.clienteSirviendose.cantArt =  articulosQueSacoDeGondola + panaderia.clienteSirviendose.cantArt;
                                                gondola.clienteSirviendose = panaderia.clienteSirviendose;
                                            }
                                            else
                                            {

                                                panaderia.clienteSirviendose.Estado = "EAP";
                                                gondola.Cola.Enqueue(panaderia.clienteSirviendose);

                                            }

                                           




                                            //PARTE DEL CLIENTE
                                            dr2["*C " + id + "* Estado"] = panaderia.clienteSirviendose.Estado;
                                            dr2["*C " + id + "* Prox seccion"] = proxRecorridoClienteSirviendose;
                                            dr2["Cont CantArticulos C " + id] = panaderia.clienteSirviendose.cantArt;
                                            


                                            break;
                                        }
                                    default:
                                        break;
                                }


                                // PARTE TRES QUE HACER SI HAY GENTE ESPERANDO O NO
                                if (panaderia.Cola.Count != 0)
                                {
                                    panaderia.clienteSirviendose = panaderia.Cola.Dequeue();
                                    panaderia.clienteSirviendose.Estado = "SAP";
                                    panaderia.clienteSirviendose.cantArt = 1 + panaderia.clienteSirviendose.cantArt;

                                    panaderia.Estado = "Oc";
                                    //Generar Tiempo para Carniceria

                                    panaderia.finAtencion = 3 + super.Reloj;



                                    dr2["FinTiempo AtencionV"] = panaderia.finAtencion;



                                }
                                else
                                {
                                    panaderia.Estado = "L";
                                    panaderia.clienteSirviendose = null;//Pongo que no tiene mas sirviendose
                                    panaderia.finAtencion = -1;//pongo -1 ya que no va a haber mas clientes q se tienen q servir
                                }
                                dr2["ProxLleg"] = super.ProximaLlegadaCliente;
                                if (carniceria.finAtencion != -1) { dr2["FinTiempo AtencionC"] = carniceria.finAtencion; }
                                if (verduleria.finAtencion != -1) { dr2["FinTiempo AtencionV"] = verduleria.finAtencion; }
                                if (panaderia.finAtencion != -1) { dr2["*FinAtencion Panadería*"] = panaderia.finAtencion; }
                                if (gondola.finAtencion != -1) { dr2["FinTiempo Atencion Gondola"] = gondola.finAtencion; }
                                if (cajaR.finAtencion != -1) { dr2["FinTiempo AtencíonCR"] = cajaR.finAtencion; }
                                if (caja2.finAtencion != -1) { dr2["FinTiempo AtencionC2"] = caja2.finAtencion; }
                                if (caja3.finAtencion != -1) { dr2["\nFinTiempoAC3"] = caja3.finAtencion; }
                                dr2["ContClientes Atendidos"] = super.CantClientesAtendidos;
                                dr2["*Verduleria* Estado"] = verduleria.Estado;
                                dr2["*Carniceria* Estado"] = carniceria.Estado;
                                dr2["*Panaderia* Estado"] = panaderia.Estado;
                                dr2["*Gondola* Estado"] = gondola.Estado;
                                dr2["*CajaRapida* Estado"] = cajaR.Estado;
                                dr2["*Caja2* Estado"] = caja2.Estado;
                                dr2["*Caja3* Estado"] = caja3.Estado;


                                dr2["\nColaV"] = verduleria.Cola.Count;
                                dr2["\nColaC"] = carniceria.Cola.Count;
                                dr2["\nColaP"] = panaderia.Cola.Count;
                                dr2["\nColaG"] = gondola.Cola.Count;
                                dr2["\nColaCR"] = cajaR.Cola.Count;
                                dr2["\nColaC2"] = caja2.Cola.Count;
                                dr2["\nColaC3"] = caja3.Cola.Count;




                                dt.Rows.Add(dr2);
                                break;

                            }
                        case "FAC"://Fin Atencion Carniceria
                            {
                                DataRow dr3 = dt.NewRow();
                                dr3["Reloj"] = Math.Round(super.Reloj, 4);
                                dr3["Evento"] = "Fin atenc Carniceria";
                                int id = carniceria.clienteSirviendose.id;
                                string proxRecorridoClienteSirviendose = carniceria.clienteSirviendose.Recorrido.Dequeue();

                                //PASO DOS : Mandar Cliente Sirviendose a seguir el circuito
                                switch (proxRecorridoClienteSirviendose) {
                                    case "P"://PANA
                                        {
                                            if (panaderia.Estado == "L")
                                            {

                                                carniceria.clienteSirviendose.Estado = "SAP";
                                                carniceria.clienteSirviendose.cantArt = 1 + carniceria.clienteSirviendose.cantArt;

                                                panaderia.Estado = "Oc";
                                                //Generar Tiempo para Panaderia

                                                panaderia.finAtencion = Math.Round(3 + super.Reloj,4);


                                                dr3["*FinAtencion Panadería*"] = panaderia.finAtencion;
                                                panaderia.clienteSirviendose = carniceria.clienteSirviendose;
                                            }
                                            else
                                            {

                                                carniceria.clienteSirviendose.Estado = "EAP";
                                                panaderia.Cola.Enqueue(carniceria.clienteSirviendose);

                                            }

                                            




                                            //PARTE DEL CLIENTE
                                            dr3["*C " + id + "* Estado"] = panaderia.clienteSirviendose.Estado;
                                            dr3["*C " + id + "* Prox seccion"] = proxRecorridoClienteSirviendose;
                                            dr3["Cont CantArticulos C " + id] = panaderia.clienteSirviendose.cantArt;


                                            break;
                                        }
                                    case "G":
                                        {
                                            if (gondola.Estado == "L")
                                            {

                                                carniceria.clienteSirviendose.Estado = "SAG";
                                                

                                                gondola.Estado = "Oc";
                                                //Generar Tiempo para GONDOLA
                                                
                                                double rndG = RND.Next(100);
                                                int articulosQueSacoDeGondola = gondola.generarCantArticulos(rndG);
                                                dr3["*FinAtencion Gondola* RND"] = rndG;

                                                dr3["Cant Articulos Gondola"] = articulosQueSacoDeGondola;
                                                
                                                gondola.finAtencion = Math.Round(articulosQueSacoDeGondola + super.Reloj,4);
                                                dr3["FinTiempo Atencion Gondola"] = gondola.finAtencion;

                                                carniceria.clienteSirviendose.cantArt = articulosQueSacoDeGondola + carniceria.clienteSirviendose.cantArt;
                                                gondola.clienteSirviendose = carniceria.clienteSirviendose;
                                            }
                                            else
                                            {

                                                carniceria.clienteSirviendose.Estado = "EAG";
                                                gondola.Cola.Enqueue(carniceria.clienteSirviendose);

                                            }

                                            




                                            //PARTE DEL CLIENTE
                                            dr3["*C " + id + "* Estado"] = carniceria.clienteSirviendose.Estado;
                                            dr3["*C " + id + "* Prox seccion"] = proxRecorridoClienteSirviendose;
                                            dr3["Cont CantArticulos C " + id] = carniceria.clienteSirviendose.cantArt;


                                            break;
                                        }
                                    default:
                                        break;
                                } ///FIN SWITCH


                                // PARTE TRES QUE HACER SI HAY GENTE ESPERANDO O NO
                                if (carniceria.Cola.Count != 0)
                                {
                                    carniceria.clienteSirviendose = carniceria.Cola.Dequeue();
                                    carniceria.clienteSirviendose.Estado = "SAC";
                                    carniceria.clienteSirviendose.cantArt = 1 + carniceria.clienteSirviendose.cantArt;

                                    carniceria.Estado = "Oc";
                                    //Generar Tiempo para Carniceria
                                    double rndC = Math.Round(RND.NextDouble(),4);
                                    carniceria.tiempoAtencion = Math.Round(super.generarUniforme(rndC),2);///SADNKASJDKJASNDJAS CAMBIAR UNIFORME
                                    carniceria.finAtencion = Math.Round(carniceria.tiempoAtencion + super.Reloj,4);

                                    dr3["*FinAtencion Carniceria* RND"] = rndC;
                                    dr3["Tiempo AtencionC"] = carniceria.tiempoAtencion;
                                    dr3["FinTiempo AtencionC"] = carniceria.finAtencion;



                                }
                                else
                                {
                                    carniceria.Estado = "L";
                                    carniceria.clienteSirviendose = null;//Pongo que no tiene mas sirviendose
                                    carniceria.finAtencion = -1;//pongo -1 ya que no va a haber mas clientes q se tienen q servir
                                }
                                dr3["ProxLleg"] = super.ProximaLlegadaCliente;
                                if (carniceria.finAtencion != -1) { dr3["FinTiempo AtencionC"] = carniceria.finAtencion; }
                                if (verduleria.finAtencion != -1) { dr3["FinTiempo AtencionV"] = verduleria.finAtencion; }
                                if (panaderia.finAtencion != -1) { dr3["*FinAtencion Panadería*"] = panaderia.finAtencion; }
                                if (gondola.finAtencion != -1) { dr3["FinTiempo Atencion Gondola"] = gondola.finAtencion; }
                                if (cajaR.finAtencion != -1) { dr3["FinTiempo AtencíonCR"] = cajaR.finAtencion; }
                                if (caja2.finAtencion != -1) { dr3["FinTiempo AtencionC2"] = caja2.finAtencion; }
                                if (caja3.finAtencion != -1) { dr3["\nFinTiempoAC3"] = caja3.finAtencion; }
                                dr3["ContClientes Atendidos"] = super.CantClientesAtendidos;
                                dr3["*Verduleria* Estado"] = verduleria.Estado;
                                dr3["*Carniceria* Estado"] = carniceria.Estado;
                                dr3["*Panaderia* Estado"] = panaderia.Estado;
                                dr3["*Gondola* Estado"] = gondola.Estado;
                                dr3["*CajaRapida* Estado"] = cajaR.Estado;
                                dr3["*Caja2* Estado"] = caja2.Estado;
                                dr3["*Caja3* Estado"] = caja3.Estado;


                                dr3["\nColaV"] = verduleria.Cola.Count;
                                dr3["\nColaC"] = carniceria.Cola.Count;
                                dr3["\nColaP"] = panaderia.Cola.Count;
                                dr3["\nColaG"] = gondola.Cola.Count;
                                dr3["\nColaCR"] = cajaR.Cola.Count;
                                dr3["\nColaC2"] = caja2.Cola.Count;
                                dr3["\nColaC3"] = caja3.Cola.Count;





                                dt.Rows.Add(dr3);
                                break;
                            }


                        case "FAV"://Fin Atencion Verduleria 
                            {
                                //PASO UNO : ANOTAR TODOS LOS DATOS ESENCIALES EN ROW
                                DataRow dr5 = dt.NewRow();
                                dr5["Evento"] = "Fin atenc Verduleria";
                                
                                dr5["Reloj"] = Math.Round(super.Reloj, 4);
                                int id = verduleria.clienteSirviendose.id;
                                string proxRecorridoClienteSirviendose = verduleria.clienteSirviendose.Recorrido.Dequeue();

                                //PASO DOS : Mandar Cliente Sirviendose a seguir el circuito
                                switch (proxRecorridoClienteSirviendose)
                                {
                                    case "Ca":
                                        {

                                            if (carniceria.Estado == "L")
                                            {

                                                verduleria.clienteSirviendose.Estado = "SAC";
                                                verduleria.clienteSirviendose.cantArt = 1 + verduleria.clienteSirviendose.cantArt;

                                                carniceria.Estado = "Oc";
                                                //Generar Tiempo para Carniceria
                                                double rndC = Math.Round(RND.NextDouble(),4);
                                                carniceria.tiempoAtencion = Math.Round(super.generarUniforme(rndC),2);
                                                carniceria.finAtencion = Math.Round(carniceria.tiempoAtencion + super.Reloj,2);

                                                dr5["*FinAtencion Carniceria* RND"] = rndC;
                                                dr5["Tiempo AtencionC"] = carniceria.tiempoAtencion;
                                                dr5["FinTiempo AtencionC"] = carniceria.finAtencion;
                                                carniceria.clienteSirviendose = verduleria.clienteSirviendose;
                                            }
                                            else
                                            {

                                                verduleria.clienteSirviendose.Estado = "EAC";
                                                carniceria.Cola.Enqueue(verduleria.clienteSirviendose);

                                            }

                                            




                                            //PARTE DEL CLIENTE
                                            dr5["*C " + id + "* Estado"] = carniceria.clienteSirviendose.Estado;
                                            dr5["*C " + id + "* Prox seccion"] = proxRecorridoClienteSirviendose;
                                            dr5["Cont CantArticulos C " + id] = carniceria.clienteSirviendose.cantArt;


                                            break;
                                        }
                                    case "C"://CASO CAJA VER CUANTOS PRODUCTOS TRAE
                                        {
                                            if (verduleria.clienteSirviendose.cantArt > 3)
                                            {
                                                bool queCaja = false;
                                                if (RND.Next(100) >= 50) { queCaja = true; }//DEFINIR PARA QUE CAJA VAN A IR (CAJA 2 o CAJA 3)
                                                //SI mayor que 50 va a caja 2
                                                if (caja2.Estado == "L" && queCaja == true)
                                                {

                                                    verduleria.clienteSirviendose.Estado = "SAC2";
                                                    caja2.Estado = "Oc";
                                                    //Generar Tiempo para Caja2
                                                    caja2.clienteSirviendose = verduleria.clienteSirviendose;
                                                    caja2.finAtencion = Math.Round(caja2.clienteSirviendose.cantArt * 0.333 + 1 + super.Reloj, 4);
                                                    dr5["*Atencion  Caja2* CantArticulos"] = caja2.clienteSirviendose.cantArt;
                                                    dr5["FinTiempo AtencionC2"] = caja2.finAtencion;

                                                    //PARTE DEL CLIENTE
                                                    dr5["*C " + id + "* Estado"] = verduleria.clienteSirviendose.Estado;
                                                    dr5["*C " + id + "* Prox seccion"] = proxRecorridoClienteSirviendose;
                                                    dr5["Cont CantArticulos C " + id] = caja2.clienteSirviendose.cantArt;

                                                }
                                                else if (caja2.Estado == "Oc" && queCaja == true)
                                                {

                                                    verduleria.clienteSirviendose.Estado = "EAC2";
                                                    caja2.Cola.Enqueue(verduleria.clienteSirviendose);

                                                    //PARTE DEL CLIENTE
                                                    dr5["*C " + id + "* Estado"] = verduleria.clienteSirviendose.Estado;
                                                    dr5["*C " + id + "* Prox seccion"] = proxRecorridoClienteSirviendose;
                                                    dr5["Cont CantArticulos C " + id] = verduleria.clienteSirviendose.cantArt;
                                                }
                                                else if (caja3.Estado == "L" && queCaja == false)
                                                {
                                                    verduleria.clienteSirviendose.Estado = "SAC3";
                                                    caja3.Estado = "Oc";
                                                    //Generar Tiempo para Caja3
                                                    caja3.clienteSirviendose = verduleria.clienteSirviendose;
                                                    caja3.finAtencion = Math.Round(caja3.clienteSirviendose.cantArt * 0.333 + 1 + super.Reloj, 4);
                                                    dr5["*AtencionCaja3* CantArticulos"] = caja3.clienteSirviendose.cantArt;
                                                    dr5["\nFinTiempoAC3"] =caja3.finAtencion;

                                                    //PARTE DEL CLIENTE
                                                    dr5["*C " + id + "* Estado"] = caja3.clienteSirviendose.Estado;
                                                    dr5["*C " + id + "* Prox seccion"] = proxRecorridoClienteSirviendose;
                                                    dr5["Cont CantArticulos C " + id] = caja3.clienteSirviendose.cantArt;


                                                }
                                                else if (caja3.Estado == "Oc" && queCaja == false)
                                                {
                                                    verduleria.clienteSirviendose.Estado = "EAC3";
                                                    caja3.Cola.Enqueue(verduleria.clienteSirviendose);

                                                    //PARTE DEL CLIENTE
                                                    dr5["*C " + id + "* Estado"] = verduleria.clienteSirviendose.Estado;
                                                    dr5["*C " + id + "* Prox seccion"] = proxRecorridoClienteSirviendose;
                                                    dr5["Cont CantArticulos C " + id] = verduleria.clienteSirviendose.cantArt;

                                                }

                                            }
                                            else//CAJA RAPIDA 
                                            {
                                                if (cajaR.Estado == "L")
                                                {

                                                    verduleria.clienteSirviendose.Estado = "SACR";
                                                    cajaR.Estado = "Oc";
                                                    //Generar Tiempo para CajaR
                                                    cajaR.clienteSirviendose = verduleria.clienteSirviendose;
                                                    cajaR.finAtencion = Math.Round(cajaR.clienteSirviendose.cantArt * 0.333 + 1 + super.Reloj, 4);
                                                    dr5["*Atencion CajaRapida* CantArticulos"] = cajaR.clienteSirviendose.cantArt;
                                                    dr5["FinTiempo AtencíonCR"] = cajaR.finAtencion;



                                                    //PARTE DEL CLIENTE
                                                    dr5["*C " + id + "* Estado"] = cajaR.clienteSirviendose.Estado;
                                                    dr5["*C " + id + "* Prox seccion"] = proxRecorridoClienteSirviendose;
                                                    dr5["Cont CantArticulos C " + id] = cajaR.clienteSirviendose.cantArt;

                                                }
                                                else if (cajaR.Estado == "Oc")
                                                {

                                                    verduleria.clienteSirviendose.Estado = "EACR";
                                                    cajaR.Cola.Enqueue(verduleria.clienteSirviendose);

                                                    //PARTE DEL CLIENTE
                                                    dr5["*C " + id + "* Estado"] = verduleria.clienteSirviendose.Estado;
                                                    dr5["*C " + id + "* Prox seccion"] = proxRecorridoClienteSirviendose;
                                                    dr5["Cont CantArticulos C " + id] = verduleria.clienteSirviendose.cantArt;
                                                }
                                            }


                                            break;
                                        }
                                    case "P"://PANA
                                        {
                                            if (panaderia.Estado == "L")
                                            {

                                                verduleria.clienteSirviendose.Estado = "SAP";
                                                verduleria.clienteSirviendose.cantArt = 1 + verduleria.clienteSirviendose.cantArt;

                                                verduleria.Estado = "Oc";
                                                //Generar Tiempo para Panaderia

                                                panaderia.finAtencion = 3 + super.Reloj;


                                                dr5["*FinAtencion Panadería*"] = panaderia.finAtencion;
                                                panaderia.clienteSirviendose = verduleria.clienteSirviendose;
                                            }
                                            else
                                            {

                                                verduleria.clienteSirviendose.Estado = "EAP";
                                                panaderia.Cola.Enqueue(verduleria.clienteSirviendose);

                                            }

                                            




                                            //PARTE DEL CLIENTE
                                            dr5["*C " + id + "* Estado"] = verduleria.clienteSirviendose.Estado;
                                            dr5["*C " + id + "* Prox seccion"] = proxRecorridoClienteSirviendose;
                                            dr5["Cont CantArticulos C " + id] = verduleria.clienteSirviendose.cantArt;


                                            break;
                                        }

                                    default:
                                        break;
                                }
                                if (verduleria.Cola.Count != 0)
                                {
                                    verduleria.clienteSirviendose = verduleria.Cola.Dequeue();
                                    verduleria.clienteSirviendose.Estado = "SAV";
                                    verduleria.clienteSirviendose.cantArt = 1 + verduleria.clienteSirviendose.cantArt;

                                    verduleria.Estado = "Oc";
                                    //Generar Tiempo para Verduleria
                                    double rndV = Math.Round(RND.NextDouble(),4);
                                    verduleria.tiempoAtencion = Math.Round(super.generarPoisson(2,rndV),2);
                                    verduleria.finAtencion = Math.Round(verduleria.tiempoAtencion + super.Reloj,4);

                                    dr5["*FinAtencion Verduleria* RND"] = rndV;
                                    dr5["Tiempo AtencionV"] = verduleria.tiempoAtencion;
                                    dr5["FinTiempo AtencionV"] = verduleria.finAtencion;



                                }
                                else
                                {
                                    verduleria.Estado = "L";
                                    verduleria.clienteSirviendose = null;//Pongo que no tiene mas sirviendose
                                    verduleria.finAtencion = -1;//pongo -1 ya que no va a haber mas clientes q se tienen q servir
                                }
                                dr5["ProxLleg"] = super.ProximaLlegadaCliente;
                                if (carniceria.finAtencion != -1) { dr5["FinTiempo AtencionC"] = carniceria.finAtencion; }
                                if (verduleria.finAtencion != -1) { dr5["FinTiempo AtencionV"] = verduleria.finAtencion; }
                                if (panaderia.finAtencion != -1) { dr5["*FinAtencion Panadería*"] = panaderia.finAtencion; }
                                if (gondola.finAtencion != -1) { dr5["FinTiempo Atencion Gondola"] = gondola.finAtencion; }
                                if (cajaR.finAtencion != -1) { dr5["FinTiempo AtencíonCR"] = cajaR.finAtencion; }
                                if (caja2.finAtencion != -1) { dr5["FinTiempo AtencionC2"] = caja2.finAtencion; }
                                if (caja3.finAtencion != -1) { dr5["\nFinTiempoAC3"] = caja3.finAtencion; }
                                dr5["ContClientes Atendidos"] = super.CantClientesAtendidos;
                                dr5["*Verduleria* Estado"] = verduleria.Estado;
                                dr5["*Carniceria* Estado"] = carniceria.Estado;
                                dr5["*Panaderia* Estado"] = panaderia.Estado;
                                dr5["*Gondola* Estado"] = gondola.Estado;
                                dr5["*CajaRapida* Estado"] = cajaR.Estado;
                                dr5["*Caja2* Estado"] = caja2.Estado;
                                dr5["*Caja3* Estado"] = caja3.Estado;


                                dr5["\nColaV"] = verduleria.Cola.Count;
                                dr5["\nColaC"] = carniceria.Cola.Count;
                                dr5["\nColaP"] = panaderia.Cola.Count;
                                dr5["\nColaG"] = gondola.Cola.Count;
                                dr5["\nColaCR"] = cajaR.Cola.Count;
                                dr5["\nColaC2"] = caja2.Cola.Count;
                                dr5["\nColaC3"] = caja3.Cola.Count;

                                dt.Rows.Add(dr5);









                                break;
                            }
                        case "FAG"://Fin Atencion Gondola
                            {
                                //PASO UNO : ANOTAR TODOS LOS DATOS ESENCIALES EN ROW (5)
                                DataRow dr4 = dt.NewRow();
                                dr4["Evento"] = "Fin atenc Gondola";
                                
                                dr4["Reloj"] = Math.Round(super.Reloj, 4);
                                int id = gondola.clienteSirviendose.id;
                                string proxRecorridoClienteSirviendose = gondola.clienteSirviendose.Recorrido.Dequeue();

                                //PASO DOS : Mandar Cliente Sirviendose a seguir el circuito
                                switch (proxRecorridoClienteSirviendose)
                                {
                                    case "C"://CASO CAJA VER CUANTOS PRODUCTOS TRAE
                                        {
                                            if (gondola.clienteSirviendose.cantArt > 3)
                                            {
                                                bool queCaja = false;
                                                if (RND.Next(100) >= 50) { queCaja = true; }//DEFINIR PARA QUE CAJA VAN A IR (CAJA 2 o CAJA 3)
                                                //SI mayor que 50 va a caja 2
                                                if (caja2.Estado == "L" && queCaja == true)
                                                {

                                                    gondola.clienteSirviendose.Estado = "SAC2";
                                                    caja2.Estado = "Oc";
                                                    //Generar Tiempo para Caja2
                                                    caja2.clienteSirviendose = gondola.clienteSirviendose;
                                                    caja2.finAtencion = Math.Round(caja2.clienteSirviendose.cantArt * 0.333 + 1 + super.Reloj, 4);
                                                    dr4["*Atencion  Caja2* CantArticulos"] = caja2.clienteSirviendose.cantArt;
                                                    dr4["FinTiempo AtencionC2"] =caja2.finAtencion;

                                                    //PARTE DEL CLIENTE
                                                    dr4["*C " + id + "* Estado"] = caja2.clienteSirviendose.Estado;
                                                    dr4["*C " + id + "* Prox seccion"] = proxRecorridoClienteSirviendose;
                                                    dr4["Cont CantArticulos C " + id] = caja2.clienteSirviendose.cantArt;

                                                }
                                                else if (caja2.Estado == "Oc" && queCaja == true)
                                                {

                                                    gondola.clienteSirviendose.Estado = "EAC2";
                                                    caja2.Cola.Enqueue(gondola.clienteSirviendose);
                                                   
                                                    //PARTE DEL CLIENTE
                                                    dr4["*C " + id + "* Estado"] = caja2.clienteSirviendose.Estado;
                                                    dr4["*C " + id + "* Prox seccion"] = proxRecorridoClienteSirviendose;
                                                    dr4["Cont CantArticulos C " + id] = caja2.clienteSirviendose.cantArt;
                                                }
                                                else if (caja3.Estado == "L" && queCaja == false)
                                                {
                                                    gondola.clienteSirviendose.Estado = "SAC3";
                                                    caja3.Estado = "Oc";
                                                    //Generar Tiempo para Caja3
                                                    caja3.clienteSirviendose = gondola.clienteSirviendose;
                                                    caja3.finAtencion = Math.Round(caja3.clienteSirviendose.cantArt * 0.333 + 1 + super.Reloj, 4);
                                                    dr4["*AtencionCaja3* CantArticulos"] = caja3.clienteSirviendose.cantArt;
                                                    dr4["\nFinTiempoAC3"] =caja3.finAtencion;

                                                    //PARTE DEL CLIENTE
                                                    dr4["*C " + id + "* Estado"] = caja3.clienteSirviendose.Estado;
                                                    dr4["*C " + id + "* Prox seccion"] = proxRecorridoClienteSirviendose;
                                                    dr4["Cont CantArticulos C " + id] = caja3.clienteSirviendose.cantArt;


                                                }
                                                else if (caja3.Estado == "Oc" && queCaja == false)
                                                {
                                                    gondola.clienteSirviendose.Estado = "EAC3";
                                                    caja3.Cola.Enqueue(gondola.clienteSirviendose);
                                                   
                                                    //PARTE DEL CLIENTE
                                                    dr4["*C " + id + "* Estado"] = gondola.clienteSirviendose.Estado;
                                                    dr4["*C " + id + "* Prox seccion"] = proxRecorridoClienteSirviendose;
                                                    dr4["Cont CantArticulos C " + id] = gondola.clienteSirviendose.cantArt;

                                                }

                                            }
                                            else//CAJA RAPIDA 
                                            {
                                                if (cajaR.Estado == "L")
                                                {

                                                    gondola.clienteSirviendose.Estado = "SACR";
                                                    cajaR.Estado = "Oc";
                                                    //Generar Tiempo para CajaR
                                                    cajaR.clienteSirviendose = gondola.clienteSirviendose;
                                                    cajaR.finAtencion = Math.Round(cajaR.clienteSirviendose.cantArt * 0.333 + 1 + super.Reloj, 4);
                                                    dr4["*Atencion CajaRapida* CantArticulos"] = cajaR.clienteSirviendose.cantArt;
                                                    dr4["FinTiempo AtencíonCR"] =cajaR.finAtencion;



                                                    //PARTE DEL CLIENTE
                                                    dr4["*C " + id + "* Estado"] = cajaR.clienteSirviendose.Estado;
                                                    dr4["*C " + id + "* Prox seccion"] = proxRecorridoClienteSirviendose;
                                                    dr4["Cont CantArticulos C " + id] = cajaR.clienteSirviendose.cantArt;

                                                }
                                                else if (cajaR.Estado == "Oc")
                                                {

                                                    gondola.clienteSirviendose.Estado = "EACR";
                                                    cajaR.Cola.Enqueue(gondola.clienteSirviendose);
                                                    
                                                    //PARTE DEL CLIENTE
                                                    dr4["*C " + id + "* Estado"] = gondola.clienteSirviendose.Estado;
                                                    dr4["*C " + id + "* Prox seccion"] = proxRecorridoClienteSirviendose;
                                                    dr4["Cont CantArticulos C " + id] = gondola.clienteSirviendose.cantArt;
                                                }
                                            }


                                            break;
                                        }
                                    case "V": {
                                            
                                            if (verduleria.Estado == "L")
                                            {

                                                gondola.clienteSirviendose.Estado = "SAV";
                                                gondola.clienteSirviendose.cantArt = 1 + gondola.clienteSirviendose.cantArt;

                                                verduleria.Estado = "Oc";
                                                
                                                //Generar Tiempo para Verduleria
                                                double rndV = Math.Round(RND.NextDouble(), 4);
                                                verduleria.tiempoAtencion = Math.Round(super.generarPoisson(2, rndV), 2);
                                                verduleria.finAtencion = Math.Round(verduleria.tiempoAtencion + super.Reloj,4);

                                                dr4["*FinAtencion Verduleria* RND"] = rndV;
                                                dr4["Tiempo AtencionV"] = verduleria.tiempoAtencion;
                                                dr4["FinTiempo AtencionV"] = verduleria.finAtencion;
                                                verduleria.clienteSirviendose = gondola.clienteSirviendose;
                                            }
                                            else
                                            {

                                                gondola.clienteSirviendose.Estado = "EAV";
                                                verduleria.Cola.Enqueue(gondola.clienteSirviendose);

                                            }

                                           




                                            //PARTE DEL CLIENTE
                                            dr4["*C " + id + "* Estado"] = verduleria.clienteSirviendose.Estado;
                                            dr4["*C " + id + "* Prox seccion"] = proxRecorridoClienteSirviendose;
                                            dr4["Cont CantArticulos C " + id] = verduleria.clienteSirviendose.cantArt;



                                            break;
                                        }
                                    default:
                                        break;
                                }
                                // PARTE TRES QUE HACER SI HAY GENTE ESPERANDO O NO

                                if (gondola.Cola.Count != 0)
                            {
                                gondola.clienteSirviendose = gondola.Cola.Dequeue();
                                gondola.clienteSirviendose.Estado = "SAG";
                                 
                                
                                gondola.Estado = "Oc";
                                    //Generar Tiempo para Gondola
                                    double rndG = RND.Next(100);
                                    gondola.clienteSirviendose.cantArt = gondola.generarCantArticulos(rndG);
                                    gondola.finAtencion = Math.Round(gondola.clienteSirviendose.cantArt + super.Reloj, 4);
                                    dr4["*FinAtencion Gondola* RND"] = rndG;
                                    dr4["Cant Articulos Gondola"] = gondola.clienteSirviendose.cantArt;
                                    dr4["FinTiempo Atencion Gondola"] = gondola.finAtencion;



                                }
                            else
                            {
                                gondola.Estado = "L";
                                gondola.clienteSirviendose = null;//Pongo que no tiene mas sirviendose
                                gondola.finAtencion = -1;//pongo -1 ya que no va a haber mas clientes q se tienen q servir
                            }
                                dr4["ProxLleg"] = super.ProximaLlegadaCliente;
                                //Los fin atencion de los eventos se setean al principio con -1 , Si nunca se escribió un fin Atencion de un server no debe poner nada en Fin tiempo de la fila siguiente
                                if (carniceria.finAtencion != -1) { dr4["FinTiempo AtencionC"] = carniceria.finAtencion; }
                                if (verduleria.finAtencion != -1) { dr4["FinTiempo AtencionV"] = verduleria.finAtencion; }
                                if (panaderia.finAtencion != -1) { dr4["*FinAtencion Panadería*"] = panaderia.finAtencion; }
                                if (gondola.finAtencion != -1) { dr4["FinTiempo Atencion Gondola"] = gondola.finAtencion; }
                                if (cajaR.finAtencion != -1) { dr4["FinTiempo AtencíonCR"] = cajaR.finAtencion; }
                                if (caja2.finAtencion != -1) { dr4["FinTiempo AtencionC2"] = caja2.finAtencion; }
                                if (caja3.finAtencion != -1) { dr4["\nFinTiempoAC3"] = caja3.finAtencion; }
                                dr4["ContClientes Atendidos"] = super.CantClientesAtendidos;
                            dr4["*Verduleria* Estado"] = verduleria.Estado;
                            dr4["*Carniceria* Estado"] = carniceria.Estado;
                            dr4["*Panaderia* Estado"] = panaderia.Estado;
                            dr4["*Gondola* Estado"] = gondola.Estado;
                            dr4["*CajaRapida* Estado"] = cajaR.Estado;
                            dr4["*Caja2* Estado"] = caja2.Estado;
                            dr4["*Caja3* Estado"] = caja3.Estado;


                            dr4["\nColaV"] = verduleria.Cola.Count;
                            dr4["\nColaC"] = carniceria.Cola.Count;
                            dr4["\nColaP"] = panaderia.Cola.Count;
                            dr4["\nColaG"] = gondola.Cola.Count;
                            dr4["\nColaCR"] = cajaR.Cola.Count;
                            dr4["\nColaC2"] = caja2.Cola.Count;
                            dr4["\nColaC3"] = caja3.Cola.Count;
                                
                                dt.Rows.Add(dr4);

                                break;


                          


                            }

                        case "FAC2"://Fin Atencion Caja 2 
                            {
                                DataRow dr6 = dt.NewRow();
                                dr6["Evento"] = "Fin atenc Caja 2";
                                dr6["Reloj"] = Math.Round(super.Reloj, 4);

                                if (caja2.Cola.Count != 0)
                                {
                                    caja2.clienteSirviendose = caja2.Cola.Dequeue();
                                    caja2.finAtencion = Math.Round(caja2.clienteSirviendose.cantArt * 0.333 +1 + super.Reloj,4);
                                    dr6["*Atencion  Caja2* CantArticulos"] = caja2.clienteSirviendose.cantArt;
                                    dr6["FinTiempo AtencionC2"] = caja2.finAtencion;

                                }
                                else
                                {
                                    caja2.Estado = "L";
                                    caja2.clienteSirviendose = null;
                                    caja2.finAtencion = -1;
                                   
                                }
                                dr6["ProxLleg"] = super.ProximaLlegadaCliente;
                                super.CantClientesAtendidos = super.CantClientesAtendidos + 1;
                                //Los fin atencion de los eventos se setean al principio con -1 , Si nunca se escribió un fin Atencion de un server no debe poner nada en Fin tiempo de la fila siguiente
                                if (carniceria.finAtencion != -1) { dr6["FinTiempo AtencionC"] = carniceria.finAtencion; }
                                if (verduleria.finAtencion != -1) { dr6["FinTiempo AtencionV"] = verduleria.finAtencion; }
                                if (panaderia.finAtencion != -1) { dr6["*FinAtencion Panadería*"] = panaderia.finAtencion; }
                                if (gondola.finAtencion != -1) { dr6["FinTiempo Atencion Gondola"] = gondola.finAtencion; }
                                if (cajaR.finAtencion != -1) { dr6["FinTiempo AtencíonCR"] = cajaR.finAtencion; }
                                if (caja2.finAtencion != -1) { dr6["FinTiempo AtencionC2"] = caja2.finAtencion; }
                                if (caja3.finAtencion != -1) { dr6["\nFinTiempoAC3"] = caja3.finAtencion; }
                                dr6["ContClientes Atendidos"] = super.CantClientesAtendidos;
                                dr6["*Verduleria* Estado"] = verduleria.Estado;
                                dr6["*Carniceria* Estado"] = carniceria.Estado;
                                dr6["*Panaderia* Estado"] = panaderia.Estado;
                                dr6["*Gondola* Estado"] = gondola.Estado;
                                dr6["*CajaRapida* Estado"] = cajaR.Estado;
                                dr6["*Caja2* Estado"] = caja2.Estado;
                                dr6["*Caja3* Estado"] = caja3.Estado;


                                dr6["\nColaV"] = verduleria.Cola.Count;
                                dr6["\nColaC"] = carniceria.Cola.Count;
                                dr6["\nColaP"] = panaderia.Cola.Count;
                                dr6["\nColaG"] = gondola.Cola.Count;
                                dr6["\nColaCR"] = cajaR.Cola.Count;
                                dr6["\nColaC2"] = caja2.Cola.Count;
                                dr6["\nColaC3"] = caja3.Cola.Count;
                                dt.Rows.Add(dr6);
                                break;
                            
                              
                            }
                        case "FAC3"://Fin Atencion Caja 3
                            {
                                DataRow dr7 = dt.NewRow();
                                dr7["Evento"] = "Fin atenc Caja 3";
                                dr7["Reloj"] = Math.Round(super.Reloj, 4);

                                if (caja3.Cola.Count != 0)
                                {
                                    caja3.clienteSirviendose = caja3.Cola.Dequeue();
                                    caja3.finAtencion = Math.Round(caja3.clienteSirviendose.cantArt * 0.333 + 1 + super.Reloj, 4);
                                    dr7["*AtencionCaja3* CantArticulos"] = caja3.clienteSirviendose.cantArt;
                                    dr7["\nFinTiempoAC3"] = caja3.finAtencion;
                                  
                                }
                                else
                                {
                                    caja3.Estado = "L";
                                    caja3.clienteSirviendose = null;
                                    caja3.finAtencion = -1;

                                }
                                dr7["ProxLleg"] = super.ProximaLlegadaCliente;
                                super.CantClientesAtendidos = super.CantClientesAtendidos + 1;
                                //Los fin atencion de los eventos se setean al principio con -1 , Si nunca se escribió un fin Atencion de un server no debe poner nada en Fin tiempo de la fila siguiente
                                if (carniceria.finAtencion != -1) { dr7["FinTiempo AtencionC"] = carniceria.finAtencion; }
                                if (verduleria.finAtencion != -1) { dr7["FinTiempo AtencionV"] = verduleria.finAtencion; }
                                if (panaderia.finAtencion != -1) { dr7["*FinAtencion Panadería*"] = panaderia.finAtencion; }
                                if (gondola.finAtencion != -1) { dr7["FinTiempo Atencion Gondola"] = gondola.finAtencion; }
                                if (cajaR.finAtencion != -1) { dr7["FinTiempo AtencíonCR"] = cajaR.finAtencion; }
                                if (caja2.finAtencion != -1) { dr7["FinTiempo AtencionC2"] = caja2.finAtencion; }
                                if (caja3.finAtencion != -1) { dr7["\nFinTiempoAC3"] = caja3.finAtencion; }
                               
                                dr7["ContClientes Atendidos"] = super.CantClientesAtendidos;
                                dr7["*Verduleria* Estado"] = verduleria.Estado;
                                dr7["*Carniceria* Estado"] = carniceria.Estado;
                                dr7["*Panaderia* Estado"] = panaderia.Estado;
                                dr7["*Gondola* Estado"] = gondola.Estado;
                                dr7["*CajaRapida* Estado"] = cajaR.Estado;
                                dr7["*Caja2* Estado"] = caja2.Estado;
                                dr7["*Caja3* Estado"] = caja3.Estado;


                                dr7["\nColaV"] = verduleria.Cola.Count;
                                dr7["\nColaC"] = carniceria.Cola.Count;
                                dr7["\nColaP"] = panaderia.Cola.Count;
                                dr7["\nColaG"] = gondola.Cola.Count;
                                dr7["\nColaCR"] = cajaR.Cola.Count;
                                dr7["\nColaC2"] = caja2.Cola.Count;
                                dr7["\nColaC3"] = caja3.Cola.Count;

                                dt.Rows.Add(dr7);
                                break;
                            }
                        case "FACR"://Fin Atencion Caja R 
                            {
                                DataRow dr8 = dt.NewRow();
                                dr8["Evento"] = "Fin atenc Caja R";
                                dr8["Reloj"] = Math.Round(super.Reloj, 4);

                                if (cajaR.Cola.Count != 0)
                                {
                                    cajaR.clienteSirviendose = cajaR.Cola.Dequeue();
                                    cajaR.finAtencion = Math.Round(cajaR.clienteSirviendose.cantArt * 0.333 + 1 + super.Reloj, 4);
                                    dr8["*Atencion CajaRapida* CantArticulos"] = cajaR.clienteSirviendose.cantArt;
                                    dr8["FinTiempo AtencíonCR"] = cajaR.finAtencion;

                                }
                                else
                                {
                                    cajaR.Estado = "L";
                                    cajaR.clienteSirviendose = null;
                                    cajaR.finAtencion = -1;

                                }
                                super.CantClientesAtendidos = super.CantClientesAtendidos + 1;
                                dr8["ProxLleg"] = super.ProximaLlegadaCliente;
                                //Los fin atencion de los eventos se setean al principio con -1 , Si nunca se escribió un fin Atencion de un server no debe poner nada en Fin tiempo de la fila siguiente
                                if (carniceria.finAtencion != -1) { dr8["FinTiempo AtencionC"] = carniceria.finAtencion; }
                                if (verduleria.finAtencion != -1) { dr8["FinTiempo AtencionV"] = verduleria.finAtencion; }
                                if (panaderia.finAtencion != -1) { dr8["*FinAtencion Panadería*"] = panaderia.finAtencion; }
                                if (gondola.finAtencion != -1) { dr8["FinTiempo Atencion Gondola"] = gondola.finAtencion; }
                                if (cajaR.finAtencion != -1) { dr8["FinTiempo AtencíonCR"] = cajaR.finAtencion; }
                                if (caja2.finAtencion != -1) { dr8["FinTiempo AtencionC2"] = caja2.finAtencion; }
                                if (caja3.finAtencion != -1) { dr8["\nFinTiempoAC3"] = caja3.finAtencion; }

                                
                                dr8["ContClientes Atendidos"] = super.CantClientesAtendidos;
                                dr8["*Verduleria* Estado"] = verduleria.Estado;
                                dr8["*Carniceria* Estado"] = carniceria.Estado;
                                dr8["*Panaderia* Estado"] = panaderia.Estado;
                                dr8["*Gondola* Estado"] = gondola.Estado;
                                dr8["*CajaRapida* Estado"] = cajaR.Estado;
                                dr8["*Caja2* Estado"] = caja2.Estado;
                                dr8["*Caja3* Estado"] = caja3.Estado;


                                dr8["\nColaV"] = verduleria.Cola.Count;
                                dr8["\nColaC"] = carniceria.Cola.Count;
                                dr8["\nColaP"] = panaderia.Cola.Count;
                                dr8["\nColaG"] = gondola.Cola.Count;
                                dr8["\nColaCR"] = cajaR.Cola.Count;
                                dr8["\nColaC2"] = caja2.Cola.Count;
                                dr8["\nColaC3"] = caja3.Cola.Count;
                                dt.Rows.Add(dr8);
                                break;
                            }
                        default:
                            MessageBox.Show("Hubo un error en el switch de Eventos");
                            break;
                    }


                    // DeterminarEventoSiguiente() metodo que va a actualizar EventoSiguiente 
                    
                    super.EventoSiguiente = DeterminarEventoSiguiente();
                    
                   
                }

                if (dt.Columns.Count > 500) { //ELIMINO COLUMNAS 

                    for (int i = 1; i < contClientesEntrantes - 50; i++)
                    {
                        
                        dt.Columns.Remove("*C " + i + "* Estado");
                        dt.Columns.Remove("*C " + i + "* Prox seccion");
                        dt.Columns.Remove("Cont CantArticulos C " + i);
                    }


                }

               this.dgv_simulacion.DataSource = dt;
                this.colorColumnas();






            }
           //RESULTADO DE LA SIMULACIÓN
            int x = 0;
            Int32.TryParse(txt_horas.Text, out x);
            int prom = 420 * super.CantClientesAtendidos / x;
            this.lbl_resultado.Text = "   Llegaron " + contClientesEntrantes + " clientes en " + txt_horas.Text + " minutos.           Se atendieron " + super.CantClientesAtendidos + " consumidores.        Si el supermercado atiende 8 horas (420 min), en promedio obtenemos que se atendieron " + prom;
        }

        private String DeterminarEventoSiguiente()
        {
            List<double> menorTiempo = new List<double> { carniceria.finAtencion, verduleria.finAtencion, super.ProximaLlegadaCliente, gondola.finAtencion, caja2.finAtencion, panaderia.finAtencion, caja3.finAtencion, cajaR.finAtencion };
            //remuevo todos los -1
            menorTiempo.RemoveAll(item => item == -1);
            //busco el menor tiempo
            menorTiempo.Sort();
            double menorTiempoEvento = menorTiempo[0];
            super.Reloj = menorTiempoEvento;//ASIGNO A RELOJ
           
            if (menorTiempoEvento == carniceria.finAtencion) { return "FAC"; }
            if (menorTiempoEvento == verduleria.finAtencion) { return "FAV"; }
            if(menorTiempoEvento == panaderia.finAtencion) { return "FAP"; }
            if (menorTiempoEvento == gondola.finAtencion) { return "FAG"; }
            if (menorTiempoEvento == cajaR.finAtencion) { return "FACR"; }
            if (menorTiempoEvento == caja2.finAtencion) { return "FAC2"; }
            if (menorTiempoEvento == super.ProximaLlegadaCliente) { return "LLC"; }

            return "FAC3";

        }




        // -----------------INTERFAZ------------------//

        private int horas, desde, hasta;

        
        private void PantallaSimulacion_Load(object sender, EventArgs e)
        {
            lbl_resultado.Text = "";

            // valores por defecto
            txt_horas.Text = "200";
            txt_desde.Text = "0";
            txt_hasta.Text = "200";
        }
        private Boolean validar_campos() // para validar que los valores de los campos del form esten bien ingresados
        {
            if (txt_horas.Text == "")
            {
                return false;
            }
            horas = Convert.ToInt32(txt_horas.Text);
            desde = Convert.ToInt32(txt_desde.Text);
            hasta = Convert.ToInt32(txt_hasta.Text);

            
            if (desde >= hasta || hasta > horas) // valida el rango
            {
                MessageBox.Show("El rango ingresado no es válido.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_desde.Text = "";
                txt_hasta.Text = "";
                txt_desde.Focus();
                return false;
            }
            return true;
        }




        private void colorColumnas()
        {
            Color llegCliente = Color.Silver;
            dgv_simulacion.Columns[2].DefaultCellStyle.BackColor = llegCliente;
            dgv_simulacion.Columns[3].DefaultCellStyle.BackColor = llegCliente;
            dgv_simulacion.Columns[4].DefaultCellStyle.BackColor = llegCliente;
            Color recorrido = Color.LightGray;
            dgv_simulacion.Columns[5].DefaultCellStyle.BackColor = recorrido;
            dgv_simulacion.Columns[6].DefaultCellStyle.BackColor = recorrido;
           
            // VERDULERIA
            Color verduleria = Color.LightGreen;
            lbl_verduleria.BackColor = verduleria;
            lbl_verduleria.Visible = true;
            dgv_simulacion.Columns[7].DefaultCellStyle.BackColor = verduleria;
            dgv_simulacion.Columns[8].DefaultCellStyle.BackColor = verduleria;
            dgv_simulacion.Columns[9].DefaultCellStyle.BackColor = verduleria;
            // CARNICERIA
            Color carniceria = Color.LightSalmon;
            lbl_carniceria.BackColor = carniceria;
            lbl_carniceria.Visible = true;
            dgv_simulacion.Columns[10].DefaultCellStyle.BackColor = carniceria;
            dgv_simulacion.Columns[11].DefaultCellStyle.BackColor = carniceria;
            dgv_simulacion.Columns[12].DefaultCellStyle.BackColor = carniceria;
            // PANADERIA
            Color panaderia = Color.LightBlue;
            lbl_panaderia.BackColor = panaderia;
            lbl_panaderia.Visible = true;
            dgv_simulacion.Columns[13].DefaultCellStyle.BackColor = panaderia;
            // GONDOLA
            Color gondola = Color.PaleVioletRed;
            lbl_gondola.BackColor = gondola;
            lbl_gondola.Visible = true;
            dgv_simulacion.Columns[14].DefaultCellStyle.BackColor = gondola;
            dgv_simulacion.Columns[15].DefaultCellStyle.BackColor = gondola;
            dgv_simulacion.Columns[16].DefaultCellStyle.BackColor = gondola;
            // CAJA RAPIDA
            Color cajaRapida = Color.DarkKhaki;
            lbl_cajaRapida.BackColor = cajaRapida;
            lbl_cajaRapida.Visible = true;
            dgv_simulacion.Columns[17].DefaultCellStyle.BackColor = cajaRapida;
            dgv_simulacion.Columns[18].DefaultCellStyle.BackColor = cajaRapida;
            // CAJA 2
            Color caja2 = Color.PaleGoldenrod;
            lbl_caja2.BackColor = caja2;
            lbl_caja2.Visible = true;
            dgv_simulacion.Columns[19].DefaultCellStyle.BackColor = caja2;
            dgv_simulacion.Columns[20].DefaultCellStyle.BackColor = caja2;
            // CAJA 1
            Color caja3 = Color.LemonChiffon;
            lbl_caja3.BackColor = caja3;
            lbl_caja3.Visible = true;
            dgv_simulacion.Columns[21].DefaultCellStyle.BackColor = caja3;
            dgv_simulacion.Columns[22].DefaultCellStyle.BackColor = caja3;

            dgv_simulacion.Columns[23].DefaultCellStyle.BackColor = verduleria;
            dgv_simulacion.Columns[24].DefaultCellStyle.BackColor = verduleria;

            dgv_simulacion.Columns[25].DefaultCellStyle.BackColor = carniceria;
            dgv_simulacion.Columns[26].DefaultCellStyle.BackColor = carniceria;

            dgv_simulacion.Columns[27].DefaultCellStyle.BackColor = panaderia;
            dgv_simulacion.Columns[28].DefaultCellStyle.BackColor = panaderia;

            dgv_simulacion.Columns[29].DefaultCellStyle.BackColor = gondola;
            dgv_simulacion.Columns[30].DefaultCellStyle.BackColor = gondola;

            dgv_simulacion.Columns[31].DefaultCellStyle.BackColor = cajaRapida;
            dgv_simulacion.Columns[32].DefaultCellStyle.BackColor = cajaRapida;

            dgv_simulacion.Columns[33].DefaultCellStyle.BackColor = caja2;
            dgv_simulacion.Columns[34].DefaultCellStyle.BackColor = caja2;

            dgv_simulacion.Columns[35].DefaultCellStyle.BackColor = caja3;
            dgv_simulacion.Columns[36].DefaultCellStyle.BackColor = caja3;
            //contador
            dgv_simulacion.Columns[37].DefaultCellStyle.BackColor = Color.Gainsboro;
        }

        private void inicializarColumnas()
        {
            dt.Rows.Clear();

            dt.Columns.Add("Evento", typeof(string));
            dt.Columns.Add("Reloj", typeof(double));
            // LLEGADA CLIENTE
            dt.Columns.Add("*Llegada cliente* RND", typeof(double));
            dt.Columns.Add("TiempoLleg", typeof(double));
            dt.Columns.Add("ProxLleg", typeof(double));
            // RECORRIDO
            dt.Columns.Add("*Recorrido* RND", typeof(double));
            dt.Columns.Add("Recorrido", typeof(string));
            // VERDULERIA
            dt.Columns.Add("*FinAtencion Verduleria* RND", typeof(double));
            dt.Columns.Add("Tiempo AtencionV", typeof(double));
            dt.Columns.Add("FinTiempo AtencionV", typeof(double));
            // CARNICERIA
            dt.Columns.Add("*FinAtencion Carniceria* RND", typeof(double));
            dt.Columns.Add("Tiempo AtencionC", typeof(double));
            dt.Columns.Add("FinTiempo AtencionC", typeof(double));
            // PANADERIA
            dt.Columns.Add("*FinAtencion Panadería*", typeof(double));
            // GÓNDOLA
            dt.Columns.Add("*FinAtencion Gondola* RND", typeof(double));
            dt.Columns.Add("Cant Articulos Gondola", typeof(Int32));
            dt.Columns.Add("FinTiempo Atencion Gondola", typeof(double));
            // CAJAS
            // CAJA Rápida 
            dt.Columns.Add("*Atencion CajaRapida* CantArticulos", typeof(Int32));
            dt.Columns.Add("FinTiempo AtencíonCR", typeof(double));
            // CAJA 2 
            dt.Columns.Add("*Atencion  Caja2* CantArticulos", typeof(Int32));
            dt.Columns.Add("FinTiempo AtencionC2", typeof(double));
            // CAJA 3 
            dt.Columns.Add("*AtencionCaja3* CantArticulos", typeof(Int32));
            dt.Columns.Add("\nFinTiempoAC3", typeof(double));
            //OBJETOS
            //Verduleria
            dt.Columns.Add("*Verduleria* Estado", typeof(string));
            dt.Columns.Add("\nColaV", typeof(Int32));
            //Carniceria
            dt.Columns.Add("*Carniceria* Estado", typeof(string));
            dt.Columns.Add("\nColaC", typeof(Int32));
            //Panaderia
            dt.Columns.Add("*Panaderia* Estado", typeof(string));
            dt.Columns.Add("\nColaP", typeof(Int32));
            //Gondola
            dt.Columns.Add("*Gondola* Estado", typeof(string));
            dt.Columns.Add("\nColaG", typeof(Int32));
            // CAJA Rápida 
            dt.Columns.Add("*CajaRapida* Estado", typeof(string));
            dt.Columns.Add("\nColaCR", typeof(Int32));
            // CAJA 2 
            dt.Columns.Add("*Caja2* Estado", typeof(string));
            dt.Columns.Add("\nColaC2", typeof(Int32));
            // CAJA 3 
            dt.Columns.Add("*Caja3* Estado", typeof(string));
            dt.Columns.Add("\nColaC3", typeof(Int32));
            //ContClientes Atendidos
            dt.Columns.Add("ContClientes Atendidos", typeof(Int32));

        }

        
        private void iniciarPrimeraFila()
        {
            DataRow dr = dt.NewRow();
            super.Reloj = 0.0;
            super.AleatorioLlegadaCliente = super.generarAleatorio();
            super.LlegadaCliente = super.generarPoisson(0.5, super.AleatorioLlegadaCliente);
            super.ProximaLlegadaCliente = super.Reloj + super.LlegadaCliente;
            super.EventoSiguiente = "LLC";

            carniceria.finAtencion = -1;
            verduleria.finAtencion = -1;
            panaderia.finAtencion = -1;
            gondola.finAtencion = -1;
            caja3.finAtencion = -1;
            caja2.finAtencion = -1;
            cajaR.finAtencion = -1;

            
            

            super.CantClientesAtendidos = 0;
            //COLUMNA LLEGADA CLIENTE//
            dr["Reloj"] = super.Reloj;
            dr["*Llegada cliente* RND"] = super.AleatorioLlegadaCliente;
            dr["TiempoLleg"] = super.LlegadaCliente;
            dr["ProxLleg"] = super.ProximaLlegadaCliente;
            
            
            
            // COLUMNA RECORRIDO // 
            //super.AleatorioRecorrido = RND.Next(100);
            //super.IDRecorrido = super.generarRecorrido(super.AleatorioRecorrido);
            //string cadena = super.generarCadenaRecorrido(super.IDRecorrido);
            //dr["*Recorrido* RND"] = super.AleatorioRecorrido;
            //dr["Recorrido"] = cadena;
            //dr["idRec"] = super.IDRecorrido;
            //// VERDULERIA
            //dr["*FinAtencion Verduleria* RND"] = ;
            //dr["Tiempo AtencionV"] = ;
            //dr["FinTiempo AtencionV"] = ;
            //// CARNICERIA
            //dr["*FinAtencion Carniceria* RND"] = ;
            //dr["Tiempo AtencionC"] = ;
            //dr["FinTiempo AtencionC"] = ;
            //// PANADERIA
            //dr["*FinAtencion Panadería*"] = ;
            //// GÓNDOLA
            //dr["*FinAtencion Gondola* RND"] = ;
            //dr["Cant Articulos Gondola"] = ;
            //dr["FinTiempo Atencion Gondola"] = ;
            //// CAJAS
            //// CAJA Rápida 
            //dr["*Atencion CajaRapida* CantArticulos"] = ;
            //dr["FinTiempo AtencíonCR"] = ;
            //// CAJA 2 
            //dr["*Atencion  Caja2* CantArticulos"] = ;
            //dr["FinTiempo AtencionC2"] = ;
            //// CAJA 3 
            //dr["*AtencionCaja3* CantArticulos"] = ;
            //dr["\nFinTiempoAC3"] = ;
            //OBJETOS
            //Verduleria
            string[] objetos = { "Verduleria", "Carniceria", "Panaderia", "Gondola", "CajaRapida", "Caja2", "Caja3" };
            string[] objetosLetra = { "V", "C", "P", "G", "CR", "C2", "C3" };
            foreach (string item in objetos)
            {
                dr["*" + item + "* Estado"] = "L";
            }
            foreach (string item in objetosLetra)
            {
                dr["\nCola" + item] = 0;
            }
            
            //ContClientes Atendidos
            dr["ContClientes Atendidos"] = 0;
            dt.Rows.Add(dr);

        }



        public PantallaSimulacion()
        {
            InitializeComponent();
        }
       

    }
}

