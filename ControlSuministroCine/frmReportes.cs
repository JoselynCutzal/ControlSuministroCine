using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace ControlSuministroCine
{
    public partial class frmReportes : Form
    {
        // ✅ Pega aquí la MISMA conexión que ya funciona en tu proyecto
        private string conexion = @"Data Source=.\SQLEXPRESS;Initial Catalog=ControlDeSuministroCine;Integrated Security=True";

        DataGridView dgv;
        Label lblTitulo;
        Button[] btnRep;

        public frmReportes()
        {
            ConfigurarVentana();
            CargarReporte(1);
        }

        void ConfigurarVentana()
        {
            Text = "REPORTES — CONTROL DE SUMINISTRO";
            Size = new Size(1000, 650);
            StartPosition = FormStartPosition.CenterScreen;

            lblTitulo = new Label
            {
                Text = "",
                Font = new Font("Arial", 12, FontStyle.Bold),
                Location = new Point(200, 10),
                Size = new Size(750, 30)
            };
            Controls.Add(lblTitulo);

            dgv = new DataGridView
            {
                Location = new Point(200, 50),
                Size = new Size(760, 550),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true
            };
            Controls.Add(dgv);

            btnRep = new Button[10];
            string[] nombres = {
                "1. Stock Crítico",
                "2. Órdenes de Suministro",
                "3. Compras Realizadas",
                "4. Proveedores",
                "5. Por Sucursal",
                "6. Estado Inventario",
                "7. Historial Entregas",
                "8. Métodos de Pago",
                "9. Empleados por Sucursal",
                "10. Productos Mayor Stock"
            };

            for (int i = 0; i < 10; i++)
            {
                btnRep[i] = new Button
                {
                    Text = nombres[i],
                    Location = new Point(10, 50 + i * 55),
                    Size = new Size(170, 45),
                    BackColor = Color.SteelBlue,
                    ForeColor = Color.White
                };
                int num = i + 1;
                btnRep[i].Click += (s, e) => CargarReporte(num);
                Controls.Add(btnRep[i]);
            }
        }

        void CargarReporte(int num)
        {
            string sql = "", titulo = "";

            switch (num)
            {
                case 1:
                    titulo = "Reporte 1 — Stock Crítico";
                    sql = @"
                        SELECT i.id_sucursal, s.nombre_sucursal, s.direccion,
                               i.cantidad_stock, i.stock_minimo,
                               (i.stock_minimo - i.cantidad_stock) AS cantidad_a_reponer
                        FROM Inventario_Sucursal i
                        INNER JOIN Sucursal_Cine s ON i.id_sucursal = s.id_sucursal
                        WHERE i.cantidad_stock < i.stock_minimo
                        ORDER BY s.nombre_sucursal";
                    break;

                case 2:
                    titulo = "Reporte 2 — Órdenes de Suministro";
                    sql = @"
                        SELECT id_orden, fecha_orden, estado_orden, id_sucursal
                        FROM Orden_Suministro
                        ORDER BY fecha_orden DESC";
                    break;

                case 3:
                    titulo = "Reporte 3 — Compras Realizadas";
                    sql = @"
                        SELECT c.id_compra, c.fecha_compra, c.estado_compra,
                               s.nombre_sucursal, mp.nombre_metodo
                        FROM Compras c
                        INNER JOIN Sucursal_Cine s ON c.id_sucursal = s.id_sucursal
                        INNER JOIN Metodo_Pago mp ON c.id_metodo_pago = mp.id_metodo_pago
                        ORDER BY c.fecha_compra DESC";
                    break;

                case 4:
                    titulo = "Reporte 4 — Proveedores Activos";
                    sql = @"
                        SELECT 'EQUIPO' AS tipo_proveedor, id_proveedor_eq AS id, nombre_proveedor AS nombre
                        FROM Proveedor_Equipos WHERE estado = 'Activo'
                        UNION ALL
                        SELECT 'SERVICIO', id_proveedor_serv, nombre_empresa
                        FROM Proveedor_Servicios WHERE estado = 'Activo'
                        ORDER BY nombre";
                    break;

                case 5:
                    titulo = "Reporte 5 — Compras por Sucursal";
                    sql = @"
                        SELECT s.id_sucursal, s.nombre_sucursal,
                               COUNT(DISTINCT c.id_compra) AS total_compras
                        FROM Sucursal_Cine s
                        LEFT JOIN Compras c ON s.id_sucursal = c.id_sucursal
                        GROUP BY s.id_sucursal, s.nombre_sucursal
                        ORDER BY s.nombre_sucursal";
                    break;

                case 6:
                    titulo = "Reporte 6 — Estado de Inventario";
                    sql = @"
                        SELECT s.nombre_sucursal, SUM(i.cantidad_stock) AS total_existencias
                        FROM Inventario_Sucursal i
                        INNER JOIN Sucursal_Cine s ON i.id_sucursal = s.id_sucursal
                        GROUP BY s.nombre_sucursal
                        ORDER BY s.nombre_sucursal";
                    break;

                case 7:
                    titulo = "Reporte 7 — Historial de Entregas";
                    sql = @"
                        SELECT id_orden, fecha_orden, fecha_entrega, estado_orden
                        FROM Orden_Suministro
                        WHERE fecha_entrega IS NOT NULL
                        ORDER BY fecha_entrega DESC";
                    break;

                case 8:
                    titulo = "Reporte 8 — Uso de Métodos de Pago";
                    sql = @"
                        SELECT mp.nombre_metodo, COUNT(c.id_compra) AS veces_usado
                        FROM Metodo_Pago mp
                        LEFT JOIN Compras c ON mp.id_metodo_pago = c.id_metodo_pago
                        GROUP BY mp.nombre_metodo
                        ORDER BY veces_usado DESC";
                    break;

                case 9:
                    titulo = "Reporte 9 — Empleados por Sucursal";
                    sql = @"
                        SELECT s.nombre_sucursal, COUNT(e.id_empleado) AS total_empleados
                        FROM Sucursal_Cine s
                        LEFT JOIN Empleado e ON s.id_sucursal = e.id_sucursal
                        GROUP BY s.nombre_sucursal
                        ORDER BY total_empleados DESC";
                    break;

                case 10:
                    titulo = "Reporte 10 — Productos con Mayor Stock";
                    sql = @"
                        SELECT p.nombre_producto, c.nombre_categoria,
                               SUM(i.cantidad_stock) AS existencias_totales
                        FROM Inventario_Sucursal i
                        INNER JOIN Producto p ON i.id_producto = p.id_producto
                        INNER JOIN Categoria_Alimento c ON p.id_categoria = c.id_categoria
                        GROUP BY p.nombre_producto, c.nombre_categoria
                        ORDER BY existencias_totales DESC";
                    break;
            }

            try
            {
                using (SqlConnection cn = new SqlConnection(conexion))
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(sql, cn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgv.DataSource = dt;
                    lblTitulo.Text = titulo;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Reporte", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}