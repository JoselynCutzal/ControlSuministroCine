using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace ControlSuministroCine
{
    public partial class Inventario : Form
    {
        
        private string conexion = @"Data Source=.\SQLEXPRESS;Initial Catalog=ControlDeSuministroCine;Integrated Security=True";

        private ComboBox cmbSucursal;
        private ComboBox cmbProducto;
        private ComboBox cmbTipoAlmacen;
        private NumericUpDown numStockActual;
        private NumericUpDown numStockMinimo;
        private DateTimePicker dtpFecha;
        private DataGridView dgvInventario;

        private int idSeleccionado = 0;

        public Inventario()
        {
            CrearInterfaz();
            CargarSucursales();
            CargarProductos();
            CargarTiposAlmacen();
            CargarDatos();
        }

        private void CrearInterfaz()
        {
            this.Text = "Gestión de Inventario";
            this.Size = new Size(900, 620);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(22, 28, 42);
            this.ForeColor = Color.White;

            int y = 20;

            // Título
            Label lblTitulo = new Label
            {
                Text = "GESTIÓN DE INVENTARIO",
                Font = new Font("Arial", 18, FontStyle.Bold),
                Location = new Point(280, y),
                AutoSize = true
            };
            y += 50;

            // Sucursal
            Label lblSuc = new Label { Text = "Sucursal:", Location = new Point(30, y + 5), AutoSize = true };
            cmbSucursal = new ComboBox { Location = new Point(180, y), Width = 380, DropDownStyle = ComboBoxStyle.DropDownList };

            // Producto
            y += 45;
            Label lblProd = new Label { Text = "Producto:", Location = new Point(30, y + 5), AutoSize = true };
            cmbProducto = new ComboBox { Location = new Point(180, y), Width = 450, DropDownStyle = ComboBoxStyle.DropDownList };

            // Stock
            y += 45;
            Label lblAct = new Label { Text = "Stock Actual:", Location = new Point(30, y + 5), AutoSize = true };
            numStockActual = new NumericUpDown { Location = new Point(180, y), Width = 120, Minimum = 0, Maximum = 9999 };

            Label lblMin = new Label { Text = "Stock Mínimo:", Location = new Point(340, y + 5), AutoSize = true };
            numStockMinimo = new NumericUpDown { Location = new Point(460, y), Width = 120, Minimum = 0, Maximum = 9999 };

            // Tipo Almacenamiento y Fecha
            y += 45;
            Label lblTipo = new Label { Text = "Almacenamiento:", Location = new Point(30, y + 5), AutoSize = true };
            cmbTipoAlmacen = new ComboBox { Location = new Point(180, y), Width = 250, DropDownStyle = ComboBoxStyle.DropDownList };

            Label lblFec = new Label { Text = "Fecha:", Location = new Point(470, y + 5), AutoSize = true };
            dtpFecha = new DateTimePicker { Location = new Point(530, y), Width = 180, Format = DateTimePickerFormat.Short };
            dtpFecha.Value = DateTime.Now;

            // Botones
            y += 55;
            Button btnGuardar = new Button { Text = "💾 GUARDAR", Location = new Point(30, y), Width = 160, Height = 45, BackColor = Color.FromArgb(40, 167, 69), ForeColor = Color.White, Font = new Font("Arial", 10, FontStyle.Bold) };
            btnGuardar.Click += (s, e) => Guardar();

            Button btnEditar = new Button { Text = "✏️ EDITAR", Location = new Point(210, y), Width = 160, Height = 45, BackColor = Color.FromArgb(255, 193, 7), ForeColor = Color.Black, Font = new Font("Arial", 10, FontStyle.Bold) };
            btnEditar.Click += (s, e) => Editar();

            Button btnEliminar = new Button { Text = "🗑️ ELIMINAR", Location = new Point(390, y), Width = 160, Height = 45, BackColor = Color.FromArgb(220, 53, 69), ForeColor = Color.White, Font = new Font("Arial", 10, FontStyle.Bold) };
            btnEliminar.Click += (s, e) => Eliminar();

            Button btnLimpiar = new Button { Text = "🔄 LIMPIAR", Location = new Point(570, y), Width = 160, Height = 45, BackColor = Color.FromArgb(108, 117, 125), ForeColor = Color.White, Font = new Font("Arial", 10, FontStyle.Bold) };
            btnLimpiar.Click += (s, e) => Limpiar();

            // Tabla
            y += 70;
            Label lblTab = new Label { Text = "📋 Inventario Completo", Font = new Font("Arial", 11, FontStyle.Bold), Location = new Point(30, y), AutoSize = true };
            y += 30;

            dgvInventario = new DataGridView
            {
                Location = new Point(30, y),
                Size = new Size(820, 230),
                BackgroundColor = Color.White,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false
            };

            // === AQUÍ SE CREAN LAS COLUMNAS ===
            dgvInventario.Columns.Add("id_inventario", "ID");
            dgvInventario.Columns.Add("sucursal", "Sucursal");
            dgvInventario.Columns.Add("producto", "Producto");
            dgvInventario.Columns.Add("stock_actual", "Stock Actual");
            dgvInventario.Columns.Add("stock_minimo", "Stock Mínimo");
            dgvInventario.Columns.Add("tipo", "Tipo Almacenamiento");
            dgvInventario.Columns.Add("fecha", "Último Ingreso");

            dgvInventario.CellClick += (s, e) => SeleccionarFila();

            // Agregar todo al formulario
            this.Controls.AddRange(new Control[] {
                lblTitulo, lblSuc, cmbSucursal, lblProd, cmbProducto,
                lblAct, numStockActual, lblMin, numStockMinimo,
                lblTipo, cmbTipoAlmacen, lblFec, dtpFecha,
                btnGuardar, btnEditar, btnEliminar, btnLimpiar,
                lblTab, dgvInventario
            });
        }

        private void CargarSucursales()
        {
            cmbSucursal.Items.Clear();
            using (SqlConnection cn = new SqlConnection(conexion))
            {
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT id_sucursal, nombre_sucursal FROM Sucursal_Cine", cn);
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                        cmbSucursal.Items.Add(new { Id = r[0], Nombre = r[1].ToString() });
                    cmbSucursal.DisplayMember = "Nombre";
                    if (cmbSucursal.Items.Count > 0) cmbSucursal.SelectedIndex = 0;
                }
                catch (Exception ex) { MessageBox.Show("Error Sucursales: " + ex.Message); }
            }
        }

        private void CargarProductos()
        {
            cmbProducto.Items.Clear();
            using (SqlConnection cn = new SqlConnection(conexion))
            {
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT id_producto_alim, nombre_producto FROM Producto_Alimenticio", cn);
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                        cmbProducto.Items.Add(new { Id = r[0], Nombre = r[1].ToString() });
                    cmbProducto.DisplayMember = "Nombre";
                    if (cmbProducto.Items.Count > 0) cmbProducto.SelectedIndex = 0;
                }
                catch (Exception ex) { MessageBox.Show("Error Productos: " + ex.Message); }
            }
        }

        private void CargarTiposAlmacen()
        {
            cmbTipoAlmacen.Items.Clear();
            using (SqlConnection cn = new SqlConnection(conexion))
            {
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT id_tipo_almacen, descripcion FROM Tipo_Almacenamiento", cn);
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                        cmbTipoAlmacen.Items.Add(new { Id = r[0], Nombre = r[1].ToString() });
                    cmbTipoAlmacen.DisplayMember = "Nombre";
                    if (cmbTipoAlmacen.Items.Count > 0) cmbTipoAlmacen.SelectedIndex = 0;
                }
                catch (Exception ex) { MessageBox.Show("Error Almacenamiento: " + ex.Message); }
            }
        }

        private void CargarDatos()
        {
            dgvInventario.Rows.Clear();
            using (SqlConnection cn = new SqlConnection(conexion))
            {
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(@"
                        SELECT i.id_inventario, s.nombre_sucursal, p.nombre_producto, 
                               i.stock_actual, i.stock_minimo, t.descripcion, i.ultimo_ingreso
                        FROM Inventario_Sucursal i
                        JOIN Sucursal_Cine s ON i.id_sucursal = s.id_sucursal
                        JOIN Producto_Alimenticio p ON i.id_producto_alim = p.id_producto_alim
                        JOIN Tipo_Almacenamiento t ON i.id_tipo_almacen = t.id_tipo_almacen", cn);
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        dgvInventario.Rows.Add(
                            r[0],
                            r[1],
                            r[2],
                            r[3],
                            r[4],
                            r[5],
                            Convert.ToDateTime(r[6]).ToShortDateString()
                        );
                    }
                }
                catch (Exception ex) { MessageBox.Show("Error Cargar Datos: " + ex.Message); }
            }
        }

        private void SeleccionarFila()
        {
            if (dgvInventario.CurrentRow == null) return;

            idSeleccionado = Convert.ToInt32(dgvInventario.CurrentRow.Cells[0].Value);

            string nomSuc = dgvInventario.CurrentRow.Cells[1].Value.ToString();
            foreach (var item in cmbSucursal.Items)
                if (((dynamic)item).Nombre == nomSuc) { cmbSucursal.SelectedItem = item; break; }

            string nomProd = dgvInventario.CurrentRow.Cells[2].Value.ToString();
            foreach (var item in cmbProducto.Items)
                if (((dynamic)item).Nombre == nomProd) { cmbProducto.SelectedItem = item; break; }

            numStockActual.Value = Convert.ToDecimal(dgvInventario.CurrentRow.Cells[3].Value);
            numStockMinimo.Value = Convert.ToDecimal(dgvInventario.CurrentRow.Cells[4].Value);

            string nomTipo = dgvInventario.CurrentRow.Cells[5].Value.ToString();
            foreach (var item in cmbTipoAlmacen.Items)
                if (((dynamic)item).Nombre == nomTipo) { cmbTipoAlmacen.SelectedItem = item; break; }

            dtpFecha.Value = Convert.ToDateTime(dgvInventario.CurrentRow.Cells[6].Value);
        }

        private void Guardar()
        {
            if (cmbSucursal.SelectedItem == null || cmbProducto.SelectedItem == null || cmbTipoAlmacen.SelectedItem == null)
            {
                MessageBox.Show("Completa todos los campos");
                return;
            }

            dynamic s = cmbSucursal.SelectedItem;
            dynamic p = cmbProducto.SelectedItem;
            dynamic t = cmbTipoAlmacen.SelectedItem;

            using (SqlConnection cn = new SqlConnection(conexion))
            {
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(@"
                        INSERT INTO Inventario_Sucursal 
                        (id_sucursal, id_producto_alim, stock_actual, stock_minimo, id_tipo_almacen, ultimo_ingreso)
                        VALUES (@s, @p, @act, @min, @t, @fec)", cn);
                    cmd.Parameters.AddWithValue("@s", s.Id);
                    cmd.Parameters.AddWithValue("@p", p.Id);
                    cmd.Parameters.AddWithValue("@act", numStockActual.Value);
                    cmd.Parameters.AddWithValue("@min", numStockMinimo.Value);
                    cmd.Parameters.AddWithValue("@t", t.Id);
                    cmd.Parameters.AddWithValue("@fec", dtpFecha.Value);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("✅ Guardado correctamente");
                    Limpiar();
                    CargarDatos();
                }
                catch (Exception ex) { MessageBox.Show("Error al Guardar: " + ex.Message); }
            }
        }

        private void Editar()
        {
            if (idSeleccionado == 0)
            {
                MessageBox.Show("Selecciona un registro de la tabla");
                return;
            }
            if (MessageBox.Show("¿Actualizar este registro?", "Confirmar", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            dynamic s = cmbSucursal.SelectedItem;
            dynamic p = cmbProducto.SelectedItem;
            dynamic t = cmbTipoAlmacen.SelectedItem;

            using (SqlConnection cn = new SqlConnection(conexion))
            {
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(@"
                        UPDATE Inventario_Sucursal 
                        SET id_sucursal=@s, id_producto_alim=@p, stock_actual=@act, 
                            stock_minimo=@min, id_tipo_almacen=@t, ultimo_ingreso=@fec
                        WHERE id_inventario=@id", cn);
                    cmd.Parameters.AddWithValue("@id", idSeleccionado);
                    cmd.Parameters.AddWithValue("@s", s.Id);
                    cmd.Parameters.AddWithValue("@p", p.Id);
                    cmd.Parameters.AddWithValue("@act", numStockActual.Value);
                    cmd.Parameters.AddWithValue("@min", numStockMinimo.Value);
                    cmd.Parameters.AddWithValue("@t", t.Id);
                    cmd.Parameters.AddWithValue("@fec", dtpFecha.Value);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("✅ Actualizado");
                    Limpiar();
                    CargarDatos();
                }
                catch (Exception ex) { MessageBox.Show("Error al Editar: " + ex.Message); }
            }
        }

        private void Eliminar()
        {
            if (idSeleccionado == 0)
            {
                MessageBox.Show("Selecciona un registro de la tabla");
                return;
            }
            if (MessageBox.Show("¿Eliminar este registro?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            using (SqlConnection cn = new SqlConnection(conexion))
            {
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Inventario_Sucursal WHERE id_inventario=@id", cn);
                    cmd.Parameters.AddWithValue("@id", idSeleccionado);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("✅ Eliminado");
                    Limpiar();
                    CargarDatos();
                }
                catch (Exception ex) { MessageBox.Show("Error al Eliminar: " + ex.Message); }
            }
        }

        private void Limpiar()
        {
            if (cmbSucursal.Items.Count > 0) cmbSucursal.SelectedIndex = 0;
            if (cmbProducto.Items.Count > 0) cmbProducto.SelectedIndex = 0;
            if (cmbTipoAlmacen.Items.Count > 0) cmbTipoAlmacen.SelectedIndex = 0;
            numStockActual.Value = 0;
            numStockMinimo.Value = 0;
            dtpFecha.Value = DateTime.Now;
            idSeleccionado = 0;
        }
    }
}