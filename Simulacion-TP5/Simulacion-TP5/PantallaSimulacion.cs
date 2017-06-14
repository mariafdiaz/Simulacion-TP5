

using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Simulacion_TP5.Objetos;

namespace Simulacion_TP5
{
    public partial class PantallaSimulacion : Form
    {
        //ATRIBUTOS//
        private DataTable dt = new DataTable();
        private Supermercado super = new Supermercado();
        private static Random RND = new Random();
        //Instaciación de clases
        private Verduleria verduleria = new Verduleria();
        private Carniceria carniceria = new Carniceria();
        private Panaderia panaderia = new Panaderia();
        private Gondola gondola = new Gondola();
        private Caja3 caja3 = new Caja3();
        private Caja2 caja2 = new Caja2();
        private CajaR cajaR = new CajaR();

        


        //FUNCIONALIDAD//
        private void btn_generar_Click_1(object sender, EventArgs e)
        {
            if (validar_campos())
            {
                inicializarColumnas(); //Todas las columnas menos los clientes           
                iniciarPrimeraFila();//iniciarPrimeraFila
               
                agregarColumnaNuevoCliente(); //empezar a funcionar

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

                            dr1["ContClientes Atendidos"] = super.CantClientesAtendidos;


                            //Agregar en alguna Cola
                            Cliente nuevo = new Cliente();
                            //verduleria.Cola.Enqueue(nuevo);



                            


                            dt.Rows.Add(dr1);
                            break;


                        case "FAP"://Fin Atencion Panaderia

                            DataRow dr2 = dt.NewRow();
                            dr2["Evento"] = "Fin atenc Panaderia";
                            dt.Rows.Add(dr2);
                            break;


                        case "FAC"://Fin Atencion Carniceria
                            {
                                DataRow dr3 = dt.NewRow();
                                dr3["Evento"] = "Fin atenc Carniceria";
                                dt.Rows.Add(dr3);
                                break;
                            }

                        case "FAG"://Fin Atencion Gondola
                            {
                                DataRow dr4 = dt.NewRow();
                                dr4["Evento"] = "Fin atenc Gondola";
                                dt.Rows.Add(dr4);
                                break;
                            }
                        case "FAV"://Fin Atencion Verduleria
                            {

                                DataRow dr5 = dt.NewRow();
                                dr5["Evento"] = "Fin atenc Verduleria";
                                dt.Rows.Add(dr5);

                                break;
                            }
                        case "FAC2"://Fin Atencion Caja 2 
                            {
                                DataRow dr6 = dt.NewRow();
                                dr6["Evento"] = "Fin atenc Caja 2";
                                dt.Rows.Add(dr6);
                                break;
                            }
                        case "FAC3"://Fin Atencion Caja 3
                            {
                                DataRow dr7 = dt.NewRow();
                                dr7["Evento"] = "Fin atenc Caja 3";
                                dt.Rows.Add(dr7);
                                break;
                            }
                        case "FACR"://Fin Atencion Caja R 
                            {
                                DataRow dr8 = dt.NewRow();
                                dr8["Evento"] = "Fin atenc Caja R";
                                dt.Rows.Add(dr8);
                                break;
                            }
                        default:

                            break;
                    }


                    // DeterminarEventoSiguiente() metodo que va a actualizar EventoSiguiente 


                }

                this.dgv_simulacion.DataSource = dt;
                this.colorColumnas();






            }



        }
        
        

        private void agregarColumnaNuevoCliente()
        {
            int idCliente = 0;
            idCliente = idCliente + 1;
            int i = idCliente;

            //Clientes
            dt.Columns.Add("*C " + i + "* Estado", typeof(string));
            // dt.Columns.Add("Actual IDRecorrido C " + i, typeof(Int32));
            dt.Columns.Add("*C " + i + "* Actual IDRecorrido", typeof(Int32));
            dt.Columns.Add("Cont CantArticulos C " + i, typeof(Int32));
        }



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
            Color llegCliente = Color.Aqua;
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

