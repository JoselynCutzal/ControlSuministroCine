using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace ControlSuministroCine
{
    public partial class FormProveedores : Form
    {
        private string conexion = @"Data Source=JOSELYN\SQLEXPRESS;Initial Catalog=ControlDeSuministroCine;Integrated Security=True";
        private ComboBox cmbTipo;
        private ComboBox cmbPais;
        private TextBox txtNombre;
        private TextBox txtTelefono;
        private TextBox txtDireccion;
        private TextBox txtExtra;
        private DataGridView dgvProveedores;
        private int idSeleccionado = 0;

        public FormProveedores()
        {
            InitializeComponent();
            CrearInterfaz();
            InicializarTabla(); // ✅ Creamos columnas al iniciar
        }

        private void InicializarTabla()
        {
            dgvProveedores.Columns.Clear();
            dgvProveedores.Columns.Add("ID", "ID");
            dgvProveedores.Columns.Add("Nombre", "Nombre");
            dgvProveedores.Columns.Add("Contacto", "Contacto");
            dgvProveedores.Columns.Add("DatoExtra", "Dato Extra");
        }

        private void CrearInterfaz()
        {
            this.Text = "Gestión de Proveedores";
            this.Size = new Size(820, 650);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(22, 28, 42);

            Label lblTitulo = new Label
            {
                Text = "GESTIÓN DE PROVEEDORES",
                ForeColor = Color.White,
                Font = new Font("Arial", 18, FontStyle.Bold),
                Location = new Point(280, 10),
                AutoSize = true
            };
            this.Controls.Add(lblTitulo);

            Label lblTipo = new Label
            {
                Text = "Selecciona el tipo:",
                ForeColor = Color.White,
                Font = new Font("Arial", 11, FontStyle.Bold),
                Location = new Point(30, 60),
                AutoSize = true
            };

            cmbTipo = new ComboBox
            {
                Location = new Point(180, 58),
                Width = 520,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Arial", 11)
            };
            cmbTipo.Items.AddRange(new string[] {
                "🛠️ Proveedor de Equipos",
                "🍿 Proveedor de Alimentos",
                "🔧 Proveedor de Servicios"
            });
            cmbTipo.SelectedIndexChanged += (s, e) =>
            {
                idSeleccionado = 0;
                LimpiarCampos();
                ActualizarEtiquetas();
                CargarDatos();
            };

            Label lblNombre = new Label { Name = "lblNombre", Text = "Nombre:", ForeColor = Color.White, Location = new Point(30, 110), AutoSize = true };
            txtNombre = new TextBox { Location = new Point(180, 108), Width = 520, Font = new Font("Arial", 11) };

            Label lblTelefono = new Label { Name = "lblTelefono", Text = "Teléfono:", ForeColor = Color.White, Location = new Point(30, 150), AutoSize = true };
            txtTelefono = new TextBox { Location = new Point(180, 148), Width = 520, Font = new Font("Arial", 11) };

            Label lblDireccion = new Label { Name = "lblDireccion", Text = "Dirección:", ForeColor = Color.White, Location = new Point(30, 190), AutoSize = true };
            txtDireccion = new TextBox { Location = new Point(180, 188), Width = 520, Font = new Font("Arial", 11) };

            Label lblPais = new Label { Text = "País:", ForeColor = Color.White, Location = new Point(30, 230), AutoSize = true };
            cmbPais = new ComboBox
            {
                Location = new Point(180, 228),
                Width = 300,
                DropDownStyle = ComboBoxStyle.DropDown,
                Font = new Font("Arial", 11)
            };
            CargarPaises();

            Label lblExtra = new Label { Name = "lblExtra", Text = "Correo/Garantía:", ForeColor = Color.White, Location = new Point(500, 230), AutoSize = true };
            txtExtra = new TextBox { Location = new Point(620, 228), Width = 150, Font = new Font("Arial", 11) };

            Button btnGuardar = new Button
            {
                Text = "💾 GUARDAR",
                Location = new Point(30, 280),
                Width = 160,
                Height = 45,
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            btnGuardar.Click += (s, e) => Guardar();

            Button btnEditar = new Button
            {
                Text = "✏️ EDITAR",
                Location = new Point(210, 280),
                Width = 160,
                Height = 45,
                BackColor = Color.FromArgb(255, 193, 7),
                ForeColor = Color.Black,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            btnEditar.Click += (s, e) => Editar();

            Button btnEliminar = new Button
            {
                Text = "🗑️ ELIMINAR",
                Location = new Point(390, 280),
                Width = 160,
                Height = 45,
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            btnEliminar.Click += (s, e) => Eliminar();

            Button btnLimpiar = new Button
            {
                Text = "🔄 LIMPIAR",
                Location = new Point(570, 280),
                Width = 130,
                Height = 45,
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            btnLimpiar.Click += (s, e) => { LimpiarCampos(); };

            Label lblTabla = new Label
            {
                Text = "📋 Lista de Proveedores",
                ForeColor = Color.White,
                Font = new Font("Arial", 11, FontStyle.Bold),
                Location = new Point(30, 340),
                AutoSize = true
            };

            dgvProveedores = new DataGridView
            {
                Location = new Point(30, 370),
                Size = new Size(740, 230),
                BackgroundColor = Color.White,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true
            };
            dgvProveedores.CellClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow fila = dgvProveedores.Rows[e.RowIndex];
                    idSeleccionado = Convert.ToInt32(fila.Cells[0].Value);
                    txtNombre.Text = fila.Cells[1].Value?.ToString() ?? "";
                    txtTelefono.Text = fila.Cells[2].Value?.ToString() ?? "";
                    txtDireccion.Text = fila.Cells[3].Value?.ToString() ?? "";
                }
            };

            this.Controls.AddRange(new Control[] {
                lblTipo, cmbTipo, lblNombre, txtNombre, lblTelefono, txtTelefono,
                lblDireccion, txtDireccion, lblPais, cmbPais, lblExtra, txtExtra,
                btnGuardar, btnEditar, btnEliminar, btnLimpiar, lblTabla, dgvProveedores
            });
        }

        private void CargarPaises()
        {
            cmbPais.Items.Clear();
            try
            {
                using (SqlConnection cn = new SqlConnection(conexion))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT id_pais, nombre FROM Pais ORDER BY nombre", cn);
                    SqlDataReader lector = cmd.ExecuteReader();
                    while (lector.Read())
                    {
                        cmbPais.Items.Add(new PaisItem { Id = Convert.ToInt32(lector[0]), Nombre = lector[1].ToString() });
                    }
                }
                cmbPais.DisplayMember = "Nombre";
                if (cmbPais.Items.Count > 0) cmbPais.SelectedIndex = 0;
            }
            catch
            {
                cmbPais.Items.Add(new PaisItem { Id = 1, Nombre = "Guatemala" });
                cmbPais.Items.Add(new PaisItem { Id = 2, Nombre = "México" });
                cmbPais.Items.Add(new PaisItem { Id = 3, Nombre = "Estados Unidos" });
                cmbPais.DisplayMember = "Nombre";
                cmbPais.SelectedIndex = 0;
            }
        }

        private void ActualizarEtiquetas()
        {
            Label lblNombre = (Label)this.Controls.Find("lblNombre", true)[0];
            Label lblTelefono = (Label)this.Controls.Find("lblTelefono", true)[0];
            Label lblDireccion = (Label)this.Controls.Find("lblDireccion", true)[0];
            Label lblExtra = (Label)this.Controls.Find("lblExtra", true)[0];

            switch (cmbTipo.SelectedIndex)
            {
                case 0:
                    lblNombre.Text = "Nombre proveedor:";
                    lblTelefono.Text = "Contacto ventas:";
                    lblDireccion.Text = "Marca distribuye:";
                    lblExtra.Text = "Garantía (meses):";
                    cmbPais.Visible = false; break;
                case 1:
                    lblNombre.Text = "Nombre proveedor:";
                    lblTelefono.Text = "Teléfono:";
                    lblDireccion.Text = "Dirección:";
                    lblExtra.Text = "Correo:";
                    cmbPais.Visible = true; break;
                case 2:
                    lblNombre.Text = "Nombre empresa:";
                    lblTelefono.Text = "Teléfono contacto:";
                    lblDireccion.Text = "Tipo servicio:";
                    lblExtra.Visible = false;
                    cmbPais.Visible = false; break;
            }
        }

        private void CargarDatos()
        {
            if (cmbTipo.SelectedIndex == -1) return;
            dgvProveedores.Rows.Clear();

            string sql = "";
            switch (cmbTipo.SelectedIndex)
            {
                case 0:
                    sql = "SELECT id_proveedor_eq, nombre_proveedor, contacto_ventas, marca_distribuye FROM Proveedor_Equipos ORDER BY id_proveedor_eq";
                    break;
                case 1:
                    sql = "SELECT id_proveedor_alim, nombre_proveedor, telefono, direccion FROM Proveedor_Alimentos ORDER BY id_proveedor_alim";
                    break;
                case 2:
                    sql = "SELECT id_proveedor_serv, nombre_empresa, telefono_contacto, tipo_servicio FROM Proveedor_Servicios ORDER BY id_proveedor_serv";
                    break;
            }

            using (SqlConnection cn = new SqlConnection(conexion))
            {
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(sql, cn);
                    SqlDataReader lector = cmd.ExecuteReader();
                    while (lector.Read())
                    {
                        dgvProveedores.Rows.Add(lector[0], lector[1], lector[2], lector[3]);
                    }
                }
                catch (Exception ex) { MessageBox.Show("Error: " + ex.Message, "Error"); }
            }
        }

        private void Guardar()
        {
            if (cmbTipo.SelectedIndex == -1) { MessageBox.Show("Selecciona un tipo", "Aviso"); return; }
            if (string.IsNullOrWhiteSpace(txtNombre.Text)) { MessageBox.Show("Escribe el nombre", "Aviso"); return; }

            using (SqlConnection cn = new SqlConnection(conexion))
            {
                try
                {
                    cn.Open();
                    SqlCommand cmd = null;

                    switch (cmbTipo.SelectedIndex)
                    {
                        case 0:
                            cmd = new SqlCommand(
                                "INSERT INTO Proveedor_Equipos (nombre_proveedor, contacto_ventas, marca_distribuye, garantia_meses) " +
                                "VALUES (@nom, @tel, @dir, @extra)", cn);
                            cmd.Parameters.AddWithValue("@nom", txtNombre.Text);
                            cmd.Parameters.AddWithValue("@tel", string.IsNullOrWhiteSpace(txtTelefono.Text) ? (object)DBNull.Value : txtTelefono.Text);
                            cmd.Parameters.AddWithValue("@dir", string.IsNullOrWhiteSpace(txtDireccion.Text) ? (object)DBNull.Value : txtDireccion.Text);
                            cmd.Parameters.AddWithValue("@extra", string.IsNullOrWhiteSpace(txtExtra.Text) ? (object)DBNull.Value : Convert.ToInt16(txtExtra.Text));
                            break;
                        case 1:
                            if (cmbPais.SelectedItem == null) { MessageBox.Show("Selecciona un país", "Aviso"); return; }
                            PaisItem pais = (PaisItem)cmbPais.SelectedItem;
                            cmd = new SqlCommand(
                                "INSERT INTO Proveedor_Alimentos (nombre_proveedor, telefono, direccion, correo, id_pais, estado) " +
                                "VALUES (@nom, @tel, @dir, @extra, @pais, 1)", cn);
                            cmd.Parameters.AddWithValue("@nom", txtNombre.Text);
                            cmd.Parameters.AddWithValue("@tel", string.IsNullOrWhiteSpace(txtTelefono.Text) ? (object)DBNull.Value : txtTelefono.Text);
                            cmd.Parameters.AddWithValue("@dir", string.IsNullOrWhiteSpace(txtDireccion.Text) ? (object)DBNull.Value : txtDireccion.Text);
                            cmd.Parameters.AddWithValue("@extra", string.IsNullOrWhiteSpace(txtExtra.Text) ? (object)DBNull.Value : txtExtra.Text);
                            cmd.Parameters.AddWithValue("@pais", pais.Id);
                            break;
                        case 2:
                            cmd = new SqlCommand(
                                "INSERT INTO Proveedor_Servicios (nombre_empresa, telefono_contacto, tipo_servicio, estado) " +
                                "VALUES (@nom, @tel, @dir, 1)", cn);
                            cmd.Parameters.AddWithValue("@nom", txtNombre.Text);
                            cmd.Parameters.AddWithValue("@tel", string.IsNullOrWhiteSpace(txtTelefono.Text) ? (object)DBNull.Value : txtTelefono.Text);
                            cmd.Parameters.AddWithValue("@dir", txtDireccion.Text);
                            break;
                    }

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("✅ Guardado correctamente", "Éxito");
                    LimpiarCampos();
                    CargarDatos();
                }
                catch (Exception ex) { MessageBox.Show("Error: " + ex.Message, "Error"); }
            }
        }

        private void Editar()
        {
            if (cmbTipo.SelectedIndex == -1 || idSeleccionado == 0) { MessageBox.Show("Selecciona uno de la tabla", "Aviso"); return; }
            if (MessageBox.Show("¿Actualizar los datos?", "Confirmar", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            using (SqlConnection cn = new SqlConnection(conexion))
            {
                try
                {
                    cn.Open();
                    SqlCommand cmd = null;

                    switch (cmbTipo.SelectedIndex)
                    {
                        case 0:
                            cmd = new SqlCommand(
                                "UPDATE Proveedor_Equipos SET nombre_proveedor=@nom, contacto_ventas=@tel, marca_distribuye=@dir, garantia_meses=@extra " +
                                "WHERE id_proveedor_eq=@id", cn);
                            cmd.Parameters.AddWithValue("@id", idSeleccionado);
                            cmd.Parameters.AddWithValue("@nom", txtNombre.Text);
                            cmd.Parameters.AddWithValue("@tel", string.IsNullOrWhiteSpace(txtTelefono.Text) ? (object)DBNull.Value : txtTelefono.Text);
                            cmd.Parameters.AddWithValue("@dir", string.IsNullOrWhiteSpace(txtDireccion.Text) ? (object)DBNull.Value : txtDireccion.Text);
                            cmd.Parameters.AddWithValue("@extra", string.IsNullOrWhiteSpace(txtExtra.Text) ? (object)DBNull.Value : Convert.ToInt16(txtExtra.Text));
                            break;
                        case 1:
                            if (cmbPais.SelectedItem == null) { MessageBox.Show("Selecciona país", "Aviso"); return; }
                            PaisItem pais = (PaisItem)cmbPais.SelectedItem;
                            cmd = new SqlCommand(
                                "UPDATE Proveedor_Alimentos SET nombre_proveedor=@nom, telefono=@tel, direccion=@dir, correo=@extra, id_pais=@pais " +
                                "WHERE id_proveedor_alim=@id", cn);
                            cmd.Parameters.AddWithValue("@id", idSeleccionado);
                            cmd.Parameters.AddWithValue("@nom", txtNombre.Text);
                            cmd.Parameters.AddWithValue("@tel", string.IsNullOrWhiteSpace(txtTelefono.Text) ? (object)DBNull.Value : txtTelefono.Text);
                            cmd.Parameters.AddWithValue("@dir", string.IsNullOrWhiteSpace(txtDireccion.Text) ? (object)DBNull.Value : txtDireccion.Text);
                            cmd.Parameters.AddWithValue("@extra", string.IsNullOrWhiteSpace(txtExtra.Text) ? (object)DBNull.Value : txtExtra.Text);
                            cmd.Parameters.AddWithValue("@pais", pais.Id);
                            break;
                        case 2:
                            cmd = new SqlCommand(
                                "UPDATE Proveedor_Servicios SET nombre_empresa=@nom, telefono_contacto=@tel, tipo_servicio=@dir " +
                                "WHERE id_proveedor_serv=@id", cn);
                            cmd.Parameters.AddWithValue("@id", idSeleccionado);
                            cmd.Parameters.AddWithValue("@nom", txtNombre.Text);
                            cmd.Parameters.AddWithValue("@tel", string.IsNullOrWhiteSpace(txtTelefono.Text) ? (object)DBNull.Value : txtTelefono.Text);
                            cmd.Parameters.AddWithValue("@dir", txtDireccion.Text);
                            break;
                    }

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("✅ Actualizado", "Éxito");
                    LimpiarCampos();
                    CargarDatos();
                }
                catch (Exception ex) { MessageBox.Show("Error: " + ex.Message, "Error"); }
            }
        }

        private void Eliminar()
        {
            if (cmbTipo.SelectedIndex == -1 || idSeleccionado == 0) { MessageBox.Show("Selecciona uno de la tabla", "Aviso"); return; }
            if (MessageBox.Show("¿Eliminar este proveedor?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            using (SqlConnection cn = new SqlConnection(conexion))
            {
                try
                {
                    cn.Open();
                    SqlCommand cmd = null;
                    switch (cmbTipo.SelectedIndex)
                    {
                        case 0: cmd = new SqlCommand("DELETE FROM Proveedor_Equipos WHERE id_proveedor_eq=@id", cn); break;
                        case 1: cmd = new SqlCommand("DELETE FROM Proveedor_Alimentos WHERE id_proveedor_alim=@id", cn); break;
                        case 2: cmd = new SqlCommand("DELETE FROM Proveedor_Servicios WHERE id_proveedor_serv=@id", cn); break;
                    }
                    cmd.Parameters.AddWithValue("@id", idSeleccionado);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("✅ Eliminado", "Éxito");
                    LimpiarCampos();
                    CargarDatos();
                }
                catch (Exception ex) { MessageBox.Show("Error: " + ex.Message, "Error"); }
            }
        }

        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtTelefono.Clear();
            txtDireccion.Clear();
            txtExtra.Clear();
            idSeleccionado = 0;
        }
    }

    public class PaisItem
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public override string ToString() => Nombre;
    }
}