using ControlDeSuministroCine;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ControlSuministroCine
{
    public partial class Form1 : Form
    {
        private Panel panelLateral;
        private Panel panelContenido;
        private Panel panelTarjetas;
        private Panel panelTabla;
        private DataGridView dgvInventario;

        public Form1()
        {
            InitializeComponent();
            ConfigurarFormulario();
            CrearPanelLateral();
            CrearPanelContenido();
            CrearTarjetasEstadisticas();
            CrearBuscadorYTabla();
        }

        private void ConfigurarFormulario()
        {
            this.Text = "Sistema de Control de Suministro";
            this.Size = new Size(1200, 750);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(1000, 650);
            this.BackColor = Color.FromArgb(12, 20, 39);
        }

        private void CrearPanelLateral()
        {
            panelLateral = new Panel
            {
                Dock = DockStyle.Left,
                Width = 210,
                BackColor = Color.FromArgb(22, 33, 58)
            };

            Label lblMenu = new Label
            {
                Text = "MENÚ",
                ForeColor = Color.FromArgb(148, 163, 184),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(25, 20),
                AutoSize = true
            };
            panelLateral.Controls.Add(lblMenu);

            int y = 60;

            string[] opciones = {
                "🏠 Inicio",
                "📦 Inventario",
                "🛒 Compras",
                "🤝 Proveedores",
                "📋 Órdenes de Suministro",
                "📊 Reportes",
                "⚙️ Configuración",
                "❌ Salir"
            };

            foreach (string opcion in opciones)
            {
                Button btn = new Button
                {
                    Text = opcion,
                    Size = new Size(160, 45),
                    Location = new Point(25, y),
                    BackColor = Color.FromArgb(30, 50, 90),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Padding = new Padding(12, 0, 0, 0),
                    Font = new Font("Segoe UI", 9.5f),
                    Cursor = Cursors.Hand
                };
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(49, 46, 129);

                if (opcion.Contains("Inicio"))
                {
                    btn.BackColor = Color.FromArgb(30, 64, 175);
                }

                if (opcion.Contains("Inicio"))
                {
                    btn.Click += (s, e) => { MessageBox.Show("Pantalla principal", "Inicio"); };
                }
                else if (opcion.Contains("Inventario"))
                {
                    btn.Click += (s, e) => { new Inventario().ShowDialog(); };
                }
                else if (opcion.Contains("Compras"))
                {
                    btn.Click += (s, e) => { new Compras().ShowDialog(); };
                }
                else if (opcion.Contains("Proveedores"))
                {
                    btn.Click += (s, e) => { new FormProveedores().Show(); };
                }
                else if (opcion.Contains("Órdenes"))
                {
                    btn.Click += (s, e) => { new frmOrdenes().Show(); };
                }
                else if (opcion.Contains("Reportes"))
                {
                    btn.Click += (s, e) => { new frmReportes().Show(); };
                }
                else if (opcion.Contains("Configuración"))
                {
                    btn.Click += (s, e) => { new frmConfiguracion().Show(); };
                }
                else if (opcion.Contains("Salir"))
                {
                    btn.Click += (s, e) => {
                        if (MessageBox.Show("¿Salir del sistema?", "Confirmar",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            Application.Exit();
                    };
                }

                panelLateral.Controls.Add(btn);
                y += 50;
            }

            this.Controls.Add(panelLateral);
        }

        private void CrearPanelContenido()
        {
            panelContenido = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(12, 20, 39)
            };

            Panel cabecera = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.FromArgb(15, 27, 52)
            };

            Label lblTitulo = new Label
            {
                Text = "SISTEMA DE CONTROL DE SUMINISTRO",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(25, 20),
                AutoSize = true
            };
            cabecera.Controls.Add(lblTitulo);

            Label lblSubtitulo = new Label
            {
                Text = "Mantén tu inventario en orden y nunca te quedes sin lo esencial",
                ForeColor = Color.FromArgb(148, 163, 184),
                Font = new Font("Segoe UI", 10),
                Location = new Point(25, 50),
                AutoSize = true
            };
            cabecera.Controls.Add(lblSubtitulo);

            panelContenido.Controls.Add(cabecera);
            this.Controls.Add(panelContenido);
            panelContenido.BringToFront();
        }

        private void CrearTarjetasEstadisticas()
        {
            panelTarjetas = new Panel
            {
                Location = new Point(230, 95),
                Size = new Size(930, 110),
                BackColor = Color.Transparent
            };

            CrearTarjeta("Insumos Totales", "142", Color.FromArgb(220, 38, 38), 0);
            CrearTarjeta("Stock en Almacén", "87%", Color.FromArgb(5, 150, 105), 240);
            CrearTarjeta("Pedidos Pendientes", "4", Color.FromArgb(217, 119, 6), 480);
            CrearTarjeta("Proveedores Activos", "12", Color.FromArgb(79, 70, 229), 720);

            panelContenido.Controls.Add(panelTarjetas);
        }

        private void CrearTarjeta(string titulo, string valor, Color color, int x)
        {
            Panel tarjeta = new Panel
            {
                Size = new Size(210, 95),
                Location = new Point(x, 0),
                BackColor = Color.FromArgb(26, 34, 62)
            };

            Panel colorBar = new Panel
            {
                Size = new Size(5, 95),
                Location = new Point(0, 0),
                BackColor = color
            };
            tarjeta.Controls.Add(colorBar);

            Label lblValor = new Label
            {
                Text = valor,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 26, FontStyle.Bold),
                Location = new Point(20, 15),
                AutoSize = true
            };

            Label lblTitulo = new Label
            {
                Text = titulo,
                ForeColor = Color.FromArgb(156, 163, 175),
                Font = new Font("Segoe UI", 9.5f),
                Location = new Point(22, 55),
                AutoSize = true
            };

            tarjeta.Controls.Add(lblValor);
            tarjeta.Controls.Add(lblTitulo);
            panelTarjetas.Controls.Add(tarjeta);
        }

        private void CrearBuscadorYTabla()
        {
            int posicionY = 230;

            panelTabla = new Panel
            {
                Location = new Point(230, posicionY),
                Size = new Size(930, 450),
                BackColor = Color.FromArgb(26, 34, 62)
            };

            Label lblTabla = new Label
            {
                Text = "Inventario de Insumos",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(15, 15),
                AutoSize = true
            };
            panelTabla.Controls.Add(lblTabla);

            TextBox txtBuscar = new TextBox
            {
                Text = "Buscar insumo...",
                Location = new Point(15, 50),
                Size = new Size(280, 35),
                BackColor = Color.FromArgb(30, 42, 71),
                ForeColor = Color.Gray,
                BorderStyle = BorderStyle.FixedSingle
            };

            txtBuscar.Enter += (s, e) =>
            {
                if (txtBuscar.Text == "Buscar insumo...")
                {
                    txtBuscar.Text = "";
                    txtBuscar.ForeColor = Color.White;
                }
            };

            txtBuscar.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtBuscar.Text))
                {
                    txtBuscar.Text = "Buscar insumo...";
                    txtBuscar.ForeColor = Color.Gray;
                }
            };

            panelTabla.Controls.Add(txtBuscar);

            Button btnAgregar = new Button
            {
                Text = "+ Agregar Insumo",
                Size = new Size(140, 35),
                Location = new Point(panelTabla.Width - 160, 48),
                BackColor = Color.FromArgb(37, 99, 235),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnAgregar.FlatAppearance.BorderSize = 0;
            panelTabla.Controls.Add(btnAgregar);

            dgvInventario = new DataGridView
            {
                Location = new Point(15, 95),
                Size = new Size(panelTabla.Width - 30, 340),
                BackgroundColor = Color.FromArgb(30, 42, 71),
                ForeColor = Color.White,
                GridColor = Color.FromArgb(51, 65, 85),
                BorderStyle = BorderStyle.None,
                EnableHeadersVisualStyles = false,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            dgvInventario.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 58, 95);
            dgvInventario.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvInventario.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvInventario.DefaultCellStyle.BackColor = Color.FromArgb(30, 42, 71);
            dgvInventario.DefaultCellStyle.ForeColor = Color.White;
            dgvInventario.DefaultCellStyle.SelectionBackColor = Color.FromArgb(49, 46, 129);

            dgvInventario.Columns.Add("Producto", "Producto");
            dgvInventario.Columns.Add("Categoria", "Categoría");
            dgvInventario.Columns.Add("StockActual", "Stock Actual");
            dgvInventario.Columns.Add("StockMinimo", "Stock Mínimo");
            dgvInventario.Columns.Add("Estado", "Estado");

            dgvInventario.Rows.Add("Palomitas de maíz", "Alimentos", "50 kg", "20 kg", "✅ En stock");
            dgvInventario.Rows.Add("Mantequilla", "Alimentos", "8 kg", "10 kg", "⚠️ Stock bajo");
            dgvInventario.Rows.Add("Refrescos (Lata)", "Bebidas", "1200 u", "300 u", "✅ En stock");
            dgvInventario.Rows.Add("Vasos desechables", "Utensilios", "4000 u", "1000 u", "✅ En stock");
            dgvInventario.Rows.Add("Servilletas", "Utensilios", "6000 u", "2000 u", "✅ En stock");
            dgvInventario.Rows.Add("Aceite vegetal", "Alimentos", "5 L", "10 L", "⚠️ Stock bajo");

            panelTabla.Controls.Add(dgvInventario);
            panelContenido.Controls.Add(panelTabla);
        }
    }
}