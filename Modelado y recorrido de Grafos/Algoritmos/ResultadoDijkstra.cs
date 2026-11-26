namespace Modelado_y_recorrido_de_Grafos.Algoritmos
{
    public class ResultadoDijkstra
    {
        public string Origen { get; }
        public string Destino { get; }
        public IReadOnlyList<string> RutaOptima { get; }
        public double DistanciaTotal { get; }
        public bool ExisteRuta { get; }
        public IReadOnlyDictionary<string, double> DistanciasFinales { get; }
        public IReadOnlyDictionary<string, string?> PredecesoresFinales { get; }
        public IReadOnlyList<PasoDijkstra> Traza { get; }

        public ResultadoDijkstra(
            string origen,
            string destino,
            List<string> rutaOptima,
            double distanciaTotal,
            bool existeRuta,
            Dictionary<string, double> distanciasFinales,
            Dictionary<string, string?> predecesoresFinales,
            List<PasoDijkstra> traza)
        {
            Origen = origen;
            Destino = destino;
            RutaOptima = rutaOptima;
            DistanciaTotal = distanciaTotal;
            ExisteRuta = existeRuta;
            DistanciasFinales = new Dictionary<string, double>(distanciasFinales, StringComparer.OrdinalIgnoreCase);
            PredecesoresFinales = new Dictionary<string, string?>(predecesoresFinales, StringComparer.OrdinalIgnoreCase);
            Traza = traza;
        }
    }
}
