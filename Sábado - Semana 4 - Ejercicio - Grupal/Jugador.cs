using System;

namespace Sabado_Semana_4_Ejercicio_Grupal
{
    public class Jugador : PersonajeBase
    {
        public int DanioBase { get; set; }
        public int Experiencia { get; set; }
        public int Nivel { get; set; }
        public int VidaMaxima { get; set; }
        public Inventario Inventario;

        public Jugador(string nombre, int vida) : base(nombre, vida)
        {
            VidaMaxima = vida;
            DanioBase = 10;
            Nivel = 1;
            Experiencia = 0;
            Inventario = new Inventario();
        }

        public override int Atacar(IPersonaje objetivo)
        {
            int danio = AleatorioSeguro(2, DanioBase + 1);
            objetivo.RecibirDanio(danio);
            return danio;
        }

        private int AyudanteAtaque(IPersonaje enemigo, int min, int reduccion)
        {
            int max = DanioBase - reduccion;
            int danio = AleatorioSeguro(min, max);
            enemigo.RecibirDanio(danio);
            return danio;
        }

        public void Ataque1(IPersonaje enemigo) => AyudanteAtaque(enemigo, 2, 2);
        public void Ataque2(IPersonaje enemigo) => AyudanteAtaque(enemigo, 5, 3);
        public void Ataque3(IPersonaje enemigo) => AyudanteAtaque(enemigo, 4, 2);
        public void Ataque4(IPersonaje enemigo) => AyudanteAtaque(enemigo, 3, 2);

        public void AumentarDanio()
        {
            DanioBase += 5;
            Console.WriteLine(Nombre + " aumento su danio base a " + DanioBase);
        }

        public void Curar(int cantidad)
        {
            if (cantidad <= 0) return;
            Vida = Math.Min(VidaMaxima, Vida + cantidad);
            Console.WriteLine(Nombre + " recupero vida. Vida actual: " + Vida + "/" + VidaMaxima);
        }

        public void GanarExperiencia(int xp)
        {
            Experiencia += xp;
            Console.WriteLine(Nombre + " gano " + xp + " XP (total: " + Experiencia + ")");
            while (Experiencia >= Nivel * 100)
            {
                SubirNivel();
            }
        }

        private void SubirNivel()
        {
            Nivel++;
            VidaMaxima += 20;
            Vida = VidaMaxima;
            DanioBase += 5;
            Console.WriteLine(Nombre + " subio al nivel " + Nivel + ". Vida: " + Vida + ", Danio base: " + DanioBase);
        }

        public void MostrarEstado()
        {
            Console.WriteLine("Vida: " + Vida + "/" + VidaMaxima +
                              " | Danio: " + DanioBase +
                              " | Nivel: " + Nivel +
                              " | XP: " + Experiencia);
        }
    }
}