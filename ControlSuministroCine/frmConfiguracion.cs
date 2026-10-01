using System;
using System.Drawing;
using System.Windows.Forms;

namespace ControlSuministroCine
{
    public partial class frmConfiguracion : Form
    {
        public frmConfiguracion()
        {
            ConfigurarVentana();
        }

        void ConfigurarVentana()
        {
            Text = "CONFIGURACIÓN DEL SISTEMA";
            Size = new Size(600, 500);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.WhiteSmoke;

            Label lblTitulo = new Label
            {
                Text = "Parámetros del Sistema",
                Font = new Font("Arial", 14, FontStyle.Bold),
                Location = new Point(20, 20),
                Size = new Size(400, 30)
            };
            Controls.Add(lblTitulo);

            // Umbral de stock mínimo
            Label lblStock = new Label { Text = "Umbral de Stock Crítico:", Location = new Point(30, 80), Size = new Size(250, 25) };
            TextBox txtStock = new TextBox { Name = "txtStock", Text = "10", Location = new Point(200, 80), Size = new Size(100, 25) };
            Controls.Add(lblStock); Controls.Add(txtStock);

            // Estado de envío predeterminado
            Label lblEstado = new Label { Text = "Estado Inicial de Envíos:", Location = new Point(30, 130), Size = new Size(250, 25) };
            ComboBox cboEstado = new ComboBox { Name = "cboEstado", Location = new Point(200, 130), Size = new Size(200, 25) };
            cboEstado.Items.AddRange(new string[] { "Pendiente", "En Camino", "Entregado" });
            cboEstado.SelectedIndex = 0;
            Controls.Add(lblEstado); Controls.Add(cboEstado);

            // Sucursal activa
            Label lblSucursal = new Label { Text = "Sucursal Actual:", Location = new Point(30, 180), Size = new Size(250, 25) };
            ComboBox cboSucursal = new ComboBox { Name = "cboSucursal", Location = new Point(200, 180), Size = new Size(200, 25) };
            cboSucursal.Items.AddRange(new string[] { "Sucursal Principal", "Sucursal Centro", "Sucursal Norte" });
            cboSucursal.SelectedIndex = 0;
            Controls.Add(lblSucursal); Controls.Add(cboSucursal);

            // Conexión BD
            Label lblConexion = new Label { Text = "Base de Datos:", Location = new Point(30, 230), Size = new Size(250, 25) };
            TextBox txtConexion = new TextBox { Name = "txtConexion", Text = @".\SQLEXPRESS", Location = new Point(200, 230), Size = new Size(300, 25), ReadOnly = true };
            Controls.Add(lblConexion); Controls.Add(txtConexion);

            // Botones
            Button btnGuardar = new Button { Text = "Guardar", Location = new Point(100, 350), Size = new Size(120, 40), BackColor = Color.Green, ForeColor = Color.White };
            Button btnCancelar = new Button { Text = "Cancelar", Location = new Point(260, 350), Size = new Size(120, 40), BackColor = Color.Gray, ForeColor = Color.White };

            btnGuardar.Click += (s, e) =>
            {
                MessageBox.Show("Configuración guardada correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            };

            btnCancelar.Click += (s, e) => { this.Close(); };

            Controls.Add(btnGuardar); Controls.Add(btnCancelar);
        }
    }
}