using System;
using System.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ControlDeSuministroCine
{
    public partial class Compras : Form
    {
        string conexion = "Data Source=JOSELYN\\SQLEXPRESS;Initial Catalog=ControlDeSuministroCine;Integrated Security=True";

        public Compras()
        {
            InitializeComponent();
        }

        private void Compras_Load(object sender, EventArgs e)
        {
            txtIdCompra.Text = "(Auto)";
            cmbEstado.SelectedIndex = 0;
            CargarCompras();
        }

        private void CargarCompras()
        {
            using (SqlConnection cn = new SqlConnection(conexion))
            {
                try
                {
                    cn.Open();
                    string sql = @"SELECT id_compra, fecha_compra, id_sucursal, 
                                          id_proveedor_eq, id_proveedor_serv, 
                                          id_metodo_pago, estado_compra
                                   FROM Compras";
                    SqlDataAdapter da = new SqlDataAdapter(sql, cn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvCompras.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error cargando: " + ex.Message);
                }
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtIdSucursal.Clear();
            txtIdProvEq.Clear();
            txtIdProvServ.Clear();
            txtIdMetodoPago.Clear();
            dtpFecha.Value = DateTime.Now;
            cmbEstado.SelectedIndex = 0;
            txtIdSucursal.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdSucursal.Text))
            {
                MessageBox.Show("Escribe el ID de la Sucursal");
                txtIdSucursal.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtIdProvEq.Text))
            {
                MessageBox.Show("Escribe el ID del Proveedor de Equipo");
                txtIdProvEq.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtIdProvServ.Text))
            {
                MessageBox.Show("Escribe el ID del Proveedor de Servicio");
                txtIdProvServ.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtIdMetodoPago.Text))
            {
                MessageBox.Show("Escribe el ID del Método de Pago");
                txtIdMetodoPago.Focus();
                return;
            }

            using (SqlConnection cn = new SqlConnection(conexion))
            {
                try
                {
                    cn.Open();
                    string sql = @"INSERT INTO Compras 
                        (fecha_compra, id_sucursal, id_proveedor_eq, 
                         id_proveedor_serv, id_metodo_pago, estado_compra)
                        VALUES (@fecha, @sucursal, @prov_eq, @prov_serv, @pago, @estado)";

                    SqlCommand cmd = new SqlCommand(sql, cn);
                    cmd.Parameters.AddWithValue("@fecha", dtpFecha.Value.Date);
                    cmd.Parameters.AddWithValue("@sucursal", txtIdSucursal.Text);
                    cmd.Parameters.AddWithValue("@prov_eq", txtIdProvEq.Text);
                    cmd.Parameters.AddWithValue("@prov_serv", txtIdProvServ.Text);
                    cmd.Parameters.AddWithValue("@pago", txtIdMetodoPago.Text);
                    cmd.Parameters.AddWithValue("@estado", cmbEstado.SelectedItem.ToString());

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("✅ Compra guardada correctamente");
                    CargarCompras();
                    btnNuevo_Click(null, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Error: " + ex.Message);
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}