namespace Sabado_Semana_4_Ejercicio_Grupal
{
    public abstract class Objeto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public Objeto(string nombre, string descripcion)
        {
            Nombre = nombre;
            Descripcion = descripcion;
        }

        public abstract void Usar(Jugador jugador);
    }
}