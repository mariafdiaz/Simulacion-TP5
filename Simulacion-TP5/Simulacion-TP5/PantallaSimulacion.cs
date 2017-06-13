using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simulacion_TP5
{
    public partial class PantallaSimulacion : Form
    {

        private DataTable dt;
        private Supermercado super;
        private static Random RND = new Random();
        


        public PantallaSimulacion()
        {
            InitializeComponent();
        }

        private void PantallaSimulacion_Load(object sender, EventArgs e)
        {

        }
    }
}
