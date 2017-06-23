
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
        private Supermercado super;
        private static Random RND;

        //Instaciación de clases
        private Verduleria verduleria;
        private Carniceria carniceria;
        private Panaderia panaderia;
        private Gondola gondola;
        private Caja caja2, caja3;
        private CajaR cajaR;

        private List<Cliente> clientes;
        private int contClientesEntrantes; // sirve para ir numerando los clientes

        // inicializa todos los objetos para le ejecucion de la simulacion
        private void inicializarObjetosSimulacion()
        {
            //ATRIBUTOS//
            dt = new DataTable();
            super = new Supermercado();
            RND = new Random();

            //Instaciación de clases
            verduleria = new Verduleria();
            carniceria = new Carniceria();
            panaderia = new Panaderia();
            gondola = new Gondola();
            caja3 = new Caja();
            caja2 = new Caja();
            cajaR = new CajaR();

            clientes = new List<Cliente>();
            contClientesEntrantes = 0;
        }

        //FUNCIONALIDAD//
        private void btn_generar_Click_1(object sender, EventArgs e)
        {
            if (validar_campos())
            {
                this.inicializarObjetosSimulacion();
                this.inicializarColumnas(); //Todas las columnas menos los clientes           
                this.iniciarPrimeraFila();//iniciarPrimeraFila

                //agregarColumnaNuevoCliente(); //empezar a funcionar

                while (super.Reloj < horas)
                {
                    //if (super.EventoSiguiente == "FIN SIMULACION") { return; }  // AGREGO ESTO PARA VERIFICAR QUE EL EVENTO SIGUIENTE NO SEA DE UN TIEMPO > HORAS
                    DataRow dr = dt.NewRow();

                    switch (super.EventoSiguiente) //Que tipo de Evento es?
                    {
                        case "LLC"://llegada Cliente 
                            
                            Cliente nuevo = new Cliente();
                            clientes.Add(nuevo);
                            contClientesEntrantes = contClientesEntrantes + 1;
                            int i = contClientesEntrantes;
                            nuevo.id = i;

                            dr["Evento"] = "Lleg. Cliente " + nuevo.id;
                            super.Reloj = super.ProximaLlegadaCliente;
                            super.AleatorioLlegadaCliente = Math.Round(RND.NextDouble(), 4);
                            super.LlegadaCliente = super.generarPoisson(0.5, super.AleatorioLlegadaCliente);
                            super.ProximaLlegadaCliente = super.Reloj + super.LlegadaCliente;
                            dr["Reloj"] = super.Reloj;
                            dr["*Llegada cliente* RND"] = super.AleatorioLlegadaCliente;
                            dr["TiempoLleg"] = super.LlegadaCliente;
                            dr["ProxLleg"] = super.ProximaLlegadaCliente;

                            //Generar recorrido
                            super.AleatorioRecorrido = RND.Next(100);
                            super.IDRecorrido = super.generarRecorrido(super.AleatorioRecorrido);
                            string cadena = super.generarCadenaRecorrido(super.IDRecorrido);
                            dr["*Recorrido* RND"] = super.AleatorioRecorrido;
                            dr["Recorrido"] = cadena;
                            dr["ContClientes Atendidos"] = super.CantClientesAtendidos;                            

                            switch (super.IDRecorrido)
                            {
                                case 1://Verduleria-Panaderia                                    
                                    nuevo.Recorrido.Enqueue("V"); nuevo.Recorrido.Enqueue("P"); nuevo.Recorrido.Enqueue("C");
                                    nuevo.id_recorrido = nuevo.Recorrido.Dequeue();//saco de la cola
                                    if (verduleria.Estado == "L")
                                    {
                                        nuevo.Estado = "SAV";
                                        nuevo.cantArt = 1;
                                        
                                        verduleria.Estado = "Oc";
                                        double rnd = Math.Round(RND.NextDouble(), 4);
                                        dr["*FinAtencion Verduleria* RND"] = rnd;
                                        verduleria.tiempoAtencion = super.generarPoisson(2, rnd);                  // 2' = 0.0333 (HORARIO -> DECIMAL)          
                                        dr["Tiempo AtencionV"] = verduleria.tiempoAtencion;
                                        verduleria.finAtencion = verduleria.tiempoAtencion + super.Reloj;             // ESTO HACE QUE SE CLAVE POR ESO LO COMENTE
                                        dr["FinTiempo AtencionV"] = verduleria.finAtencion;
                                        verduleria.Cola.Enqueue(nuevo);//Si esta Siendo Atendido tambien lo meto en cola para no perderlo
                                        dr["\nColaV"] = verduleria.Cola.Count - 1;
                                       
                                    }
                                    else
                                    {
                                        nuevo.Estado = "EAV";
                                        verduleria.Cola.Enqueue(nuevo);
                                        dr["\nColaV"] = verduleria.Cola.Count - 1;
                                    };
                                    break;
                                case 2: //Verduleria-Carniceria-Gondola 
                                    nuevo.Recorrido.Enqueue("V"); nuevo.Recorrido.Enqueue("Ca"); nuevo.Recorrido.Enqueue("G"); nuevo.Recorrido.Enqueue("C");
                                    nuevo.id_recorrido = nuevo.Recorrido.Dequeue();//saco de la cola
                                    if (verduleria.Estado == "L")
                                    {
                                        nuevo.Estado = "SAV";
                                        nuevo.cantArt = 1;

                                        verduleria.Estado = "Oc";
                                        double rnd = Math.Round(RND.NextDouble(), 4);
                                        dr["*FinAtencion Verduleria* RND"] = rnd;
                                        verduleria.tiempoAtencion = super.generarPoisson(2, rnd);                        
                                        dr["Tiempo AtencionV"] = verduleria.tiempoAtencion;
                                        verduleria.finAtencion = verduleria.tiempoAtencion + super.Reloj;             // ESTO HACE QUE SE CLAVE POR ESO LO COMENTE
                                        dr["FinTiempo AtencionV"] = verduleria.finAtencion;
                                        verduleria.Cola.Enqueue(nuevo);//Si esta Siendo Atendido tambien lo meto en cola para no perderlo
                                        dr["\nColaV"] = verduleria.Cola.Count - 1;
                                    }
                                    else
                                    {
                                        nuevo.Estado = "EAV";
                                        verduleria.Cola.Enqueue(nuevo);
                                        dr["\nColaV"] = verduleria.Cola.Count - 1;
                                    };
                                    break;
                                case 3: //Panaderia
                                    nuevo.Recorrido.Enqueue("P"); nuevo.Recorrido.Enqueue("C");
                                    if (panaderia.Estado == "L")
                                    {
                                        nuevo.Estado = "SAP";
                                        nuevo.cantArt = 1;
                                        //nuevo.id_recorrido = nuevo.Recorrido.Dequeue();
                                        panaderia.Estado = "Oc";
                                        dr["*FinAtencion Panadería*"] = super.Reloj + 0.05;      // 3' = 0.05 (HORARIO -> DECIMAL)
                                        panaderia.Cola.Enqueue(nuevo);//Si esta Siendo Atendido tambien lo meto en cola para no perderlo
                                    }
                                    else
                                    {
                                        nuevo.Estado = "EAP";
                                        panaderia.Cola.Enqueue(nuevo);
                                    }
                                    nuevo.id_recorrido = nuevo.Recorrido.Dequeue();//saco de la cola
                                    break;
                                case 4://Carniceria-Panaderia-Gondola-Verduleria
                                    nuevo.Recorrido.Enqueue("Ca"); nuevo.Recorrido.Enqueue("P"); nuevo.Recorrido.Enqueue("G"); nuevo.Recorrido.Enqueue("V"); nuevo.Recorrido.Enqueue("C");//C de que va a caja
                                    nuevo.id_recorrido = nuevo.Recorrido.Dequeue();//saco de la cola
                                    if (carniceria.Estado == "L")
                                    {
                                        nuevo.Estado = "SAC";
                                        nuevo.cantArt = 1;

                                        carniceria.Estado = "Oc";
                                        double rnd = Math.Round(RND.NextDouble(), 4);
                                        dr["*FinAtencion Carniceria* RND"] = rnd;
                                        carniceria.tiempoAtencion = super.generarUniformeCarniceria(rnd);                  // 3' 1'' = 0.05027 (HORARIO -> DECIMAL)  
                                        dr["FinTiempo AtencionC"] = super.Reloj + carniceria.tiempoAtencion;
                                        dr["Tiempo AtencionC"] = carniceria.tiempoAtencion;
                                        carniceria.Cola.Enqueue(nuevo);//Si esta Siendo Atendido tambien lo meto en cola para no perderlo
                                    }
                                    else
                                    {
                                        nuevo.Estado = "EAC";
                                        carniceria.Cola.Enqueue(nuevo);
                                    }
                                    
                                    break;
                                case 5://Gondola
                                    nuevo.Recorrido.Enqueue("G"); nuevo.Recorrido.Enqueue("C");
                                    if (gondola.Estado == "L")
                                    {
                                        nuevo.Estado = "SAG";
                                        double rnd = Math.Round(RND.NextDouble(), 4);
                                        nuevo.cantArt = gondola.generarCantArticulos(rnd);
                                        
                                        gondola.Estado = "Oc";          // 1' = 0.0166 (HORARIO -> DECIMAL)
                                        dr["*FinAtencion Gondola* RND"] = rnd;
                                        dr["Cant Articulos Gondola"] = nuevo.cantArt;
                                        dr["FinTiempo Atencion Gondola"] = gondola.getFinAtencion(nuevo.cantArt, super.Reloj);
                                        gondola.Cola.Enqueue(nuevo);//Si esta Siendo Atendido tambien lo meto en cola para no perderlo
                                    }
                                    else
                                    {
                                        nuevo.Estado = "EAG";
                                        gondola.Cola.Enqueue(nuevo);
                                    }
                                    nuevo.id_recorrido = nuevo.Recorrido.Dequeue();//saco el recorrido G del recorrido total del cliente
                                    break;
                                default:
                                    MessageBox.Show("Hubo un error en el switch de recorrido.");
                                    break;
                            }

                            // ACA VA LA OTRA PARTE QUE TAMBIEN ESTABA AL FINAL DE CADA CASE, QUE TAMBIEN ES INDEPENDIENTE DEL RECORRIDO
                            //Clientes
                            dt.Columns.Add("*C " + i + "* Estado", typeof(string));
                            dt.Columns.Add("*C " + i + "* Actual seccion", typeof(string));
                            dt.Columns.Add("Cont CantArticulos C " + i, typeof(Int32));

                            dr["*C " + i + "* Estado"] = nuevo.Estado;
                            dr["*C " + i + "* Actual seccion"] = nuevo.id_recorrido;
                            dr["Cont CantArticulos C " + i] = nuevo.cantArt;


                            //Agregar en alguna Cola
                            //verduleria.Cola.Enqueue(nuevo);

                            //Escribo en la Fila los tiempos que no se cambiaron
                            //Los fin atencion de los eventos se setean al principio con -1 , Si nunca se escribió un fin Atencion de un server no debe poner nada en Fin tiempo de la fila siguiente
                            if (carniceria.finAtencion != -1) { dr["FinTiempo AtencionC"] = carniceria.finAtencion; }
                            if (verduleria.finAtencion != -1) { dr["FinTiempo AtencionV"] = verduleria.finAtencion; }
                            if (panaderia.finAtencion != -1) { dr["*FinAtencion Panadería*"] = panaderia.finAtencion; }
                            if (gondola.finAtencion != -1) { dr["FinTiempo Atencion Gondola"] = gondola.finAtencion; }
                            if (cajaR.finAtencion != -1) { dr["FinTiempo AtencíonCR"] = cajaR.finAtencion; }
                            if (caja2.finAtencion != -1) { dr["FinTiempo AtencionC2"] = caja2.finAtencion; }
                            if (caja3.finAtencion != -1) { dr["\nFinTiempoAC3"] = caja3.finAtencion; }
                            //Servers

                            dr["*Verduleria* Estado"] = verduleria.Estado;
                            dr["*Carniceria* Estado"] = carniceria.Estado;
                            dr["*Panaderia* Estado"] = panaderia.Estado;
                            dr["*Gondola* Estado"] = gondola.Estado;
                            dr["*CajaRapida* Estado"] = cajaR.Estado;
                            dr["*Caja2* Estado"] = caja2.Estado;
                            dr["*Caja3* Estado"] = caja3.Estado;

                           
                            dr["\nColaC"] = carniceria.Cola.Count;
                            dr["\nColaP"] = panaderia.Cola.Count;
                            dr["\nColaG"] = gondola.Cola.Count;
                            dr["\nColaCR"] = cajaR.Cola.Count;
                            dr["\nColaC2"] = caja2.Cola.Count;
                            dr["\nColaC3"] = caja3.Cola.Count;                            
                            break;
                        case "FAP"://Fin Atencion Panaderia
                            dr["Evento"] = "Fin atenc Panaderia";
                            
                            break;
                        case "FAC"://Fin Atencion Carniceria
                            {
                                dr["Evento"] = "Fin atenc Carniceria";

                                break;
                            }
                        case "FAG"://Fin Atencion Gondola
                            {
                                dr["Evento"] = "Fin atenc Gondola";

                                break;
                            }
                        case "FAV"://Fin Atencion Verduleria
                            {
                                dr["Evento"] = "Fin atenc Verduleria";
                                super.Reloj = verduleria.finAtencion;
                                dr["Reloj"] = super.Reloj;
                                dr["ProxLleg"] = super.ProximaLlegadaCliente;

                                if (verduleria.Cola.Count != 0)
                                {
                                    Cliente elQueAcabaDeSerAtendido = verduleria.Cola.Dequeue();

                                    if (elQueAcabaDeSerAtendido.Recorrido.ToString() == "Ca") { carniceria.Cola.Enqueue(elQueAcabaDeSerAtendido); }
                                    if (elQueAcabaDeSerAtendido.Recorrido.ToString() == "G") { gondola.Cola.Enqueue(elQueAcabaDeSerAtendido); }
                                    if (elQueAcabaDeSerAtendido.Recorrido.ToString() == "P") { panaderia.Cola.Enqueue(elQueAcabaDeSerAtendido); }
                                    if (elQueAcabaDeSerAtendido.Recorrido.ToString() == "V") { verduleria.Cola.Enqueue(elQueAcabaDeSerAtendido); }
                                    if (elQueAcabaDeSerAtendido.Recorrido.ToString() == "C") { caja2.Cola.Enqueue(elQueAcabaDeSerAtendido); }
                                    
                                    double rnd = Math.Round(RND.NextDouble(), 4);
                                    dr["*FinAtencion Verduleria* RND"] = rnd;
                                    verduleria.tiempoAtencion = super.generarPoisson(-2, rnd);                  // 2' = 0.0333 (HORARIO -> DECIMAL)          
                                    dr["Tiempo AtencionV"] = verduleria.tiempoAtencion;
                                    verduleria.finAtencion = verduleria.tiempoAtencion + super.Reloj;             // ESTO HACE QUE SE CLAVE POR ESO LO COMENTE
                                    dr["FinTiempo AtencionV"] = verduleria.finAtencion;
                                    dr["*Verduleria* Estado"] = verduleria.Estado;
                                    dr["*Carniceria* Estado"] = carniceria.Estado;
                                    dr["*Panaderia* Estado"] = panaderia.Estado;
                                    dr["*Gondola* Estado"] = gondola.Estado;
                                    dr["*CajaRapida* Estado"] = cajaR.Estado;
                                    dr["*Caja2* Estado"] = caja2.Estado;
                                    dr["*Caja3* Estado"] = caja3.Estado;

                                    dr["\nColaV"] = verduleria.Cola.Count;
                                    dr["\nColaC"] = carniceria.Cola.Count;
                                    dr["\nColaP"] = panaderia.Cola.Count;
                                    dr["\nColaG"] = gondola.Cola.Count;
                                    dr["\nColaCR"] = cajaR.Cola.Count;
                                    dr["\nColaC2"] = caja2.Cola.Count;
                                    dr["\nColaC3"] = caja3.Cola.Count;

                                }
                                else {

                                    verduleria.finAtencion = -1;             
                                    
                                    verduleria.Estado = "L";
                                    dr["*Verduleria* Estado"] = verduleria.Estado;
                                    dr["*Carniceria* Estado"] = carniceria.Estado;
                                    dr["*Panaderia* Estado"] = panaderia.Estado;
                                    dr["*Gondola* Estado"] = gondola.Estado;
                                    dr["*CajaRapida* Estado"] = cajaR.Estado;
                                    dr["*Caja2* Estado"] = caja2.Estado;
                                    dr["*Caja3* Estado"] = caja3.Estado;

                                    dr["\nColaV"] = verduleria.Cola.Count;
                                    dr["\nColaC"] = carniceria.Cola.Count;
                                    dr["\nColaP"] = panaderia.Cola.Count;
                                    dr["\nColaG"] = gondola.Cola.Count;
                                    dr["\nColaCR"] = cajaR.Cola.Count;
                                    dr["\nColaC2"] = caja2.Cola.Count;
                                    dr["\nColaC3"] = caja3.Cola.Count;

                                }

                                break;
                               
                            }
                        case "FAC2"://Fin Atencion Caja 2 
                            {
                                dr["Evento"] = "Fin atenc Caja 2";
                                int nroCliente = caja2.atendido.id;
                                // BUSCAR LAS COLUMNAS CORRESPONDIENTES AL CLIENTE QUE SE VA
                                dt.Columns.Remove("*C " + nroCliente + "* Estado");
                                dt.Columns.Remove("*C " + nroCliente + "* Actual seccion");
                                dt.Columns.Remove("Cont CantArticulos C " + nroCliente);
                                // SACO AL CLIENTE DE LA CAJA
                                caja2.atendido = null;
                                break;
                            }
                        case "FAC3"://Fin Atencion Caja 3
                            {
                                dr["Evento"] = "Fin atenc Caja 3";
                                int nroCliente = caja3.atendido.id;
                                // BUSCAR LAS COLUMNAS CORRESPONDIENTES AL CLIENTE QUE SE VA
                                dt.Columns.Remove("*C " + nroCliente + "* Estado");
                                dt.Columns.Remove("*C " + nroCliente + "* Actual seccion");
                                dt.Columns.Remove("Cont CantArticulos C " + nroCliente);
                                // SACO AL CLIENTE DE LA CAJA
                                caja3.atendido = null;
                                break;
                            }
                        case "FACR"://Fin Atencion Caja R 
                            {
                                dr["Evento"] = "Fin atenc Caja R";

                                break;
                            }
                        default:
                            MessageBox.Show("Hubo un error en el switch de Eventos");
                            break;
                    }
                    dt.Rows.Add(dr);

                    // DeterminarEventoSiguiente() metodo que va a actualizar EventoSiguiente 
                    //if (this.DeterminarEventoSiguiente() == "FIN SIMULACION") { return; }
                    super.EventoSiguiente = DeterminarEventoSiguiente();
                }
                this.dgv_simulacion.DataSource = dt;
                this.colorColumnas();
            }
            this.lbl_resultado.Text = "   En total, llegaron " + contClientesEntrantes + " clientes al supermercado en " + txt_horas.Text + " horas.";
        }

       

        private String DeterminarEventoSiguiente()
        {
            List<double> menorTiempo = new List<double> { carniceria.finAtencion, verduleria.finAtencion, super.ProximaLlegadaCliente, gondola.finAtencion, caja2.finAtencion, panaderia.finAtencion, caja3.finAtencion, cajaR.finAtencion };
            //remuevo todos los -1
            menorTiempo.RemoveAll(item => item == -1);
            //busco el menor tiempo
            menorTiempo.Sort();
            double menorTiempoEvento = menorTiempo[0];

            // AGREGO ESTE IF PARA VERIFICAR QUE EL PROXIMO EVENTO DE MENOR TIEMPO NO SUPERE LA CANTIDAD DE HORAS A SIMULAR
            //if (menorTiempoEvento > horas) { return "FIN SIMULACION"; }

            if (menorTiempoEvento == super.ProximaLlegadaCliente) { return "LLC"; }
            if (menorTiempoEvento == carniceria.finAtencion) { return "FAC"; }
            if (menorTiempoEvento == verduleria.finAtencion) { return "FAV"; }
            if (menorTiempoEvento == panaderia.finAtencion) { return "FAP"; }
            if (menorTiempoEvento == gondola.finAtencion) { return "FAG"; }
            if (menorTiempoEvento == cajaR.finAtencion) { return "FACR"; }
            if (menorTiempoEvento == caja2.finAtencion) { return "FAC2"; }

            return "FAC3";
        }        

        //private void agregarColumnaNuevoCliente()
        //{
        //    int idCliente = 0;
        //    idCliente = idCliente + 1;
        //    int i = idCliente;

        //    //Clientes
        //    dt.Columns.Add("*C " + i + "* Estado", typeof(string));
        //    // dt.Columns.Add("Actual seccion C " + i, typeof(Int32));
        //    dt.Columns.Add("*C " + i + "* Actual seccion", typeof(Int32));
        //    dt.Columns.Add("Cont CantArticulos C " + i, typeof(Int32));
        //}        

        // -----------------INTERFAZ------------------//
        private int horas, desde, hasta;

        private void PantallaSimulacion_Load(object sender, EventArgs e)
        {
            lbl_resultado.Text = "";

            // valores por defecto
            txt_horas.Text = "30";
            txt_desde.Text = "0";
            txt_hasta.Text = "30";
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

