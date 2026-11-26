namespace Modelado_y_recorrido_de_Grafos.Modelos
{
    public class NodoGrafo
    {
        public int Id { get; }
        public string Nombre { get; set; }
        public double PosX { get; set; }
        public double PosY { get; set; }

        public NodoGrafo(int id, string nombre, double posX, double posY)
        {
            Id = id;
            Nombre = nombre;
            PosX = posX;
            PosY = posY;
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
