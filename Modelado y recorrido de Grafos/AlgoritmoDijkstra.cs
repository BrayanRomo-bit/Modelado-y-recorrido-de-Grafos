using System.Text;

namespace Modelado_y_recorrido_de_Grafos
{
    public class AlgoritmoDijkstra
    {
        public ResultadoDijkstra Ejecutar(GrafoPonderado grafo, string nombreOrigen, string nombreDestino)
        {
            NodoGrafo? origen = grafo.ObtenerNodo(nombreOrigen);
            NodoGrafo? destino = grafo.ObtenerNodo(nombreDestino);

            if (origen == null || destino == null)
            {
                throw new InvalidOperationException("El nodo origen y el nodo destino deben existir en el grafo.");
            }

            var distancias = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase);
            var predecesores = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase);
            var visitados = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var traza = new List<PasoDijkstra>();

            foreach (NodoGrafo nodo in grafo.Nodos)
            {
                distancias[nodo.Nombre] = double.PositiveInfinity;
                predecesores[nodo.Nombre] = null;
            }

            distancias[origen.Nombre] = 0;
            int paso = 0;

            traza.Add(new PasoDijkstra(
                paso++,
                origen.Nombre,
                0,
                $"Inicializacion: distancia({origen.Nombre}) = 0, resto = INF.",
                distancias,
                predecesores,
                visitados));

            while (visitados.Count < grafo.Nodos.Count)
            {
                NodoGrafo? actual = null;
                double menorDistancia = double.PositiveInfinity;

                foreach (NodoGrafo candidato in grafo.Nodos)
                {
                    if (!visitados.Contains(candidato.Nombre) && distancias[candidato.Nombre] < menorDistancia)
                    {
                        menorDistancia = distancias[candidato.Nombre];
                        actual = candidato;
                    }
                }

                if (actual == null || double.IsPositiveInfinity(menorDistancia))
                {
                    break;
                }

                visitados.Add(actual.Nombre);

                var actualizaciones = new List<string>();
                foreach ((NodoGrafo vecino, double peso) in grafo.ObtenerVecinos(actual))
                {
                    if (visitados.Contains(vecino.Nombre))
                    {
                        continue;
                    }

                    double nuevaDistancia = distancias[actual.Nombre] + peso;
                    if (nuevaDistancia < distancias[vecino.Nombre])
                    {
                        double anterior = distancias[vecino.Nombre];
                        distancias[vecino.Nombre] = nuevaDistancia;
                        predecesores[vecino.Nombre] = actual.Nombre;
                        string anteriorTexto = double.IsPositiveInfinity(anterior) ? "INF" : $"{anterior:0.##}";
                        actualizaciones.Add($"{vecino.Nombre}: {anteriorTexto} -> {nuevaDistancia:0.##} (via {actual.Nombre})");
                    }
                }

                string detalle = actualizaciones.Count > 0
                    ? $"Visita {actual.Nombre} ({menorDistancia:0.##} km). Relaja: {string.Join("; ", actualizaciones)}."
                    : $"Visita {actual.Nombre} ({menorDistancia:0.##} km). Sin mejoras en vecinos.";

                traza.Add(new PasoDijkstra(
                    paso++,
                    actual.Nombre,
                    menorDistancia,
                    detalle,
                    distancias,
                    predecesores,
                    visitados));

                if (actual == destino)
                {
                    break;
                }
            }

            var ruta = new List<string>();
            bool existeRuta = !double.IsPositiveInfinity(distancias[destino.Nombre]);

            if (existeRuta)
            {
                string? cursor = destino.Nombre;
                while (cursor != null)
                {
                    ruta.Add(cursor);
                    cursor = predecesores[cursor];
                }
                ruta.Reverse();
            }

            return new ResultadoDijkstra(
                origen.Nombre,
                destino.Nombre,
                ruta,
                distancias[destino.Nombre],
                existeRuta,
                distancias,
                predecesores,
                traza);
        }

        public static string FormatearEstadoDistancias(PasoDijkstra paso, IReadOnlyList<NodoGrafo> nodos)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < nodos.Count; i++)
            {
                string nombre = nodos[i].Nombre;
                double valor = paso.DistanciasSnapshot[nombre];
                string texto = double.IsPositiveInfinity(valor) ? "INF" : $"{valor:0.##}";
                sb.Append($"{nombre}={texto}");
                if (i < nodos.Count - 1)
                {
                    sb.Append(", ");
                }
            }
            return sb.ToString();
        }
    }
}
