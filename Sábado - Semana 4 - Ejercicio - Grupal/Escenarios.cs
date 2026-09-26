using System;

namespace Sabado_Semana_4_Ejercicio_Grupal
{
    internal class Escenarios : IMostrable
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public Enemigo EnemigoEnEscena { get; set; }
        public TipoEvento Tipo { get; set; }
        public bool Visitado { get; set; }

        public Escenarios OpcionSiguiente1 { get; set; }
        public Escenarios OpcionSiguiente2 { get; set; }

        public Escenarios(int id, string titulo, string descripcion,
                         Enemigo enemigo = null,
                         TipoEvento tipo = TipoEvento.Normal)
        {
            Id = id;
            Titulo = titulo;
            Descripcion = descripcion;
            EnemigoEnEscena = enemigo;
            Tipo = tipo;
            Visitado = false;
            OpcionSiguiente1 = null;
            OpcionSiguiente2 = null;
        }

        public void MostrarInfo()
        {
            Console.WriteLine(" ESCENARIO " + Id + ": " + Titulo);
            Console.WriteLine(Descripcion);
        }
    }
}