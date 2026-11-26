namespace Modelado_y_recorrido_de_Grafos.Algoritmos
{
    public class PasoDijkstra
    {
        public int NumeroPaso { get; }
        public string NodoActual { get; }
        public double DistanciaAcumulada { get; }
        public string Accion { get; }
        public IReadOnlyDictionary<string, double> DistanciasSnapshot { get; }
        public IReadOnlyDictionary<string, string?> PredecesoresSnapshot { get; }
        public IReadOnlyCollection<string> VisitadosSnapshot { get; }

        public PasoDijkstra(
            int numeroPaso,
            string nodoActual,
            double distanciaAcumulada,
            string accion,
            Dictionary<string, double> distancias,
            Dictionary<string, string?> predecesores,
            HashSet<string> visitados)
        {
            NumeroPaso = numeroPaso;
            NodoActual = nodoActual;
            DistanciaAcumulada = distanciaAcumulada;
            Accion = accion;
            DistanciasSnapshot = new Dictionary<string, double>(distancias, StringComparer.OrdinalIgnoreCase);
            PredecesoresSnapshot = new Dictionary<string, string?>(predecesores, StringComparer.OrdinalIgnoreCase);
            VisitadosSnapshot = visitados.ToList();
        }
    }
}
