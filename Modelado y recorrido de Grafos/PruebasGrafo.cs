namespace Modelado_y_recorrido_de_Grafos
{
    public static class PruebasGrafo
    {
        public static int EjecutarTodas()
        {
            var pruebas = new List<(string Nombre, Action Accion)>
            {
                ("Carga de red regional y conteo de nodos/aristas", ProbarCargaRedEjemplo),
                ("Simetria y valores de matriz de adyacencia", ProbarMatrizAdyacencia),
                ("Lista de adyacencia consistente", ProbarListaAdyacencia),
                ("Dijkstra CDMX a Monterrey (925 km)", ProbarDijkstraCdmxMonterrey),
                ("Dijkstra Puebla a Guadalajara (675 km)", ProbarDijkstraPueblaGuadalajara),
                ("Nodo desconectado sin ruta", ProbarNodoDesconectado),
                ("Actualizacion de arista y eliminacion de nodo", ProbarEdicionGrafo)
            };

            int fallos = 0;
            foreach ((string nombre, Action accion) in pruebas)
            {
                try
                {
                    accion();
                    Console.WriteLine($"[PASS] {nombre}");
                }
                catch (Exception ex)
                {
                    fallos++;
                    Console.WriteLine($"[FAIL] {nombre}: {ex.Message}");
                }
            }

            Console.WriteLine($"Total: {pruebas.Count}, Exitosas: {pruebas.Count - fallos}, Fallidas: {fallos}");
            return fallos == 0 ? 0 : 1;
        }

        private static void Afirmar(bool condicion, string mensaje)
        {
            if (!condicion)
            {
                throw new InvalidOperationException(mensaje);
            }
        }

        private static void ProbarCargaRedEjemplo()
        {
            var grafo = new GrafoPonderado();
            grafo.CargarRedDistribucionEjemplo();

            Afirmar(grafo.Nodos.Count == 7, "Se esperaban 7 nodos.");
            Afirmar(grafo.Aristas.Count == 10, "Se esperaban 10 aristas.");
        }

        private static void ProbarMatrizAdyacencia()
        {
            var grafo = new GrafoPonderado();
            grafo.CargarRedDistribucionEjemplo();
            double[,] matriz = grafo.ObtenerMatrizAdyacencia();

            Afirmar(matriz.GetLength(0) == 7 && matriz.GetLength(1) == 7, "Dimensiones de matriz incorrectas.");
            for (int i = 0; i < 7; i++)
            {
                Afirmar(matriz[i, i] == 0, "La diagonal debe ser 0.");
                for (int j = 0; j < 7; j++)
                {
                    Afirmar(matriz[i, j] == matriz[j, i], "La matriz debe ser simetrica en grafo no dirigido.");
                }
            }
        }

        private static void ProbarListaAdyacencia()
        {
            var grafo = new GrafoPonderado();
            grafo.CargarRedDistribucionEjemplo();
            Dictionary<string, List<(string Destino, double Peso)>> lista = grafo.ObtenerListaAdyacencia();

            Afirmar(lista.Count == 7, "La lista debe contener 7 entradas.");
            Afirmar(lista["CDMX"].Count == 3, "CDMX debe tener 3 vecinos.");
            Afirmar(lista["Queretaro"].Count == 4, "Queretaro debe tener 4 vecinos.");
        }

        private static void ProbarDijkstraCdmxMonterrey()
        {
            var grafo = new GrafoPonderado();
            grafo.CargarRedDistribucionEjemplo();
            var dijkstra = new AlgoritmoDijkstra();

            ResultadoDijkstra res = dijkstra.Ejecutar(grafo, "CDMX", "Monterrey");

            Afirmar(res.ExisteRuta, "Debe existir ruta entre CDMX y Monterrey.");
            Afirmar(Math.Abs(res.DistanciaTotal - 925) < 0.001, $"Se esperaba 925 km y se obtuvo {res.DistanciaTotal}.");
            Afirmar(res.RutaOptima.SequenceEqual(new[] { "CDMX", "Queretaro", "Monterrey" }), "Ruta optima inesperada.");
            Afirmar(res.Traza.Count >= 4, "La traza debe registrar multiples pasos.");
        }

        private static void ProbarDijkstraPueblaGuadalajara()
        {
            var grafo = new GrafoPonderado();
            grafo.CargarRedDistribucionEjemplo();
            var dijkstra = new AlgoritmoDijkstra();

            ResultadoDijkstra res = dijkstra.Ejecutar(grafo, "Puebla", "Guadalajara");

            Afirmar(res.ExisteRuta, "Debe existir ruta entre Puebla y Guadalajara.");
            Afirmar(Math.Abs(res.DistanciaTotal - 675) < 0.001, $"Se esperaba 675 km y se obtuvo {res.DistanciaTotal}.");
            Afirmar(res.RutaOptima.SequenceEqual(new[] { "Puebla", "CDMX", "Guadalajara" }), "Ruta optima inesperada.");
        }

        private static void ProbarNodoDesconectado()
        {
            var grafo = new GrafoPonderado();
            grafo.CargarRedDistribucionEjemplo();
            grafo.AgregarNodo("Merida");
            var dijkstra = new AlgoritmoDijkstra();

            ResultadoDijkstra res = dijkstra.Ejecutar(grafo, "CDMX", "Merida");

            Afirmar(!res.ExisteRuta, "No debe existir ruta hacia un nodo aislado.");
            Afirmar(double.IsPositiveInfinity(res.DistanciaTotal), "La distancia hacia un nodo aislado debe ser infinita.");
        }

        private static void ProbarEdicionGrafo()
        {
            var grafo = new GrafoPonderado();
            grafo.CargarRedDistribucionEjemplo();
            grafo.AgregarArista("CDMX", "Monterrey", 500);
            var dijkstra = new AlgoritmoDijkstra();

            ResultadoDijkstra res = dijkstra.Ejecutar(grafo, "CDMX", "Monterrey");
            Afirmar(Math.Abs(res.DistanciaTotal - 500) < 0.001, "La ruta actualizada debe ser 500 km.");

            Afirmar(grafo.EliminarNodo("Monterrey"), "Debe eliminar Monterrey.");
            Afirmar(grafo.Nodos.Count == 6, "Deben quedar 6 nodos.");
        }
    }
}
