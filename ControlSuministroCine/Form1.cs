using System;
using System.Drawing;
using System.Windows.Forms;

namespace ControlSuministroCine
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CrearMenuLateral();
        }

        private void CrearMenuLateral()
        {
            int y = 80;

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
                    Size = new Size(190, 48),
                    Location = new Point(20, y),
                    BackColor = Color.FromArgb(42, 48, 65),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Padding = new Padding(15, 0, 0, 0)
                };

                btn.FlatAppearance.BorderSize = 0;

                if (opcion.Contains("Inicio"))
                {
                    btn.Click += (s, e) =>
                    {
                        MessageBox.Show("Pantalla principal", "Inicio");
                    };
                }
                else if (opcion.Contains("Inventario"))
                {
                    btn.Click += (s, e) =>
                    {
                        Inventario frm = new Inventario();
                        frm.ShowDialog();
                    };
                }
                else if (opcion.Contains("Compras"))
                {
                    btn.Click += (s, e) =>
                    {
                        ControlDeSuministroCine.Compras frm = new ControlDeSuministroCine.Compras();
                        frm.ShowDialog();
                    };
                }
                else if (opcion.Contains("Proveedores"))
                {
                    btn.Click += (s, e) =>
                    {
                        FormProveedores frm = new FormProveedores();
                        frm.Show();
                    };
                }
                else if (opcion.Contains("Órdenes"))
                {
                    btn.Click += (s, e) =>
                    {
                        frmOrdenes frm = new frmOrdenes();
                        frm.Show();
                    };
                }
                else if (opcion.Contains("Reportes"))
                {
                    btn.Click += (s, e) =>
                    {
                        frmReportes frm = new frmReportes();
                        frm.Show();
                    };
                }
                else if (opcion.Contains("Configuración"))
                {
                    btn.Click += (s, e) =>
                    {
                        frmConfiguracion frm = new frmConfiguracion();
                        frm.Show();
                    };
                }
                else if (opcion.Contains("Salir"))
                {
                    btn.Click += (s, e) =>
                    {
                        this.Close();
                    };
                }

                // ✅ Agregar al panel lateral

                this.Controls.Add(btn);

                y += 55;
            }
        }
    }
}