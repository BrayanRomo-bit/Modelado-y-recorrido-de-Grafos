namespace Modelado_y_recorrido_de_Grafos
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private SplitContainer splitPrincipal;
        private SplitContainer splitDerecha;
        private Panel panelControles;
        private Panel panelGrafo;
        private Label lblTitulo;
        private GroupBox grpRuta;
        private Label lblOrigen;
        private ComboBox cmbOrigen;
        private Label lblDestino;
        private ComboBox cmbDestino;
        private Button btnCalcularDijkstra;
        private Label lblResultadoRuta;
        private GroupBox grpNodos;
        private Label lblNombreNodo;
        private TextBox txtNombreNodo;
        private Button btnAgregarNodo;
        private Button btnEliminarNodo;
        private GroupBox grpAristas;
        private Label lblAristaOrigen;
        private ComboBox cmbAristaOrigen;
        private Label lblAristaDestino;
        private ComboBox cmbAristaDestino;
        private Label lblPeso;
        private NumericUpDown numPeso;
        private Button btnAgregarArista;
        private Button btnEliminarArista;
        private GroupBox grpRed;
        private Button btnCargarEjemplo;
        private Button btnLimpiarGrafo;
        private TabControl tabResultados;
        private TabPage tabTraza;
        private TabPage tabMatriz;
        private TabPage tabLista;
        private DataGridView dgvTraza;
        private DataGridView dgvMatriz;
        private TextBox txtListaAdyacencia;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            splitPrincipal = new SplitContainer();
            panelControles = new Panel();
            grpRed = new GroupBox();
            btnLimpiarGrafo = new Button();
            btnCargarEjemplo = new Button();
            grpAristas = new GroupBox();
            btnEliminarArista = new Button();
            btnAgregarArista = new Button();
            numPeso = new NumericUpDown();
            lblPeso = new Label();
            cmbAristaDestino = new ComboBox();
            lblAristaDestino = new Label();
            cmbAristaOrigen = new ComboBox();
            lblAristaOrigen = new Label();
            grpNodos = new GroupBox();
            btnEliminarNodo = new Button();
            btnAgregarNodo = new Button();
            txtNombreNodo = new TextBox();
            lblNombreNodo = new Label();
            grpRuta = new GroupBox();
            lblResultadoRuta = new Label();
            btnCalcularDijkstra = new Button();
            cmbDestino = new ComboBox();
            lblDestino = new Label();
            cmbOrigen = new ComboBox();
            lblOrigen = new Label();
            lblTitulo = new Label();
            splitDerecha = new SplitContainer();
            panelGrafo = new Panel();
            tabResultados = new TabControl();
            tabTraza = new TabPage();
            dgvTraza = new DataGridView();
            tabMatriz = new TabPage();
            dgvMatriz = new DataGridView();
            tabLista = new TabPage();
            txtListaAdyacencia = new TextBox();

            ((System.ComponentModel.ISupportInitialize)splitPrincipal).BeginInit();
            splitPrincipal.Panel1.SuspendLayout();
            splitPrincipal.Panel2.SuspendLayout();
            splitPrincipal.SuspendLayout();
            panelControles.SuspendLayout();
            grpRed.SuspendLayout();
            grpAristas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numPeso).BeginInit();
            grpNodos.SuspendLayout();
            grpRuta.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitDerecha).BeginInit();
            splitDerecha.Panel1.SuspendLayout();
            splitDerecha.Panel2.SuspendLayout();
            splitDerecha.SuspendLayout();
            tabResultados.SuspendLayout();
            tabTraza.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTraza).BeginInit();
            tabMatriz.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMatriz).BeginInit();
            tabLista.SuspendLayout();
            SuspendLayout();

            splitPrincipal.Dock = DockStyle.Fill;
            splitPrincipal.FixedPanel = FixedPanel.Panel1;
            splitPrincipal.Location = new Point(0, 0);
            splitPrincipal.Name = "splitPrincipal";
            splitPrincipal.Panel1.Controls.Add(panelControles);
            splitPrincipal.Panel1MinSize = 310;
            splitPrincipal.Panel2.Controls.Add(splitDerecha);
            splitPrincipal.Panel2MinSize = 480;
            splitPrincipal.Size = new Size(1240, 740);
            splitPrincipal.SplitterDistance = 330;
            splitPrincipal.TabIndex = 0;

            panelControles.AutoScroll = true;
            panelControles.BackColor = Color.FromArgb(241, 245, 249);
            panelControles.Controls.Add(grpRed);
            panelControles.Controls.Add(grpAristas);
            panelControles.Controls.Add(grpNodos);
            panelControles.Controls.Add(grpRuta);
            panelControles.Controls.Add(lblTitulo);
            panelControles.Dock = DockStyle.Fill;
            panelControles.Location = new Point(0, 0);
            panelControles.Name = "panelControles";
            panelControles.Padding = new Padding(10);
            panelControles.Size = new Size(330, 740);
            panelControles.TabIndex = 0;

            lblTitulo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(15, 23, 42);
            lblTitulo.Location = new Point(12, 12);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(298, 28);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Red de Distribucion (Dijkstra)";

            grpRuta.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpRuta.Controls.Add(lblResultadoRuta);
            grpRuta.Controls.Add(btnCalcularDijkstra);
            grpRuta.Controls.Add(cmbDestino);
            grpRuta.Controls.Add(lblDestino);
            grpRuta.Controls.Add(cmbOrigen);
            grpRuta.Controls.Add(lblOrigen);
            grpRuta.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpRuta.Location = new Point(12, 48);
            grpRuta.Name = "grpRuta";
            grpRuta.Size = new Size(298, 210);
            grpRuta.TabIndex = 1;
            grpRuta.TabStop = false;
            grpRuta.Text = "Calculo de Ruta Optima";

            lblOrigen.AutoSize = true;
            lblOrigen.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            lblOrigen.Location = new Point(12, 26);
            lblOrigen.Name = "lblOrigen";
            lblOrigen.Size = new Size(46, 15);
            lblOrigen.TabIndex = 0;
            lblOrigen.Text = "Origen:";

            cmbOrigen.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cmbOrigen.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbOrigen.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            cmbOrigen.FormattingEnabled = true;
            cmbOrigen.Location = new Point(12, 44);
            cmbOrigen.Name = "cmbOrigen";
            cmbOrigen.Size = new Size(274, 23);
            cmbOrigen.TabIndex = 1;

            lblDestino.AutoSize = true;
            lblDestino.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            lblDestino.Location = new Point(12, 74);
            lblDestino.Name = "lblDestino";
            lblDestino.Size = new Size(50, 15);
            lblDestino.TabIndex = 2;
            lblDestino.Text = "Destino:";

            cmbDestino.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cmbDestino.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDestino.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            cmbDestino.FormattingEnabled = true;
            cmbDestino.Location = new Point(12, 92);
            cmbDestino.Name = "cmbDestino";
            cmbDestino.Size = new Size(274, 23);
            cmbDestino.TabIndex = 3;

            btnCalcularDijkstra.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnCalcularDijkstra.BackColor = Color.FromArgb(30, 90, 180);
            btnCalcularDijkstra.FlatStyle = FlatStyle.Flat;
            btnCalcularDijkstra.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnCalcularDijkstra.ForeColor = Color.White;
            btnCalcularDijkstra.Location = new Point(12, 124);
            btnCalcularDijkstra.Name = "btnCalcularDijkstra";
            btnCalcularDijkstra.Size = new Size(274, 32);
            btnCalcularDijkstra.TabIndex = 4;
            btnCalcularDijkstra.Text = "Calcular Ruta Optima";
            btnCalcularDijkstra.UseVisualStyleBackColor = false;
            btnCalcularDijkstra.Click += btnCalcularDijkstra_Click;

            lblResultadoRuta.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblResultadoRuta.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            lblResultadoRuta.ForeColor = Color.FromArgb(15, 23, 42);
            lblResultadoRuta.Location = new Point(12, 162);
            lblResultadoRuta.Name = "lblResultadoRuta";
            lblResultadoRuta.Size = new Size(274, 40);
            lblResultadoRuta.TabIndex = 5;
            lblResultadoRuta.Text = "Ruta: sin calcular";

            grpNodos.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpNodos.Controls.Add(btnEliminarNodo);
            grpNodos.Controls.Add(btnAgregarNodo);
            grpNodos.Controls.Add(txtNombreNodo);
            grpNodos.Controls.Add(lblNombreNodo);
            grpNodos.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpNodos.Location = new Point(12, 266);
            grpNodos.Name = "grpNodos";
            grpNodos.Size = new Size(298, 118);
            grpNodos.TabIndex = 2;
            grpNodos.TabStop = false;
            grpNodos.Text = "Gestion de Nodos";

            lblNombreNodo.AutoSize = true;
            lblNombreNodo.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            lblNombreNodo.Location = new Point(12, 24);
            lblNombreNodo.Name = "lblNombreNodo";
            lblNombreNodo.Size = new Size(106, 15);
            lblNombreNodo.TabIndex = 0;
            lblNombreNodo.Text = "Nombre de ciudad:";

            txtNombreNodo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtNombreNodo.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            txtNombreNodo.Location = new Point(12, 42);
            txtNombreNodo.Name = "txtNombreNodo";
            txtNombreNodo.Size = new Size(274, 23);
            txtNombreNodo.TabIndex = 1;

            btnAgregarNodo.BackColor = Color.FromArgb(16, 145, 110);
            btnAgregarNodo.FlatStyle = FlatStyle.Flat;
            btnAgregarNodo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAgregarNodo.ForeColor = Color.White;
            btnAgregarNodo.Location = new Point(12, 74);
            btnAgregarNodo.Name = "btnAgregarNodo";
            btnAgregarNodo.Size = new Size(132, 30);
            btnAgregarNodo.TabIndex = 2;
            btnAgregarNodo.Text = "Agregar Nodo";
            btnAgregarNodo.UseVisualStyleBackColor = false;
            btnAgregarNodo.Click += btnAgregarNodo_Click;

            btnEliminarNodo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnEliminarNodo.BackColor = Color.FromArgb(185, 60, 55);
            btnEliminarNodo.FlatStyle = FlatStyle.Flat;
            btnEliminarNodo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnEliminarNodo.ForeColor = Color.White;
            btnEliminarNodo.Location = new Point(154, 74);
            btnEliminarNodo.Name = "btnEliminarNodo";
            btnEliminarNodo.Size = new Size(132, 30);
            btnEliminarNodo.TabIndex = 3;
            btnEliminarNodo.Text = "Eliminar Nodo";
            btnEliminarNodo.UseVisualStyleBackColor = false;
            btnEliminarNodo.Click += btnEliminarNodo_Click;

            grpAristas.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpAristas.Controls.Add(btnEliminarArista);
            grpAristas.Controls.Add(btnAgregarArista);
            grpAristas.Controls.Add(numPeso);
            grpAristas.Controls.Add(lblPeso);
            grpAristas.Controls.Add(cmbAristaDestino);
            grpAristas.Controls.Add(lblAristaDestino);
            grpAristas.Controls.Add(cmbAristaOrigen);
            grpAristas.Controls.Add(lblAristaOrigen);
            grpAristas.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpAristas.Location = new Point(12, 392);
            grpAristas.Name = "grpAristas";
            grpAristas.Size = new Size(298, 214);
            grpAristas.TabIndex = 3;
            grpAristas.TabStop = false;
            grpAristas.Text = "Gestion de Rutas (Aristas)";

            lblAristaOrigen.AutoSize = true;
            lblAristaOrigen.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            lblAristaOrigen.Location = new Point(12, 24);
            lblAristaOrigen.Name = "lblAristaOrigen";
            lblAristaOrigen.Size = new Size(46, 15);
            lblAristaOrigen.TabIndex = 0;
            lblAristaOrigen.Text = "Origen:";

            cmbAristaOrigen.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cmbAristaOrigen.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAristaOrigen.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            cmbAristaOrigen.FormattingEnabled = true;
            cmbAristaOrigen.Location = new Point(12, 42);
            cmbAristaOrigen.Name = "cmbAristaOrigen";
            cmbAristaOrigen.Size = new Size(274, 23);
            cmbAristaOrigen.TabIndex = 1;

            lblAristaDestino.AutoSize = true;
            lblAristaDestino.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            lblAristaDestino.Location = new Point(12, 70);
            lblAristaDestino.Name = "lblAristaDestino";
            lblAristaDestino.Size = new Size(50, 15);
            lblAristaDestino.TabIndex = 2;
            lblAristaDestino.Text = "Destino:";

            cmbAristaDestino.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cmbAristaDestino.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAristaDestino.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            cmbAristaDestino.FormattingEnabled = true;
            cmbAristaDestino.Location = new Point(12, 88);
            cmbAristaDestino.Name = "cmbAristaDestino";
            cmbAristaDestino.Size = new Size(274, 23);
            cmbAristaDestino.TabIndex = 3;

            lblPeso.AutoSize = true;
            lblPeso.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            lblPeso.Location = new Point(12, 118);
            lblPeso.Name = "lblPeso";
            lblPeso.Size = new Size(88, 15);
            lblPeso.TabIndex = 4;
            lblPeso.Text = "Distancia (km):";

            numPeso.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            numPeso.DecimalPlaces = 1;
            numPeso.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            numPeso.Location = new Point(112, 116);
            numPeso.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numPeso.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numPeso.Name = "numPeso";
            numPeso.Size = new Size(174, 23);
            numPeso.TabIndex = 5;
            numPeso.Value = new decimal(new int[] { 150, 0, 0, 0 });

            btnAgregarArista.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnAgregarArista.BackColor = Color.FromArgb(51, 65, 85);
            btnAgregarArista.FlatStyle = FlatStyle.Flat;
            btnAgregarArista.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAgregarArista.ForeColor = Color.White;
            btnAgregarArista.Location = new Point(12, 146);
            btnAgregarArista.Name = "btnAgregarArista";
            btnAgregarArista.Size = new Size(274, 28);
            btnAgregarArista.TabIndex = 6;
            btnAgregarArista.Text = "Agregar o Actualizar Ruta";
            btnAgregarArista.UseVisualStyleBackColor = false;
            btnAgregarArista.Click += btnAgregarArista_Click;

            btnEliminarArista.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnEliminarArista.BackColor = Color.FromArgb(148, 163, 184);
            btnEliminarArista.FlatStyle = FlatStyle.Flat;
            btnEliminarArista.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnEliminarArista.ForeColor = Color.FromArgb(15, 23, 42);
            btnEliminarArista.Location = new Point(12, 178);
            btnEliminarArista.Name = "btnEliminarArista";
            btnEliminarArista.Size = new Size(274, 28);
            btnEliminarArista.TabIndex = 7;
            btnEliminarArista.Text = "Eliminar Ruta";
            btnEliminarArista.UseVisualStyleBackColor = false;
            btnEliminarArista.Click += btnEliminarArista_Click;

            grpRed.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpRed.Controls.Add(btnLimpiarGrafo);
            grpRed.Controls.Add(btnCargarEjemplo);
            grpRed.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpRed.Location = new Point(12, 614);
            grpRed.Name = "grpRed";
            grpRed.Size = new Size(298, 96);
            grpRed.TabIndex = 4;
            grpRed.TabStop = false;
            grpRed.Text = "Configuracion de Red";

            btnCargarEjemplo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnCargarEjemplo.BackColor = Color.FromArgb(15, 118, 110);
            btnCargarEjemplo.FlatStyle = FlatStyle.Flat;
            btnCargarEjemplo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCargarEjemplo.ForeColor = Color.White;
            btnCargarEjemplo.Location = new Point(12, 24);
            btnCargarEjemplo.Name = "btnCargarEjemplo";
            btnCargarEjemplo.Size = new Size(274, 28);
            btnCargarEjemplo.TabIndex = 0;
            btnCargarEjemplo.Text = "Restaurar Red Regional";
            btnCargarEjemplo.UseVisualStyleBackColor = false;
            btnCargarEjemplo.Click += btnCargarEjemplo_Click;

            btnLimpiarGrafo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnLimpiarGrafo.BackColor = Color.FromArgb(226, 232, 240);
            btnLimpiarGrafo.FlatStyle = FlatStyle.Flat;
            btnLimpiarGrafo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnLimpiarGrafo.ForeColor = Color.FromArgb(15, 23, 42);
            btnLimpiarGrafo.Location = new Point(12, 58);
            btnLimpiarGrafo.Name = "btnLimpiarGrafo";
            btnLimpiarGrafo.Size = new Size(274, 28);
            btnLimpiarGrafo.TabIndex = 1;
            btnLimpiarGrafo.Text = "Limpiar Grafo";
            btnLimpiarGrafo.UseVisualStyleBackColor = false;
            btnLimpiarGrafo.Click += btnLimpiarGrafo_Click;

            splitDerecha.Dock = DockStyle.Fill;
            splitDerecha.Location = new Point(0, 0);
            splitDerecha.Name = "splitDerecha";
            splitDerecha.Orientation = Orientation.Horizontal;
            splitDerecha.Panel1.Controls.Add(panelGrafo);
            splitDerecha.Panel1MinSize = 240;
            splitDerecha.Panel2.Controls.Add(tabResultados);
            splitDerecha.Panel2MinSize = 180;
            splitDerecha.Size = new Size(906, 740);
            splitDerecha.SplitterDistance = 420;
            splitDerecha.TabIndex = 0;

            panelGrafo.BackColor = Color.FromArgb(248, 250, 252);
            panelGrafo.BorderStyle = BorderStyle.FixedSingle;
            panelGrafo.Dock = DockStyle.Fill;
            panelGrafo.Location = new Point(0, 0);
            panelGrafo.Name = "panelGrafo";
            panelGrafo.Size = new Size(906, 420);
            panelGrafo.TabIndex = 0;
            panelGrafo.Paint += panelGrafo_Paint;
            panelGrafo.Resize += panelGrafo_Resize;

            tabResultados.Controls.Add(tabTraza);
            tabResultados.Controls.Add(tabMatriz);
            tabResultados.Controls.Add(tabLista);
            tabResultados.Dock = DockStyle.Fill;
            tabResultados.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            tabResultados.Location = new Point(0, 0);
            tabResultados.Name = "tabResultados";
            tabResultados.SelectedIndex = 0;
            tabResultados.Size = new Size(906, 316);
            tabResultados.TabIndex = 0;

            tabTraza.Controls.Add(dgvTraza);
            tabTraza.Location = new Point(4, 24);
            tabTraza.Name = "tabTraza";
            tabTraza.Padding = new Padding(4);
            tabTraza.Size = new Size(898, 288);
            tabTraza.TabIndex = 0;
            tabTraza.Text = "Traza de Dijkstra";
            tabTraza.UseVisualStyleBackColor = true;

            dgvTraza.AllowUserToAddRows = false;
            dgvTraza.AllowUserToDeleteRows = false;
            dgvTraza.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTraza.BackgroundColor = Color.White;
            dgvTraza.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTraza.Dock = DockStyle.Fill;
            dgvTraza.Location = new Point(4, 4);
            dgvTraza.Name = "dgvTraza";
            dgvTraza.ReadOnly = true;
            dgvTraza.RowHeadersVisible = false;
            dgvTraza.Size = new Size(890, 280);
            dgvTraza.TabIndex = 0;

            tabMatriz.Controls.Add(dgvMatriz);
            tabMatriz.Location = new Point(4, 24);
            tabMatriz.Name = "tabMatriz";
            tabMatriz.Padding = new Padding(4);
            tabMatriz.Size = new Size(898, 288);
            tabMatriz.TabIndex = 1;
            tabMatriz.Text = "Matriz de Adyacencia";
            tabMatriz.UseVisualStyleBackColor = true;

            dgvMatriz.AllowUserToAddRows = false;
            dgvMatriz.AllowUserToDeleteRows = false;
            dgvMatriz.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMatriz.BackgroundColor = Color.White;
            dgvMatriz.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMatriz.Dock = DockStyle.Fill;
            dgvMatriz.Location = new Point(4, 4);
            dgvMatriz.Name = "dgvMatriz";
            dgvMatriz.ReadOnly = true;
            dgvMatriz.Size = new Size(890, 280);
            dgvMatriz.TabIndex = 0;

            tabLista.Controls.Add(txtListaAdyacencia);
            tabLista.Location = new Point(4, 24);
            tabLista.Name = "tabLista";
            tabLista.Padding = new Padding(4);
            tabLista.Size = new Size(898, 288);
            tabLista.TabIndex = 2;
            tabLista.Text = "Lista de Adyacencia";
            tabLista.UseVisualStyleBackColor = true;

            txtListaAdyacencia.BackColor = Color.White;
            txtListaAdyacencia.Dock = DockStyle.Fill;
            txtListaAdyacencia.Font = new Font("Consolas", 10F, FontStyle.Regular);
            txtListaAdyacencia.Location = new Point(4, 4);
            txtListaAdyacencia.Multiline = true;
            txtListaAdyacencia.Name = "txtListaAdyacencia";
            txtListaAdyacencia.ReadOnly = true;
            txtListaAdyacencia.ScrollBars = ScrollBars.Both;
            txtListaAdyacencia.Size = new Size(890, 280);
            txtListaAdyacencia.TabIndex = 0;
            txtListaAdyacencia.WordWrap = false;

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1240, 740);
            Controls.Add(splitPrincipal);
            MinimumSize = new Size(980, 620);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Modelado y Recorrido de Grafos - Algoritmo de Dijkstra";

            splitPrincipal.Panel1.ResumeLayout(false);
            splitPrincipal.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitPrincipal).EndInit();
            splitPrincipal.ResumeLayout(false);
            panelControles.ResumeLayout(false);
            panelControles.PerformLayout();
            grpRed.ResumeLayout(false);
            grpAristas.ResumeLayout(false);
            grpAristas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numPeso).EndInit();
            grpNodos.ResumeLayout(false);
            grpNodos.PerformLayout();
            grpRuta.ResumeLayout(false);
            grpRuta.PerformLayout();
            splitDerecha.Panel1.ResumeLayout(false);
            splitDerecha.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitDerecha).EndInit();
            splitDerecha.ResumeLayout(false);
            tabResultados.ResumeLayout(false);
            tabTraza.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvTraza).EndInit();
            tabMatriz.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvMatriz).EndInit();
            tabLista.ResumeLayout(false);
            tabLista.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
    }
}
