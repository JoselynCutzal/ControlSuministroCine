using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace ControlSuministroCine
{
    public partial class frmOrdenes : Form
    {
        private string conexion = @"Data Source=.\SQLEXPRESS;Initial Catalog=ControlDeSuministroCine;Integrated Security=True";

        DataGridView dgv;
        Label lblTitulo;
        Button btnVerTodas, btnPendientes, btnEntregadas, btnPorSucursal;

        public frmOrdenes()
        {
            ConfigurarVentana();
            CargarTodosEnvios();
        }

        void ConfigurarVentana()
        {
            Text = "GESTIÓN DE ENVÍOS DE MERCANCÍA";
            Size = new Size(1000, 650);
            StartPosition = FormStartPosition.CenterScreen;

            lblTitulo = new Label
            {
                Text = "Listado de Envíos",
                Font = new Font("Arial", 12, FontStyle.Bold),
                Location = new Point(20, 10),
                Size = new Size(700, 30)
            };
            Controls.Add(lblTitulo);

            btnVerTodas = new Button { Text = "Ver Todos", Location = new Point(20, 50), Size = new Size(150, 40), BackColor = Color.SteelBlue, ForeColor = Color.White };
            btnPendientes = new Button { Text = "Pendientes", Location = new Point(190, 50), Size = new Size(150, 40), BackColor = Color.Orange, ForeColor = Color.White };
            btnEntregadas = new Button { Text = "Entregados", Location = new Point(360, 50), Size = new Size(150, 40), BackColor = Color.Green, ForeColor = Color.White };
            btnPorSucursal = new Button { Text = "Por Sucursal", Location = new Point(530, 50), Size = new Size(150, 40), BackColor = Color.Purple, ForeColor = Color.White };

            btnVerTodas.Click += (s, e) => CargarTodosEnvios();
            btnPendientes.Click += (s, e) => CargarPorEstado("Pendiente");
            btnEntregadas.Click += (s, e) => CargarPorEstado("Entregado");
            btnPorSucursal.Click += (s, e) => CargarAgrupadasPorSucursal();

            Controls.Add(btnVerTodas);
            Controls.Add(btnPendientes);
            Controls.Add(btnEntregadas);
            Controls.Add(btnPorSucursal);

            dgv = new DataGridView
            {
                Location = new Point(20, 110),
                Size = new Size(940, 500),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true
            };
            Controls.Add(dgv);
        }

        void CargarTodosEnvios()
        {
            lblTitulo.Text = "📋 Todos los Envíos de Mercancía";
            string sql = @"
                SELECT e.id_envio, e.id_vehiculo, e.fecha_salida, 
                       e.fecha_llegada_estimada, e.fecha_llegada_real,
                       e.estado_envio, s.nombre_sucursal AS destino
                FROM Envio_Mercancia e
                LEFT JOIN Sucursal_Cine s ON e.id_sucursal_destino = s.id_sucursal
                ORDER BY e.fecha_salida DESC";
            EjecutarConsulta(sql);
        }

        void CargarPorEstado(string estado)
        {
            lblTitulo.Text = $"📋 Envíos — Estado: {estado}";
            string sql = @"
                SELECT e.id_envio, e.id_vehiculo, e.fecha_salida, 
                       e.fecha_llegada_estimada, e.fecha_llegada_real,
                       e.estado_envio, s.nombre_sucursal AS destino
                FROM Envio_Mercancia e
                LEFT JOIN Sucursal_Cine s ON e.id_sucursal_destino = s.id_sucursal
                WHERE e.estado_envio = @estado
                ORDER BY e.fecha_salida DESC";

            try
            {
                using (SqlConnection cn = new SqlConnection(conexion))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(sql, cn);
                    cmd.Parameters.AddWithValue("@estado", estado);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgv.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Envíos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void CargarAgrupadasPorSucursal()
        {
            lblTitulo.Text = "📊 Envíos por Sucursal de Destino";
            string sql = @"
                SELECT s.nombre_sucursal AS sucursal_destino,
                       COUNT(e.id_envio) AS total_envios,
                       SUM(CASE WHEN e.estado_envio = 'Pendiente' THEN 1 ELSE 0 END) AS pendientes,
                       SUM(CASE WHEN e.estado_envio = 'Entregado' THEN 1 ELSE 0 END) AS entregados
                FROM Sucursal_Cine s
                LEFT JOIN Envio_Mercancia e ON s.id_sucursal = e.id_sucursal_destino
                GROUP BY s.nombre_sucursal
                ORDER BY total_envios DESC";
            EjecutarConsulta(sql);
        }

        void EjecutarConsulta(string sql)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(conexion))
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(sql, cn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgv.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}