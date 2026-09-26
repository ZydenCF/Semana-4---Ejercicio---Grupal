using System;

namespace Sabado_Semana_4_Ejercicio_Grupal
{
    public class Pocion : Objeto
    {
        public int CantidadCuracion { get; set; }

        public Pocion(string nombre, string descripcion, int cantidadCuracion)
            : base(nombre, descripcion)
        {
            CantidadCuracion = cantidadCuracion;
        }

        public override void Usar(Jugador jugador)
        {
            if (jugador == null) return;
            jugador.Curar(CantidadCuracion);
        }
    }
}