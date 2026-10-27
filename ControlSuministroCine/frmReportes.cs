using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace ControlSuministroCine
{
    public partial class frmReportes : Form
    {
        private string conexion = @"Data Source=JOSELYN\SQLEXPRESS;Initial Catalog=ControlDeSuministroCine;Integrated Security=True";
        private DataGridView dgvReporte;

        public frmReportes()
        {
            InitializeComponent();
            CrearInterfaz();
        }

        private void CrearInterfaz()
        {
            this.Text = "REPORTES — CONTROL DE SUMINISTRO";
            this.Size = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 240, 240);

            dgvReporte = new DataGridView
            {
                Location = new Point(220, 20),
                Size = new Size(640, 540),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true,
                BackgroundColor = Color.White,
                GridColor = Color.LightGray,
                BorderStyle = BorderStyle.FixedSingle
            };
            dgvReporte.EnableHeadersVisualStyles = false;
            dgvReporte.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 100, 180);
            dgvReporte.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvReporte.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            this.Controls.Add(dgvReporte);

            CrearBoton(1, "Stock Crítico", 20, ReporteStockCritico);
            CrearBoton(2, "Órdenes de Suministro", 70, ReporteOrdenesSuministro);
            CrearBoton(3, "Compras Realizadas", 120, ReporteComprasRealizadas);
            CrearBoton(4, "Proveedores", 170, ReporteProveedores);
            CrearBoton(5, "Por Sucursal", 220, ReportePorSucursal);
            CrearBoton(6, "Estado Inventario", 270, ReporteEstadoInventario);
            CrearBoton(7, "Historial Entregas", 320, ReporteHistorialEntregas);
            CrearBoton(8, "Métodos de Pago", 370, ReporteMetodosPago);
            CrearBoton(9, "Empleados por Sucursal", 420, ReporteEmpleadosSucursal);
            CrearBoton(10, "Productos Mayor Stock", 470, ReporteProductosMayorStock);
        }

        private void CrearBoton(int numero, string texto, int posicionY, EventHandler eventoClick)
        {
            Button btn = new Button
            {
                Text = $"{numero}. {texto}",
                Location = new Point(20, posicionY),
                Size = new Size(180, 40),
                BackColor = Color.FromArgb(60, 140, 200),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                FlatStyle = FlatStyle.Flat
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Click += eventoClick;
            this.Controls.Add(btn);
        }

        private void EjecutarReporte(string sql)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvReporte.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error en Reporte", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReporteStockCritico(object sender, EventArgs e)
        {
            string sql = @"
                SELECT 
                    s.nombre_sucursal AS Sucursal,
                    p.nombre_producto AS Producto,
                    i.stock_actual AS Stock,
                    i.stock_minimo AS [Stock Mínimo]
                FROM Inventario_Sucursal i
                JOIN Sucursal_Cine s ON i.id_sucursal = s.id_sucursal
                JOIN Producto_Alimenticio p ON i.id_producto_alim = p.id_producto_alim
                WHERE i.stock_actual <= i.stock_minimo
                ORDER BY i.stock_actual ASC";
            EjecutarReporte(sql);
        }

        private void ReporteOrdenesSuministro(object sender, EventArgs e)
        {
            string sql = @"
                SELECT 
                    id_compra AS [ID Orden],
                    fecha_compra AS [Fecha Solicitud],
                    fecha_entrega AS [Fecha Entrega],
                    estado_compra AS Estado,
                    observacion AS Observación
                FROM Compras
                ORDER BY fecha_compra DESC";
            EjecutarReporte(sql);
        }

        private void ReporteComprasRealizadas(object sender, EventArgs e)
        {
            string sql = @"
                SELECT 
                    c.id_compra AS [ID Compra],
                    s.nombre_sucursal AS Sucursal,
                    c.fecha_compra AS Fecha,
                    c.estado_compra AS Estado,
                    COUNT(dc.id_detalle_compra) AS [Total Artículos]
                FROM Compras c
                JOIN Sucursal_Cine s ON c.id_sucursal = s.id_sucursal
                LEFT JOIN Detalle_Compra dc ON c.id_compra = dc.id_compra
                GROUP BY c.id_compra, s.nombre_sucursal, c.fecha_compra, c.estado_compra
                ORDER BY c.fecha_compra DESC";
            EjecutarReporte(sql);
        }

        private void ReporteProveedores(object sender, EventArgs e)
        {
            string sql = @"
                SELECT 'Equipos' AS Tipo, id_proveedor_eq AS [ID], nombre_proveed_eq AS Nombre, 
                       marca_distribuye AS [Marca/Serivicio], contacto_ventas AS Contacto,
                       CASE estado_operacion WHEN 1 THEN 'Activo' ELSE 'Inactivo' END AS Estado
                FROM Proveedor_Equipos
                UNION ALL
                SELECT 'Servicios' AS Tipo, id_proveedor_serv AS [ID], nombre_empresa AS Nombre, 
                       tipo_servicio AS [Marca/Servicio], telefono_contacto AS Contacto,
                       CASE estado WHEN 1 THEN 'Activo' ELSE 'Inactivo' END AS Estado
                FROM Proveedor_Servicios
                ORDER BY Tipo, Nombre";
            EjecutarReporte(sql);
        }

        private void ReportePorSucursal(object sender, EventArgs e)
        {
            string sql = @"
                SELECT 
                    s.id_sucursal AS [ID],
                    s.nombre_sucursal AS Sucursal,
                    s.direccion AS Dirección,
                    s.telefono AS Teléfono,
                    COUNT(DISTINCT i.id_producto_alim) AS [Tipos de Productos],
                    SUM(i.stock_actual) AS [Total Artículos]
                FROM Sucursal_Cine s
                LEFT JOIN Inventario_Sucursal i ON s.id_sucursal = i.id_sucursal
                GROUP BY s.id_sucursal, s.nombre_sucursal, s.direccion, s.telefono
                ORDER BY s.nombre_sucursal";
            EjecutarReporte(sql);
        }

        private void ReporteEstadoInventario(object sender, EventArgs e)
        {
            string sql = @"
                SELECT 
                    s.nombre_sucursal AS Sucursal,
                    COUNT(CASE WHEN i.stock_actual <= i.stock_minimo THEN 1 END) AS [Stock Bajo],
                    COUNT(CASE WHEN i.stock_actual > i.stock_minimo THEN 1 END) AS [En Stock],
                    SUM(i.stock_actual) AS [Total Inventario]
                FROM Sucursal_Cine s
                LEFT JOIN Inventario_Sucursal i ON s.id_sucursal = i.id_sucursal
                GROUP BY s.nombre_sucursal
                ORDER BY s.nombre_sucursal";
            EjecutarReporte(sql);
        }

        private void ReporteHistorialEntregas(object sender, EventArgs e)
        {
            string sql = @"
                SELECT 
                    id_compra AS [ID Compra],
                    fecha_compra AS [Fecha Compra],
                    fecha_entrega AS [Fecha Entrega],
                    DATEDIFF(day, fecha_compra, fecha_entrega) AS [Días Transcurridos],
                    estado_compra AS Estado
                FROM Compras
                ORDER BY fecha_entrega DESC";
            EjecutarReporte(sql);
        }

        private void ReporteMetodosPago(object sender, EventArgs e)
        {
            string sql = @"
                SELECT 
                    tipo_pago AS [Método de Pago],
                    COUNT(*) AS [Total Compras]
                FROM Compras c
                JOIN Metodo_Pago mp ON c.id_metodo_pago = mp.id_metodo_pago
                GROUP BY tipo_pago
                ORDER BY [Total Compras] DESC";
            EjecutarReporte(sql);
        }

        private void ReporteEmpleadosSucursal(object sender, EventArgs e)
        {
            string sql = @"
                SELECT 
                    s.nombre_sucursal AS Sucursal,
                    COUNT(e.id_empleado) AS [Total Empleados]
                FROM Sucursal_Cine s
                LEFT JOIN Empleado e ON s.id_sucursal = e.id_sucursal
                GROUP BY s.nombre_sucursal
                ORDER BY s.nombre_sucursal";
            EjecutarReporte(sql);
        }

        private void ReporteProductosMayorStock(object sender, EventArgs e)
        {
            string sql = @"
                SELECT TOP 10
                    p.nombre_producto AS Producto,
                    SUM(i.stock_actual) AS [Stock Total],
                    p.unidad_medida AS Unidad,
                    p.precio_compra AS [Precio Compra]
                FROM Producto_Alimenticio p
                JOIN Inventario_Sucursal i ON p.id_producto_alim = i.id_producto_alim
                GROUP BY p.nombre_producto, p.unidad_medida, p.precio_compra
                ORDER BY [Stock Total] DESC";
            EjecutarReporte(sql);
        }
    }
}