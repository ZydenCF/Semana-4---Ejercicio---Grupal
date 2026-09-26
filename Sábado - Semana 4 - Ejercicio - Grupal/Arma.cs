using System;

namespace Sabado_Semana_4_Ejercicio_Grupal
{
    public class Arma : Objeto
    {
        public int BonusDanio { get; set; }

        public Arma(string nombre, string descripcion, int bonusDanio)
            : base(nombre, descripcion)
        {
            BonusDanio = bonusDanio;
        }

        public override void Usar(Jugador jugador)
        {
            if (jugador == null) return;
            jugador.DanioBase += BonusDanio;
            Console.WriteLine("Equipaste " + Nombre + ". +" + BonusDanio + " de danio.");
        }
    }
}