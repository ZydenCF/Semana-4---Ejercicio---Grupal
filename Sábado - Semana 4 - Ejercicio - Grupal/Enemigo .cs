using Sábado___Semana_4___Ejercicio___Grupal;
using System;

namespace Sabado_Semana_4_Ejercicio_Grupal
{
    public class Enemigo : PersonajeBase
    {
        private int Danio { get; set; }
        public int RecompensaExperiencia { get; set; }

        public Enemigo(string nombre, int vida, int danio, int recompensaXp)
            : base(nombre, vida)
        {
            Danio = danio;
            RecompensaExperiencia = recompensaXp;
        }

        public override int Atacar(IPersonaje objetivo)
        {
            return ElegirAtaqueAleatorio(objetivo);
        }

        private int AyudanteAtaque(IPersonaje jugador, int min, int reduccion)
        {
            int max = Danio - reduccion;
            int danio = AleatorioSeguro(min, max);
            jugador.RecibirDanio(danio);
            return danio;
        }

        public void Ataque1(IPersonaje jugador) => AyudanteAtaque(jugador, 3, 1);
        public void Ataque2(IPersonaje jugador) => AyudanteAtaque(jugador, 4, 2);
        public void Ataque3(IPersonaje jugador) => AyudanteAtaque(jugador, 3, 3);
        public void Ataque4(IPersonaje jugador) => AyudanteAtaque(jugador, 2, 1);

        public int ElegirAtaqueAleatorio(IPersonaje jugador)
        {
            int eleccion = Aleatorio.Next(1, 5);
            switch (eleccion)
            {
                case 1: Ataque1(jugador); break;
                case 2: Ataque2(jugador); break;
                case 3: Ataque3(jugador); break;
                default: Ataque4(jugador); break;
            }
            return eleccion;
        }
    }
}