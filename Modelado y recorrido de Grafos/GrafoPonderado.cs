using System.Text;

namespace Modelado_y_recorrido_de_Grafos
{
    public class GrafoPonderado
    {
        private readonly List<NodoGrafo> _nodos = new();
        private readonly List<AristaGrafo> _aristas = new();
        private int _siguienteId;

        public bool EsDirigido { get; set; }
        public IReadOnlyList<NodoGrafo> Nodos => _nodos;
        public IReadOnlyList<AristaGrafo> Aristas => _aristas;

        public GrafoPonderado(bool esDirigido = false)
        {
            EsDirigido = esDirigido;
        }

        public void Limpiar()
        {
            _nodos.Clear();
            _aristas.Clear();
            _siguienteId = 0;
        }

        public NodoGrafo? ObtenerNodo(string nombre)
        {
            return _nodos.FirstOrDefault(n => string.Equals(n.Nombre, nombre.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        public NodoGrafo AgregarNodo(string nombre, double posX = -1, double posY = -1)
        {
            string limpio = nombre.Trim();
            if (string.IsNullOrWhiteSpace(limpio))
            {
                throw new ArgumentException("El nombre del nodo no puede estar vacio.");
            }

            if (ObtenerNodo(limpio) != null)
            {
                throw new InvalidOperationException($"Ya existe un nodo llamado '{limpio}'.");
            }

            if (posX < 0 || posY < 0)
            {
                double angulo = (_nodos.Count * 2.0 * Math.PI) / Math.Max(6, _nodos.Count + 1);
                posX = 0.5 + 0.34 * Math.Cos(angulo);
                posY = 0.5 + 0.34 * Math.Sin(angulo);
            }

            var nodo = new NodoGrafo(_siguienteId++, limpio, Math.Clamp(posX, 0.08, 0.92), Math.Clamp(posY, 0.10, 0.90));
            _nodos.Add(nodo);
            return nodo;
        }

        public bool EliminarNodo(string nombre)
        {
            NodoGrafo? nodo = ObtenerNodo(nombre);
            if (nodo == null)
            {
                return false;
            }

            _aristas.RemoveAll(a => a.Origen == nodo || a.Destino == nodo);
            _nodos.Remove(nodo);
            return true;
        }

        public void AgregarArista(string nombreOrigen, string nombreDestino, double peso)
        {
            NodoGrafo? origen = ObtenerNodo(nombreOrigen);
            NodoGrafo? destino = ObtenerNodo(nombreDestino);

            if (origen == null || destino == null)
            {
                throw new InvalidOperationException("Tanto el nodo origen como el nodo destino deben existir.");
            }

            if (origen == destino)
            {
                throw new InvalidOperationException("No se permiten lazos sobre el mismo nodo.");
            }

            if (peso <= 0)
            {
                throw new ArgumentException("El peso debe ser un numero positivo.");
            }

            AristaGrafo? existente = _aristas.FirstOrDefault(a =>
                (a.Origen == origen && a.Destino == destino) ||
                (!EsDirigido && a.Origen == destino && a.Destino == origen));

            if (existente != null)
            {
                existente.Peso = peso;
                return;
            }

            _aristas.Add(new AristaGrafo(origen, destino, peso));
        }

        public bool EliminarArista(string nombreOrigen, string nombreDestino)
        {
            NodoGrafo? origen = ObtenerNodo(nombreOrigen);
            NodoGrafo? destino = ObtenerNodo(nombreDestino);

            if (origen == null || destino == null)
            {
                return false;
            }

            int eliminadas = _aristas.RemoveAll(a =>
                (a.Origen == origen && a.Destino == destino) ||
                (!EsDirigido && a.Origen == destino && a.Destino == origen));

            return eliminadas > 0;
        }

        public List<(NodoGrafo Vecino, double Peso)> ObtenerVecinos(NodoGrafo nodo)
        {
            var vecinos = new List<(NodoGrafo Vecino, double Peso)>();

            foreach (AristaGrafo arista in _aristas)
            {
                if (arista.Origen == nodo)
                {
                    vecinos.Add((arista.Destino, arista.Peso));
                }
                else if (!EsDirigido && arista.Destino == nodo)
                {
                    vecinos.Add((arista.Origen, arista.Peso));
                }
            }

            return vecinos.OrderBy(v => v.Vecino.Nombre, StringComparer.OrdinalIgnoreCase).ToList();
        }

        public double[,] ObtenerMatrizAdyacencia()
        {
            int n = _nodos.Count;
            double[,] matriz = new double[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matriz[i, j] = i == j ? 0 : double.PositiveInfinity;
                }
            }

            var indicePorNodo = new Dictionary<NodoGrafo, int>();
            for (int i = 0; i < n; i++)
            {
                indicePorNodo[_nodos[i]] = i;
            }

            foreach (AristaGrafo arista in _aristas)
            {
                int i = indicePorNodo[arista.Origen];
                int j = indicePorNodo[arista.Destino];
                matriz[i, j] = arista.Peso;
                if (!EsDirigido)
                {
                    matriz[j, i] = arista.Peso;
                }
            }

            return matriz;
        }

        public Dictionary<string, List<(string Destino, double Peso)>> ObtenerListaAdyacencia()
        {
            var lista = new Dictionary<string, List<(string Destino, double Peso)>>(StringComparer.OrdinalIgnoreCase);

            foreach (NodoGrafo nodo in _nodos)
            {
                var adyacentes = ObtenerVecinos(nodo)
                    .Select(v => (v.Vecino.Nombre, v.Peso))
                    .ToList();
                lista[nodo.Nombre] = adyacentes;
            }

            return lista;
        }

        public string FormatearListaAdyacencia()
        {
            var sb = new StringBuilder();
            Dictionary<string, List<(string Destino, double Peso)>> lista = ObtenerListaAdyacencia();

            foreach (KeyValuePair<string, List<(string Destino, double Peso)>> entrada in lista)
            {
                string conexiones = entrada.Value.Count == 0
                    ? "(sin conexiones)"
                    : string.Join(", ", entrada.Value.Select(v => $"{v.Destino} ({v.Peso:0.##} km)"));
                sb.AppendLine($"{entrada.Key} -> {conexiones}");
            }

            return sb.ToString();
        }

        public void CargarRedDistribucionEjemplo()
        {
            Limpiar();
            EsDirigido = false;

            AgregarNodo("CDMX", 0.48, 0.65);
            AgregarNodo("Puebla", 0.72, 0.72);
            AgregarNodo("Veracruz", 0.88, 0.62);
            AgregarNodo("Queretaro", 0.42, 0.42);
            AgregarNodo("Leon", 0.25, 0.34);
            AgregarNodo("Guadalajara", 0.14, 0.54);
            AgregarNodo("Monterrey", 0.48, 0.14);

            AgregarArista("CDMX", "Puebla", 130);
            AgregarArista("CDMX", "Queretaro", 215);
            AgregarArista("CDMX", "Guadalajara", 545);
            AgregarArista("Puebla", "Veracruz", 275);
            AgregarArista("Queretaro", "Leon", 170);
            AgregarArista("Queretaro", "Monterrey", 710);
            AgregarArista("Queretaro", "Veracruz", 530);
            AgregarArista("Leon", "Guadalajara", 220);
            AgregarArista("Leon", "Monterrey", 690);
            AgregarArista("Guadalajara", "Monterrey", 790);
        }
    }
}
