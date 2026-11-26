using System.Drawing.Drawing2D;
using System.Drawing.Text;
using Modelado_y_recorrido_de_Grafos.Algoritmos;
using Modelado_y_recorrido_de_Grafos.Modelos;

namespace Modelado_y_recorrido_de_Grafos.Renderizado
{
    public class RenderizadorGrafo
    {
        public void Dibujar(Graphics g, Rectangle area, GrafoPonderado grafo, ResultadoDijkstra? resultado)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.Clear(Color.FromArgb(248, 250, 252));

            if (area.Width < 40 || area.Height < 40)
            {
                return;
            }

            if (grafo.Nodos.Count == 0)
            {
                using var fuenteVacio = new Font("Segoe UI", 11F, FontStyle.Regular);
                using var pincelVacio = new SolidBrush(Color.FromArgb(100, 116, 139));
                var formatoVacio = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                g.DrawString("Grafo vacio. Agregue nodos y rutas o presione Cargar Ejemplo.", fuenteVacio, pincelVacio, area, formatoVacio);
                return;
            }

            int margen = Math.Max(32, Math.Min(area.Width, area.Height) / 10);
            int anchoUtil = Math.Max(1, area.Width - (margen * 2));
            int altoUtil = Math.Max(1, area.Height - (margen * 2));
            int radio = Math.Clamp(Math.Min(area.Width, area.Height) / 22, 16, 26);

            var posiciones = new Dictionary<NodoGrafo, PointF>();
            foreach (NodoGrafo nodo in grafo.Nodos)
            {
                float x = area.Left + margen + (float)(nodo.PosX * anchoUtil);
                float y = area.Top + margen + (float)(nodo.PosY * altoUtil);
                posiciones[nodo] = new PointF(x, y);
            }

            var aristasOptimas = ObtenerAristasEnRuta(resultado);
            var nodosOptimos = resultado != null && resultado.ExisteRuta
                ? new HashSet<string>(resultado.RutaOptima, StringComparer.OrdinalIgnoreCase)
                : new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            using var plumaNormal = new Pen(Color.FromArgb(148, 163, 184), 2.2F);
            using var plumaOptima = new Pen(Color.FromArgb(16, 145, 110), 4.2F);
            using var fuentePeso = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            using var fondoPeso = new SolidBrush(Color.White);
            using var bordePeso = new Pen(Color.FromArgb(203, 213, 225), 1F);
            using var textoPesoNormal = new SolidBrush(Color.FromArgb(51, 65, 85));
            using var textoPesoOptimo = new SolidBrush(Color.FromArgb(6, 95, 70));

            foreach (AristaGrafo arista in grafo.Aristas)
            {
                PointF p1 = posiciones[arista.Origen];
                PointF p2 = posiciones[arista.Destino];
                bool esOptima = EsAristaOptima(arista, aristasOptimas);

                g.DrawLine(esOptima ? plumaOptima : plumaNormal, p1, p2);

                PointF medio = new((p1.X + p2.X) / 2F, (p1.Y + p2.Y) / 2F);
                string etiqueta = $"{arista.Peso:0.##} km";
                SizeF tamTexto = g.MeasureString(etiqueta, fuentePeso);
                var rectEtiqueta = new RectangleF(
                    medio.X - (tamTexto.Width / 2F) - 4F,
                    medio.Y - (tamTexto.Height / 2F) - 2F,
                    tamTexto.Width + 8F,
                    tamTexto.Height + 4F);

                g.FillRectangle(fondoPeso, rectEtiqueta);
                g.DrawRectangle(bordePeso, rectEtiqueta.X, rectEtiqueta.Y, rectEtiqueta.Width, rectEtiqueta.Height);
                g.DrawString(
                    etiqueta,
                    fuentePeso,
                    esOptima ? textoPesoOptimo : textoPesoNormal,
                    rectEtiqueta.X + 4F,
                    rectEtiqueta.Y + 2F);
            }

            using var fuenteNodo = new Font("Segoe UI", 9F, FontStyle.Bold);
            using var pincelTextoNombre = new SolidBrush(Color.FromArgb(15, 23, 42));
            using var pincelBordeNodo = new Pen(Color.White, 2.5F);
            var formatoCentro = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            foreach (NodoGrafo nodo in grafo.Nodos)
            {
                PointF centro = posiciones[nodo];
                Color colorNodo = ObtenerColorNodo(nodo.Nombre, resultado, nodosOptimos);

                var rectNodo = new RectangleF(
                    centro.X - radio,
                    centro.Y - radio,
                    radio * 2F,
                    radio * 2F);

                using var pincelNodo = new SolidBrush(colorNodo);
                g.FillEllipse(pincelNodo, rectNodo);
                g.DrawEllipse(pincelBordeNodo, rectNodo);

                string iniciales = nodo.Nombre.Length <= 3
                    ? nodo.Nombre.ToUpperInvariant()
                    : nodo.Nombre.Substring(0, 3).ToUpperInvariant();

                using var pincelInicial = new SolidBrush(Color.White);
                g.DrawString(iniciales, fuenteNodo, pincelInicial, rectNodo, formatoCentro);

                SizeF tamNombre = g.MeasureString(nodo.Nombre, fuenteNodo);
                g.DrawString(
                    nodo.Nombre,
                    fuenteNodo,
                    pincelTextoNombre,
                    centro.X - (tamNombre.Width / 2F),
                    centro.Y + radio + 4F);
            }
        }

        public NodoGrafo? ObtenerNodoEnPunto(Rectangle area, GrafoPonderado grafo, Point punto)
        {
            if (area.Width < 40 || area.Height < 40 || grafo.Nodos.Count == 0)
            {
                return null;
            }

            int margen = Math.Max(32, Math.Min(area.Width, area.Height) / 10);
            int anchoUtil = Math.Max(1, area.Width - (margen * 2));
            int altoUtil = Math.Max(1, area.Height - (margen * 2));
            int radio = Math.Clamp(Math.Min(area.Width, area.Height) / 22, 16, 26);

            foreach (NodoGrafo nodo in grafo.Nodos.Reverse())
            {
                float x = area.Left + margen + (float)(nodo.PosX * anchoUtil);
                float y = area.Top + margen + (float)(nodo.PosY * altoUtil);
                float dx = punto.X - x;
                float dy = punto.Y - y;
                if ((dx * dx) + (dy * dy) <= (radio + 4) * (radio + 4))
                {
                    return nodo;
                }
            }

            return null;
        }

        public void ActualizarPosicionDesdePunto(Rectangle area, NodoGrafo nodo, Point punto)
        {
            if (area.Width < 40 || area.Height < 40)
            {
                return;
            }

            int margen = Math.Max(32, Math.Min(area.Width, area.Height) / 10);
            int anchoUtil = Math.Max(1, area.Width - (margen * 2));
            int altoUtil = Math.Max(1, area.Height - (margen * 2));

            double normX = (punto.X - area.Left - margen) / (double)anchoUtil;
            double normY = (punto.Y - area.Top - margen) / (double)altoUtil;

            nodo.PosX = Math.Clamp(normX, 0.04, 0.96);
            nodo.PosY = Math.Clamp(normY, 0.06, 0.94);
        }

        private static HashSet<(string, string)> ObtenerAristasEnRuta(ResultadoDijkstra? resultado)
        {
            var conjunto = new HashSet<(string, string)>();
            if (resultado == null || !resultado.ExisteRuta || resultado.RutaOptima.Count < 2)
            {
                return conjunto;
            }

            for (int i = 0; i < resultado.RutaOptima.Count - 1; i++)
            {
                string a = resultado.RutaOptima[i].ToUpperInvariant();
                string b = resultado.RutaOptima[i + 1].ToUpperInvariant();
                conjunto.Add((a, b));
                conjunto.Add((b, a));
            }

            return conjunto;
        }

        private static bool EsAristaOptima(AristaGrafo arista, HashSet<(string, string)> aristasOptimas)
        {
            string a = arista.Origen.Nombre.ToUpperInvariant();
            string b = arista.Destino.Nombre.ToUpperInvariant();
            return aristasOptimas.Contains((a, b));
        }

        private static Color ObtenerColorNodo(string nombre, ResultadoDijkstra? resultado, HashSet<string> nodosOptimos)
        {
            if (resultado != null)
            {
                if (string.Equals(nombre, resultado.Origen, StringComparison.OrdinalIgnoreCase))
                {
                    return Color.FromArgb(30, 90, 180);
                }
                if (string.Equals(nombre, resultado.Destino, StringComparison.OrdinalIgnoreCase))
                {
                    return Color.FromArgb(200, 65, 55);
                }
            }

            if (nodosOptimos.Contains(nombre))
            {
                return Color.FromArgb(16, 145, 110);
            }

            return Color.FromArgb(51, 65, 85);
        }
    }
}
