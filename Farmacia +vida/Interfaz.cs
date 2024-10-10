using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Farmacia__vida
{
    public partial class Interfaz : Form
    {
        public Interfaz()
        {
            InitializeComponent();
            //centrar la ventana
            this.CenterToScreen();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //Metodo para crear reloj en tiempo real
            Timer timer = new Timer();
            timer.Tick += new EventHandler(Reloj_Tick);
            lblFecha.Text = DateTime.Now.ToString("dddd dd MMMM"); // Formato de fecha 

            timer.Interval = 1000;
            timer.Start();

        }
        private void Reloj_Tick(object sender, EventArgs e) {

            lblReloj.Text = DateTime.Now.ToString("HH:mm:ss");//formato de reloj
            
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            //Cerrar la aplicacion
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void lblReloj_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void btnAgregar_Cliente_Click(object sender, EventArgs e)
        {
            Pacientes pacientes = new Pacientes(); //ventana de clientes
            pacientes.Show();

        }

        private void lblReloj_Click_1(object sender, EventArgs e)
        {

        }

        private void lblFecha_Click(object sender, EventArgs e)
        {

        }
    }
}
