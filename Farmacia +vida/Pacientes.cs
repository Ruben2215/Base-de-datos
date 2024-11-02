using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace Farmacia__vida
{
    public partial class Pacientes : Form

    {
        string SqlConection = "Server=farmaciaunach1.cpic0k0qeyvb.us-east-1.rds.amazonaws.com; Port=3306; Database=Farmacia_Unach; Uid = admin;  Pwd=tiajosseline;";

        public Pacientes()
        {
            InitializeComponent();
            this.CenterToScreen();
            // Validar campos de entrada de datos
            txtNombre.TextChanged += validarNombre;
            txtAp_Paterno.TextChanged += validarApellido;
            txtAp_Materno.TextChanged += validarApellido;
            txtTelefono.TextChanged += validarTelefono;
            txtCorreo.TextChanged += validarCorreo;


        }

        private void Pacientes_Load(object sender, EventArgs e)
        {

        }

        private void InsertarPacientes(string nombre, string ap_paterno, string ap_materno, string telefono, string correo, string direccion, DateTime fecha_nacimiento, string sexo, string alergias_paciente)
        {
            using (MySqlConnection connection = new MySqlConnection(SqlConection))
            {
                connection.Open();
                string insertQuery = "INSERT INTO Pacientes (nombres, apellido_paterno, apellido_materno, telefono,correo_electronico, direccion, fecha_nacimiento, sexo, alergias)" +
                                     " VALUES (@nombres, @apellido_paterno, @apellido_materno, @telefono,@correo_electronico, @direccion, @fecha_nacimiento, @sexo, @alergias)";
                using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@nombres", nombre);
                    command.Parameters.AddWithValue("@apellido_paterno", ap_paterno);
                    command.Parameters.AddWithValue("@telefono", telefono);
                   // command.Parameters.AddWithValue("@alergias", alergias_paciente);

                    if (string.IsNullOrEmpty(ap_materno))
                        command.Parameters.AddWithValue("@apellido_materno", DBNull.Value);
                    else
                        command.Parameters.AddWithValue("@apellido_materno", ap_materno);

                    if (string.IsNullOrEmpty(correo))
                        command.Parameters.AddWithValue("@correo_electronico", DBNull.Value);
                    else
                        command.Parameters.AddWithValue("@correo_electronico", correo);

                    if (string.IsNullOrEmpty(direccion))
                        command.Parameters.AddWithValue("@direccion", DBNull.Value);
                    else
                        command.Parameters.AddWithValue("@direccion", direccion);

                    if (fecha_nacimiento.Date == DateTime.Today)
                        command.Parameters.AddWithValue("@fecha_nacimiento", DBNull.Value);
                    else
                        command.Parameters.AddWithValue("@fecha_nacimiento", fecha_nacimiento);
                    command.Parameters.AddWithValue("@sexo", sexo);
                    if (string.IsNullOrEmpty(alergias_paciente))
                        command.Parameters.AddWithValue("@alergias", DBNull.Value);
                    else
                        command.Parameters.AddWithValue("@alergias", alergias_paciente);
                    
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }



        private void validarNombre(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.BackColor = SystemColors.Window;
                return;
            }

            if (!Metodos_de_validacion.EsTextoValido(textBox.Text))
            {
                MessageBox.Show("Ingresa un nombre válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.Clear();
            }
            else
            {
                checkBoxNombre.Checked = true;
                checkBoxNombre.Visible = true;
            }
        }

        private void validarApellido(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.BackColor = SystemColors.Window;
                return;
            }

            if (!Metodos_de_validacion.EsTextoValido(textBox.Text))
            {
                MessageBox.Show("Ingresa un apellido válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.Clear();
            }
            else
            {
                checkBoxApellido_P.Checked = true;
                checkBoxApellido_P.Visible = true;
            }
        }

        

        private void validarTelefono(object sender, EventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textbox.Text))
            {
                textbox.BackColor = SystemColors.Window;
                return;
            }
            if (textbox.Text.Length == 10 && Metodos_de_validacion.EsEnteroValido10Digitos(textbox.Text))
            {
                textbox.BackColor = Color.Green;
                checkBoxTelefono.Checked = true;
                checkBoxTelefono.Visible = true;
            }
            else if (!Metodos_de_validacion.EsEntero(textbox.Text))
            {
                textbox.BackColor = Color.Red;
                MessageBox.Show("El teléfono debe contener solo números.", "Error teléfono", MessageBoxButtons.OK);
                return;
            }
            else if (textbox.Text.Length > 10)
            {
                textbox.BackColor = Color.Red;
                MessageBox.Show("Ingrese un teléfono de 10 dígitos máximo", "Error teléfono", MessageBoxButtons.OK);
            }
            else if (textbox.Text.Length < 10)
            {
                textbox.BackColor = Color.Red;
            }
        }

        private void validarCorreo(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.BackColor = SystemColors.Window;
                return;
            }

            if (!Metodos_de_validacion.EsCorreoValido(textBox.Text))
            {
                MessageBox.Show("Ingresa un correo electrónico válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.Clear();
            }
        }

        

        private void validarFechaNacimiento(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (!Metodos_de_validacion.EsFechaValida(textBox.Text))
            {
                MessageBox.Show("Ingresa una fecha de nacimiento válida (DD/MM/YYYY)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregar_Paciente_Click(object sender, EventArgs e)

        {
            if (txtNombre.Text == "")//|| txtAp_Paterno.Text == "" || txtTelefono.Text == "")
            {
                txtNombre.BackColor = Color.Red;
                MessageBox.Show("Por favor, rellena el campo minimo requerido de \nNombre", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (txtAp_Paterno.Text == "")
            {
                txtAp_Paterno.BackColor = Color.Red;
                MessageBox.Show("Por favor, rellena los campos minimos requeridos de \nApellido paterno", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            else if (txtTelefono.Text == "")
            {
                txtTelefono.BackColor = Color.Red;
                MessageBox.Show("Por favor, rellena los campos minimos requeridos de \nTeléfono", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            else
            {
                txtTelefono.BackColor = Color.White; //Para limpiar el color de relleno
                txtAp_Paterno.BackColor = Color.White;
                txtNombre.BackColor = Color.White;


                string nombre = txtNombre.Text;
                string ap_paterno = txtAp_Paterno.Text;
                string ap_materno = txtAp_Materno.Text;
                string telefono = txtTelefono.Text;
                string correo = txtCorreo.Text;
                string direccion = txtDireccion.Text;
                DateTime fecha_nacimiento = dtpFecha_Nacimiento.Value; //Use un DateTime directamente en vez de un string por los 
                string alergias_paciente = txtAlergias.Text;


                String sexo = "N/E";
                if (rbtnG_Masculino.Checked)
                {
                    sexo = "M";
                }
                else if (rbtnG_Femenino.Checked)
                {
                    sexo = "F";
                }
                InsertarPacientes(nombre, ap_paterno, ap_materno, telefono, correo, direccion, fecha_nacimiento, sexo,
                    alergias_paciente);

                MessageBox.Show("Paciente agregado exitosamente", "Paciente agregado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                limpiarCampos();
            }
        }


        private void btnRegresar_Click(object sender, EventArgs e)
        {
            //Interfaz interfaz = new Interfaz(); //ventana de inicio
            // interfaz.Show();
            //cerrar formulario y abrir inicio
            //this.Close();
            this.Hide();
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxtAp_Paterno_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAp_Materno_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTelefono_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCorreo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDireccion_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtFecha_Nacimiento_TextChanged(object sender, EventArgs e)
        {

        }
        //limpiar todos los campos
        private void limpiarCampos()
        {


            txtNombre.Clear();
            txtAp_Paterno.Clear();
            txtAp_Materno.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
            txtDireccion.Clear();
            rbtnG_Femenino.Checked = false;
            rbtnG_Masculino.Checked = false;
            dtpFecha_Nacimiento.Value = DateTime.Now;
            checkBoxApellido_P.Checked = false;
            checkBoxNombre.Checked = false;
            checkBoxTelefono.Checked = false;
            txtAlergias.Clear();



        }

        private void checkBoxNombre_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtAlergias_TextChanged(object sender, EventArgs e)
        {

        }
    }
}