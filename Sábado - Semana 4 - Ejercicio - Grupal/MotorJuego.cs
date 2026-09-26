using System;
using System.Collections.Generic;

namespace Sabado_Semana_4_Ejercicio_Grupal
{
    public class MotorJuego
    {
        private Jugador _jugador;
        private Escenarios _escenarioActual;
        private readonly Stack<string> _historialAcciones = new Stack<string>();
        private readonly Queue<string> _colaEventos = new Queue<string>();
        private readonly ListaEnlazadaObjetos _objetosRecogidos = new ListaEnlazadaObjetos();

        public void Iniciar()
        {
            try
            {
                Console.Title = "Aventura Bifurcada RPG";
                Console.WriteLine("=== AVENTURA DE TEXTO BIFURCADA ===\n");
                Console.Write("Ingresa el nombre de tu heroe: ");
                string nombre = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(nombre)) nombre = "Aventurero";

                _jugador = new Jugador(nombre, 100);
                _escenarioActual = ConstruirArbol();

                while (_escenarioActual != null)
                {
                    if (!_jugador.EstaVivo)
                    {
                        Console.WriteLine("\nHas muerto. Fin de la aventura.");
                        break;
                    }

                    _escenarioActual = JugarEscenario(_escenarioActual);
                }

                MostrarFinal();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error critico en el juego: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("\nPresiona ENTER para salir...");
                Console.ReadLine();
            }
        }

        private Escenarios ConstruirArbol()
        {
            var lobo = new Enemigo("Lobo Gigante", 40, 10, 60);
            var espectro = new Enemigo("Espectro", 50, 14, 80);
            var dragon = new Enemigo("Dragon Dorado", 120, 22, 300);
            var bandido = new Enemigo("Bandido", 35, 9, 50);

            var e1 = new Escenarios(1, "Aldea Inicial", "Despiertas en una aldea tranquila. Un anciano te saluda.", null, TipoEvento.Normal);
            var e2 = new Escenarios(2, "Bosque Sombrio", "El bosque susurra. Algo se mueve entre los arboles.", null, TipoEvento.Tesoro);
            var e3 = new Escenarios(3, "Cueva del Lobo", "Huesos por doquier. Un lobo gigante bloquea el paso.", lobo, TipoEvento.Combate);
            var e4 = new Escenarios(4, "Rio Encantado", "Un rio brilla. Descansas junto a sus aguas.", null, TipoEvento.Descanso);
            var e5 = new Escenarios(5, "Ruinas Antiguas", "Piedras antiguas con inscripciones magicas.", null, TipoEvento.Tesoro);
            var e6 = new Escenarios(6, "Torre del Mago", "Una torre alta con runas girando.", bandido, TipoEvento.Combate);
            var e7 = new Escenarios(7, "Cripta Olvidada", "Tumbas abiertas. Un espectro aparece.", espectro, TipoEvento.Combate);
            var e8 = new Escenarios(8, "Mercado Subterraneo", "Un mercado bullicioso en las profundidades.", null, TipoEvento.Tesoro);
            var e9 = new Escenarios(9, "Trono del Dragon", "El dragon dorado te observa desde su trono.", dragon, TipoEvento.Jefe);
            var e10 = new Escenarios(10, "Portal Final", "Un portal brillante te espera para decidir tu destino.", null, TipoEvento.Final);

            e1.OpcionSiguiente1 = e2;
            e1.OpcionSiguiente2 = e5;

            e2.OpcionSiguiente1 = e3;
            e2.OpcionSiguiente2 = e4;

            e5.OpcionSiguiente1 = e6;
            e5.OpcionSiguiente2 = e7;

            e4.OpcionSiguiente1 = e8;
            e6.OpcionSiguiente1 = e8;
            e7.OpcionSiguiente1 = e8;

            e8.OpcionSiguiente1 = e9;
            e9.OpcionSiguiente1 = e10;

            return e1;
        }

        private Escenarios JugarEscenario(Escenarios esc)
        {
            Console.WriteLine("\n" + new string('-', 50));
            esc.MostrarInfo();
            _jugador.MostrarEstado();

            ProcesarEvento(esc);

            if (!_jugador.EstaVivo) return null;

            ModificarArbolEnTiempoReal(esc);

            var opciones = new List<(string Texto, Escenarios Destino)>();

            if (esc.OpcionSiguiente1 != null) opciones.Add(("Ir al camino 1: " + esc.OpcionSiguiente1.Titulo, esc.OpcionSiguiente1));
            if (esc.OpcionSiguiente2 != null) opciones.Add(("Ir al camino 2: " + esc.OpcionSiguiente2.Titulo, esc.OpcionSiguiente2));
            opciones.Add(("Abrir inventario", null));
            opciones.Add(("Ver historial de acciones", null));

            for (int i = 0; i < opciones.Count; i++)
                Console.WriteLine("  [" + (i + 1) + "] " + opciones[i].Texto);

            Console.Write("Tu eleccion: ");
            if (!int.TryParse(Console.ReadLine(), out int sel) || sel < 1 || sel > opciones.Count)
            {
                Console.WriteLine("Opcion invalida. Se elige la primera por defecto.");
                sel = 1;
            }

            var elegida = opciones[sel - 1];

            if (elegida.Destino == null)
            {
                if (elegida.Texto.StartsWith("Abrir"))
                    _jugador.Inventario.UsarObjeto(_jugador);
                else
                    MostrarHistorial();
                return esc;
            }

            _historialAcciones.Push("Fuiste a: " + elegida.Destino.Titulo);
            _colaEventos.Enqueue("Visitado: " + elegida.Destino.Titulo);

            return elegida.Destino;
        }

        private void ProcesarEvento(Escenarios esc)
        {
            if (esc.Visitado && esc.Tipo != TipoEvento.Combate) return;
            esc.Visitado = true;

            switch (esc.Tipo)
            {
                case TipoEvento.Combate:
                case TipoEvento.Jefe:
                    if (esc.EnemigoEnEscena != null && esc.EnemigoEnEscena.EstaVivo)
                        ResolverCombate(esc.EnemigoEnEscena);
                    break;

                case TipoEvento.Tesoro:
                    EntregarTesoro(esc);
                    break;

                case TipoEvento.Trampa:
                    int danio = AleatorioLocal(5, 20);
                    _jugador.RecibirDanio(danio);
                    Console.WriteLine("Trampa! Pierdes " + danio + " HP.");
                    break;

                case TipoEvento.Descanso:
                    _jugador.Curar(_jugador.VidaMaxima);
                    Console.WriteLine("Descansas y recuperas toda tu vida.");
                    break;

                case TipoEvento.Final:
                    Console.WriteLine("Has llegado al portal final.");
                    break;

                case TipoEvento.Normal:
                default:
                    Console.WriteLine("El camino continua...");
                    break;
            }

            _colaEventos.Enqueue("Evento procesado en: " + esc.Titulo);
        }

        private void EntregarTesoro(Escenarios esc)
        {
            Objeto regalo = null;
            switch (esc.Id)
            {
                case 2: regalo = new Pocion("Pocion Menor", "Cura 30 HP", 30); break;
                case 5: regalo = new Arma("Espada Oxidada", "+5 danio", 5); break;
                case 8: regalo = new Pocion("Pocion Mayor", "Cura 60 HP", 60); break;
            }

            if (regalo == null)
            {
                Console.WriteLine("Buscas pero no encuentras nada.");
                return;
            }

            try
            {
                _jugador.Inventario.Agregar(regalo);
                _objetosRecogidos.Agregar(regalo);
            }
            catch (Exception ex)
            {
                Console.WriteLine("No pudiste guardar el objeto: " + ex.Message);
            }
        }

        private void ResolverCombate(Enemigo enemigo)
        {
            Console.WriteLine("\nCombate contra " + enemigo.Nombre + "!");
            while (_jugador.EstaVivo && enemigo.EstaVivo)
            {
                Console.WriteLine("\n1) Atacar  2) Habilidad fuerte  3) Usar inventario  4) Huir");
                Console.Write("Accion: ");

                if (!int.TryParse(Console.ReadLine(), out int op) || op < 1 || op > 4)
                {
                    Console.WriteLine("Opcion invalida.");
                    continue;
                }

                switch (op)
                {
                    case 1: _jugador.Atacar(enemigo); break;
                    case 2: _jugador.Ataque2(enemigo); break;
                    case 3: _jugador.Inventario.UsarObjeto(_jugador); break;
                    case 4:
                        Console.WriteLine("Huyes del combate...");
                        return;
                }

                if (enemigo.EstaVivo)
                {
                    enemigo.ElegirAtaqueAleatorio(_jugador);
                }
            }

            if (_jugador.EstaVivo)
            {
                Console.WriteLine("Has vencido a " + enemigo.Nombre + "!");
                _jugador.GanarExperiencia(enemigo.RecompensaExperiencia);
                _historialAcciones.Push("Venciste a " + enemigo.Nombre);
            }
        }


        private void ModificarArbolEnTiempoReal(Escenarios esc)
        {
            if (esc.Id == 3 &&
                _jugador.Inventario.Buscar("Espada Oxidada") != null &&
                esc.OpcionSiguiente2 == null)
            {
                Console.WriteLine("Con la Espada Oxidada encuentras un atajo secreto!");
   
            }
        }
        private void MostrarHistorial()
        {
            Console.WriteLine("\nHistorial de acciones:");
            if (_historialAcciones.Count == 0)
            {
                Console.WriteLine("  (vacio)");
                return;
            }
            foreach (var accion in _historialAcciones)
                Console.WriteLine("  - " + accion);

            Console.WriteLine("Eventos registrados: " + _colaEventos.Count);
        }

        private void MostrarFinal()
        {
            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("FIN DE LA AVENTURA");
            _jugador.MostrarEstado();
            Console.WriteLine("Objetos recogidos (lista enlazada): " + _objetosRecogidos.Contar());
            Console.WriteLine("Eventos procesados (cola): " + _colaEventos.Count);
            Console.WriteLine(new string('=', 50));
        }

        private static readonly Random _aleatorioLocal = new Random();
        private static int AleatorioLocal(int min, int max)
        {
            if (max <= min) return min;
            return _aleatorioLocal.Next(min, max);
        }
    }
}