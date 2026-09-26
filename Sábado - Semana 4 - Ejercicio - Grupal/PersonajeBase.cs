using System;

namespace Sabado_Semana_4_Ejercicio_Grupal
{
    public abstract class PersonajeBase : IPersonaje
    {
        protected static readonly Random Aleatorio = new Random();

        public string Nombre { get; set; }
        public int Vida { get; set; }
        public bool EstaVivo => Vida > 0;

        protected PersonajeBase(string nombre, int vida)
        {
            Nombre = nombre;
            Vida = vida;
        }

        public abstract int Atacar(IPersonaje objetivo);

        public virtual void RecibirDanio(int cantidad)
        {
            if (cantidad < 0) cantidad = 0;
            Vida = Math.Max(0, Vida - cantidad);
            Console.WriteLine(Nombre + " recibio " + cantidad + " de danio. Vida restante: " + Vida);
        }

        protected static int AleatorioSeguro(int min, int max)
        {
            if (max <= min) return Math.Max(min, 1);
            return Aleatorio.Next(min, max);
        }
    }
}