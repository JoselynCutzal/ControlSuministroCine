namespace ControlDeSuministroCine
{
    partial class Compras
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        private void InitializeComponent()
        {
            this.lblIdCompra = new System.Windows.Forms.Label();
            this.lblFecha = new System.Windows.Forms.Label();
            this.lblSucursal = new System.Windows.Forms.Label();
            this.lblProvEq = new System.Windows.Forms.Label();
            this.lblProvServ = new System.Windows.Forms.Label();
            this.lblMetodoPago = new System.Windows.Forms.Label();
            this.lblEstado = new System.Windows.Forms.Label();

            this.txtIdCompra = new System.Windows.Forms.TextBox();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.txtIdSucursal = new System.Windows.Forms.TextBox();
            this.txtIdProvEq = new System.Windows.Forms.TextBox();
            this.txtIdProvServ = new System.Windows.Forms.TextBox();
            this.txtIdMetodoPago = new System.Windows.Forms.TextBox();
            this.cmbEstado = new System.Windows.Forms.ComboBox();

            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.dgvCompras = new System.Windows.Forms.DataGridView();

            ((System.ComponentModel.ISupportInitialize)(this.dgvCompras)).BeginInit();
            this.SuspendLayout();

            // lblIdCompra
            this.lblIdCompra.AutoSize = true;
            this.lblIdCompra.Location = new System.Drawing.Point(30, 30);
            this.lblIdCompra.Text = "ID Compra:";

            // txtIdCompra
            this.txtIdCompra.Location = new System.Drawing.Point(160, 28);
            this.txtIdCompra.Size = new System.Drawing.Size(200, 30);
            this.txtIdCompra.ReadOnly = true;
            this.txtIdCompra.BackColor = System.Drawing.Color.LightGray;
            this.txtIdCompra.Name = "txtIdCompra";

            // lblFecha
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(30, 75);
            this.lblFecha.Text = "Fecha Compra:";

            // dtpFecha
            this.dtpFecha.Location = new System.Drawing.Point(160, 73);
            this.dtpFecha.Size = new System.Drawing.Size(200, 30);
            this.dtpFecha.Name = "dtpFecha";

            // lblSucursal
            this.lblSucursal.AutoSize = true;
            this.lblSucursal.Location = new System.Drawing.Point(30, 120);
            this.lblSucursal.Text = "ID Sucursal:";

            // txtIdSucursal
            this.txtIdSucursal.Location = new System.Drawing.Point(160, 118);
            this.txtIdSucursal.Size = new System.Drawing.Size(200, 30);
            this.txtIdSucursal.Name = "txtIdSucursal";

            // lblProvEq
            this.lblProvEq.AutoSize = true;
            this.lblProvEq.Location = new System.Drawing.Point(30, 165);
            this.lblProvEq.Text = "ID Prov. Equipo:";

            // txtIdProvEq
            this.txtIdProvEq.Location = new System.Drawing.Point(160, 163);
            this.txtIdProvEq.Size = new System.Drawing.Size(200, 30);
            this.txtIdProvEq.Name = "txtIdProvEq";

            // lblProvServ
            this.lblProvServ.AutoSize = true;
            this.lblProvServ.Location = new System.Drawing.Point(30, 210);
            this.lblProvServ.Text = "ID Prov. Servicio:";

            // txtIdProvServ
            this.txtIdProvServ.Location = new System.Drawing.Point(160, 208);
            this.txtIdProvServ.Size = new System.Drawing.Size(200, 30);
            this.txtIdProvServ.Name = "txtIdProvServ";

            // lblMetodoPago
            this.lblMetodoPago.AutoSize = true;
            this.lblMetodoPago.Location = new System.Drawing.Point(30, 255);
            this.lblMetodoPago.Text = "ID Método Pago:";

            // txtIdMetodoPago
            this.txtIdMetodoPago.Location = new System.Drawing.Point(160, 253);
            this.txtIdMetodoPago.Size = new System.Drawing.Size(200, 30);
            this.txtIdMetodoPago.Name = "txtIdMetodoPago";

            // lblEstado
            this.lblEstado.AutoSize = true;
            this.lblEstado.Location = new System.Drawing.Point(30, 300);
            this.lblEstado.Text = "Estado:";

            // cmbEstado
            this.cmbEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEstado.Location = new System.Drawing.Point(160, 298);
            this.cmbEstado.Size = new System.Drawing.Size(200, 30);
            this.cmbEstado.Name = "cmbEstado";
            this.cmbEstado.Items.AddRange(new object[] { "Pendiente", "Completada", "Cancelada" });

            // btnGuardar
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Location = new System.Drawing.Point(30, 360);
            this.btnGuardar.Size = new System.Drawing.Size(110, 45);
            this.btnGuardar.BackColor = System.Drawing.Color.LightGreen;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);

            // btnNuevo
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Location = new System.Drawing.Point(160, 360);
            this.btnNuevo.Size = new System.Drawing.Size(110, 45);
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);

            // btnSalir
            this.btnSalir.Text = "Salir";
            this.btnSalir.Location = new System.Drawing.Point(290, 360);
            this.btnSalir.Size = new System.Drawing.Size(110, 45);
            this.btnSalir.BackColor = System.Drawing.Color.LightCoral;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);

            // dgvCompras
            this.dgvCompras.Location = new System.Drawing.Point(30, 430);
            this.dgvCompras.Size = new System.Drawing.Size(450, 200);
            this.dgvCompras.ReadOnly = true;
            this.dgvCompras.BackgroundColor = System.Drawing.Color.White;

            // Formulario
            this.ClientSize = new System.Drawing.Size(520, 670);
            this.Text = "Compras";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            this.Controls.Add(this.lblIdCompra);
            this.Controls.Add(this.txtIdCompra);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.dtpFecha);
            this.Controls.Add(this.lblSucursal);
            this.Controls.Add(this.txtIdSucursal);
            this.Controls.Add(this.lblProvEq);
            this.Controls.Add(this.txtIdProvEq);
            this.Controls.Add(this.lblProvServ);
            this.Controls.Add(this.txtIdProvServ);
            this.Controls.Add(this.lblMetodoPago);
            this.Controls.Add(this.txtIdMetodoPago);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.cmbEstado);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.dgvCompras);

            ((System.ComponentModel.ISupportInitialize)(this.dgvCompras)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        internal System.Windows.Forms.Label lblIdCompra;
        internal System.Windows.Forms.TextBox txtIdCompra;
        internal System.Windows.Forms.Label lblFecha;
        internal System.Windows.Forms.DateTimePicker dtpFecha;
        internal System.Windows.Forms.Label lblSucursal;
        internal System.Windows.Forms.TextBox txtIdSucursal;
        internal System.Windows.Forms.Label lblProvEq;
        internal System.Windows.Forms.TextBox txtIdProvEq;
        internal System.Windows.Forms.Label lblProvServ;
        internal System.Windows.Forms.TextBox txtIdProvServ;
        internal System.Windows.Forms.Label lblMetodoPago;
        internal System.Windows.Forms.TextBox txtIdMetodoPago;
        internal System.Windows.Forms.Label lblEstado;
        internal System.Windows.Forms.ComboBox cmbEstado;
        internal System.Windows.Forms.Button btnGuardar;
        internal System.Windows.Forms.Button btnNuevo;
        internal System.Windows.Forms.Button btnSalir;
        internal System.Windows.Forms.DataGridView dgvCompras;
    }
}