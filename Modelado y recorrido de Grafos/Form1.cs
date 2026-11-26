using System.Reflection;

namespace Modelado_y_recorrido_de_Grafos
{
    public partial class Form1 : Form
    {
        private readonly GrafoPonderado _grafo = new();
        private readonly AlgoritmoDijkstra _dijkstra = new();
        private readonly RenderizadorGrafo _renderizador = new();
        private ResultadoDijkstra? _ultimoResultado;
        private NodoGrafo? _nodoArrastrado;

        public Form1()
        {
            InitializeComponent();
            HabilitarDobleBuffer(panelGrafo);
            _grafo.CargarRedDistribucionEjemplo();
            ActualizarVistasGrafo();
            SeleccionarRutaInicial();
            CalcularRutaActual();
        }

        private static void HabilitarDobleBuffer(Control control)
        {
            PropertyInfo? prop = typeof(Control).GetProperty(
                "DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);
            prop?.SetValue(control, true);
        }

        private void SeleccionarRutaInicial()
        {
            if (cmbOrigen.Items.Contains("CDMX"))
            {
                cmbOrigen.SelectedItem = "CDMX";
            }
            if (cmbDestino.Items.Contains("Monterrey"))
            {
                cmbDestino.SelectedItem = "Monterrey";
            }
        }

        private void ActualizarVistasGrafo()
        {
            ActualizarCombosNodos();
            ActualizarMatrizAdyacencia();
            ActualizarListaAdyacencia();
            panelGrafo.Invalidate();
        }

        private void ActualizarCombosNodos()
        {
            string? origenSel = cmbOrigen.SelectedItem as string;
            string? destinoSel = cmbDestino.SelectedItem as string;
            string? aristaOrigenSel = cmbAristaOrigen.SelectedItem as string;
            string? aristaDestinoSel = cmbAristaDestino.SelectedItem as string;

            string[] nombres = _grafo.Nodos.Select(n => n.Nombre).ToArray();

            ActualizarCombo(cmbOrigen, nombres, origenSel);
            ActualizarCombo(cmbDestino, nombres, destinoSel);
            ActualizarCombo(cmbAristaOrigen, nombres, aristaOrigenSel);
            ActualizarCombo(cmbAristaDestino, nombres, aristaDestinoSel);
        }

        private static void ActualizarCombo(ComboBox combo, string[] items, string? previo)
        {
            combo.Items.Clear();
            combo.Items.AddRange(items);

            if (previo != null && combo.Items.Contains(previo))
            {
                combo.SelectedItem = previo;
            }
            else if (combo.Items.Count > 0)
            {
                combo.SelectedIndex = 0;
            }
            else
            {
                combo.SelectedIndex = -1;
            }
        }

        private void ActualizarMatrizAdyacencia()
        {
            dgvMatriz.Columns.Clear();
            dgvMatriz.Rows.Clear();

            int n = _grafo.Nodos.Count;
            if (n == 0)
            {
                return;
            }

            double[,] matriz = _grafo.ObtenerMatrizAdyacencia();

            for (int i = 0; i < n; i++)
            {
                dgvMatriz.Columns.Add($"col_{i}", _grafo.Nodos[i].Nombre);
                dgvMatriz.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            for (int i = 0; i < n; i++)
            {
                object[] fila = new object[n];
                for (int j = 0; j < n; j++)
                {
                    fila[j] = double.IsPositiveInfinity(matriz[i, j]) ? "INF" : $"{matriz[i, j]:0.##}";
                }
                int idx = dgvMatriz.Rows.Add(fila);
                dgvMatriz.Rows[idx].HeaderCell.Value = _grafo.Nodos[i].Nombre;
            }

            dgvMatriz.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
        }

        private void ActualizarListaAdyacencia()
        {
            txtListaAdyacencia.Text = _grafo.FormatearListaAdyacencia();
        }

        private void ActualizarTraza(ResultadoDijkstra resultado)
        {
            dgvTraza.Columns.Clear();
            dgvTraza.Rows.Clear();

            dgvTraza.Columns.Add("colPaso", "Paso");
            dgvTraza.Columns.Add("colNodo", "Nodo Actual");
            dgvTraza.Columns.Add("colCosto", "Costo Acumulado");
            dgvTraza.Columns.Add("colDistancias", "Vector de Distancias");
            dgvTraza.Columns.Add("colAccion", "Accion Realizada");

            dgvTraza.Columns[0].FillWeight = 10;
            dgvTraza.Columns[1].FillWeight = 16;
            dgvTraza.Columns[2].FillWeight = 16;
            dgvTraza.Columns[3].FillWeight = 34;
            dgvTraza.Columns[4].FillWeight = 34;

            foreach (PasoDijkstra paso in resultado.Traza)
            {
                string distanciasTexto = AlgoritmoDijkstra.FormatearEstadoDistancias(paso, _grafo.Nodos);
                dgvTraza.Rows.Add(
                    paso.NumeroPaso,
                    paso.NodoActual,
                    $"{paso.DistanciaAcumulada:0.##} km",
                    distanciasTexto,
                    paso.Accion);
            }
        }

        private void CalcularRutaActual()
        {
            if (cmbOrigen.SelectedItem is not string origen || cmbDestino.SelectedItem is not string destino)
            {
                _ultimoResultado = null;
                dgvTraza.Rows.Clear();
                lblResultadoRuta.Text = "Ruta: seleccione origen y destino.";
                panelGrafo.Invalidate();
                return;
            }

            try
            {
                _ultimoResultado = _dijkstra.Ejecutar(_grafo, origen, destino);
                ActualizarTraza(_ultimoResultado);

                if (_ultimoResultado.ExisteRuta)
                {
                    string camino = string.Join(" -> ", _ultimoResultado.RutaOptima);
                    lblResultadoRuta.Text = $"Ruta ({_ultimoResultado.DistanciaTotal:0.##} km): {camino}";
                }
                else
                {
                    lblResultadoRuta.Text = $"No existe ruta entre {origen} y {destino}.";
                }

                panelGrafo.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al calcular ruta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCalcularDijkstra_Click(object? sender, EventArgs e)
        {
            CalcularRutaActual();
        }

        private void btnAgregarNodo_Click(object? sender, EventArgs e)
        {
            try
            {
                _grafo.AgregarNodo(txtNombreNodo.Text);
                txtNombreNodo.Clear();
                _ultimoResultado = null;
                dgvTraza.Rows.Clear();
                lblResultadoRuta.Text = "Ruta: recalcular tras cambios.";
                ActualizarVistasGrafo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Nodo invalido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEliminarNodo_Click(object? sender, EventArgs e)
        {
            string nombre = txtNombreNodo.Text.Trim();
            if (string.IsNullOrEmpty(nombre) && cmbOrigen.SelectedItem is string seleccionado)
            {
                nombre = seleccionado;
            }

            if (string.IsNullOrEmpty(nombre))
            {
                MessageBox.Show("Escriba o seleccione el nodo a eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_grafo.EliminarNodo(nombre))
            {
                txtNombreNodo.Clear();
                _ultimoResultado = null;
                dgvTraza.Rows.Clear();
                lblResultadoRuta.Text = "Ruta: recalcular tras cambios.";
                ActualizarVistasGrafo();
            }
            else
            {
                MessageBox.Show($"No se encontro el nodo '{nombre}'.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAgregarArista_Click(object? sender, EventArgs e)
        {
            if (cmbAristaOrigen.SelectedItem is not string origen || cmbAristaDestino.SelectedItem is not string destino)
            {
                MessageBox.Show("Debe haber al menos dos nodos para crear una ruta.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                _grafo.AgregarArista(origen, destino, (double)numPeso.Value);
                _ultimoResultado = null;
                dgvTraza.Rows.Clear();
                lblResultadoRuta.Text = "Ruta: recalcular tras cambios.";
                ActualizarVistasGrafo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ruta invalida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEliminarArista_Click(object? sender, EventArgs e)
        {
            if (cmbAristaOrigen.SelectedItem is not string origen || cmbAristaDestino.SelectedItem is not string destino)
            {
                return;
            }

            if (_grafo.EliminarArista(origen, destino))
            {
                _ultimoResultado = null;
                dgvTraza.Rows.Clear();
                lblResultadoRuta.Text = "Ruta: recalcular tras cambios.";
                ActualizarVistasGrafo();
            }
            else
            {
                MessageBox.Show($"No existe ruta entre {origen} y {destino}.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCargarEjemplo_Click(object? sender, EventArgs e)
        {
            _grafo.CargarRedDistribucionEjemplo();
            ActualizarVistasGrafo();
            SeleccionarRutaInicial();
            CalcularRutaActual();
        }

        private void btnLimpiarGrafo_Click(object? sender, EventArgs e)
        {
            _grafo.Limpiar();
            _ultimoResultado = null;
            dgvTraza.Rows.Clear();
            lblResultadoRuta.Text = "Ruta: grafo vacio.";
            ActualizarVistasGrafo();
        }

        private void panelGrafo_Paint(object? sender, PaintEventArgs e)
        {
            _renderizador.Dibujar(e.Graphics, panelGrafo.ClientRectangle, _grafo, _ultimoResultado);
        }

        private void panelGrafo_Resize(object? sender, EventArgs e)
        {
            panelGrafo.Invalidate();
        }

        private void txtNombreNodo_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnAgregarNodo_Click(sender, e);
            }
        }

        private void panelGrafo_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _nodoArrastrado = _renderizador.ObtenerNodoEnPunto(panelGrafo.ClientRectangle, _grafo, e.Location);
            }
        }

        private void panelGrafo_MouseMove(object? sender, MouseEventArgs e)
        {
            if (_nodoArrastrado != null && e.Button == MouseButtons.Left)
            {
                _renderizador.ActualizarPosicionDesdePunto(panelGrafo.ClientRectangle, _nodoArrastrado, e.Location);
                panelGrafo.Invalidate();
            }
        }

        private void panelGrafo_MouseUp(object? sender, MouseEventArgs e)
        {
            _nodoArrastrado = null;
        }
    }
}
