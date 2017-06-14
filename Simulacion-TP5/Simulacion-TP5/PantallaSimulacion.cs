

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
        private Caja1 caja1 = new Caja1();
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
                MessageBox.Show(super.EventoSiguiente + " " + super.ProximaLlegadaCliente + " " + super.ProximaLlegadaCliente);
                while (super.Reloj < horas)
                {

                    switch (super.EventoSiguiente) //Que tipo de Evento es?
                    {
                        case "LLC"://llegada Cliente 


                            DataRow dr = dt.NewRow();

                            super.Reloj = super.ProximaLlegadaCliente;
                            super.AleatorioLlegadaCliente = super.generarAleatorio();
                            super.LlegadaCliente = super.generarPoisson(0.5, super.AleatorioLlegadaCliente);
                            super.ProximaLlegadaCliente = super.Reloj + super.LlegadaCliente;
                            dr["Reloj"] = super.Reloj;
                            dr["*Llegada cliente* RND"] = super.AleatorioLlegadaCliente;
                            dr["TiempoLleg"] = super.LlegadaCliente;
                            dr["ProxLleg"] = super.ProximaLlegadaCliente;
                            MessageBox.Show(super.EventoSiguiente + " " + super.ProximaLlegadaCliente + " " + super.ProximaLlegadaCliente);

                            dt.Rows.Add(dr);
                            break;


                        case "FAP"://Fin Atencion Panaderia

                            break;


                        case "FAC"://Fin Atencion Carniceria
                            {
                                break;
                            }

                        case "FAG"://Fin Atencion Gondola
                            {
                                break;
                            }
                        case "FAV"://Fin Atencion Verduleria
                            {
                                break;
                            }
                        case "FAC1"://Fin Atencion Caja 1 
                            {
                                break;
                            }
                        case "FAC2"://Fin Atencion Caja 2
                            {
                                break;
                            }
                        case "FACR"://Fin Atencion Caja R 
                            {
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
        
        public void proxRecorrido()  // crea la columna del cliente que vino
        {
            DataRow dr = dt.NewRow();
            super.Reloj = super.ProximaLlegadaCliente;
            super.AleatorioLlegadaCliente = super.generarAleatorio();
            //super.LlegadaCliente = generarPoisson(0.5, super.AleatorioLlegadaCliente);
            super.ProximaLlegadaCliente = super.Reloj + super.LlegadaCliente;

            dr["Reloj"] = super.Reloj;
            dr["*Llegada cliente* RND"] = super.AleatorioLlegadaCliente;
            dr["TiempoLleg"] = super.LlegadaCliente;
            dr["ProxLleg"] = super.ProximaLlegadaCliente;
            //dr["idRec"] = i;


            //Generar recorrido
            super.IDRecorrido = 3;
            dr["*Recorrido* RND"] = super.generarAleatorio();
            dr["Recorrido"] = "P"; 
            dr["idRec"] = super.IDRecorrido;

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
            // CAJA 3
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

            //COLUMNA LLEGADA CLIENTE//
            dr["Reloj"] = super.Reloj;
            dr["*Llegada cliente* RND"] = super.AleatorioLlegadaCliente;
            dr["TiempoLleg"] = super.LlegadaCliente;
            dr["ProxLleg"] = super.ProximaLlegadaCliente;
            
            
            
            // COLUMNA RECORRIDO // 
            super.IDRecorrido = super.generarAleatorio();
            //string cadena = super.generarCadenaRecorrido(super.generarIdRecorrido(super.IDRecorrido));
            dr["*Recorrido* RND"] = super.IDRecorrido;
           // dr["Recorrido"] = cadena;
            //dr["idRec"] = ;
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
        private void PantallaSimulacion_Load(object sender, EventArgs e)
        {
            lbl_resultado.Text = "";

            // valores por defecto
            txt_horas.Text = "30";
            txt_desde.Text = "0";
            txt_hasta.Text = "30";
        }

    }
}

