namespace Modelado_y_recorrido_de_Grafos
{
    public class AristaGrafo
    {
        public NodoGrafo Origen { get; }
        public NodoGrafo Destino { get; }
        public double Peso { get; set; }

        public AristaGrafo(NodoGrafo origen, NodoGrafo destino, double peso)
        {
            if (peso <= 0)
            {
                throw new ArgumentException("El peso de la arista debe ser mayor que cero.");
            }

            Origen = origen;
            Destino = destino;
            Peso = peso;
        }
    }
}
