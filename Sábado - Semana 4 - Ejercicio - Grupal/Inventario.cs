using System;
using System.Collections.Generic;
using System.Linq;

namespace Sabado_Semana_4_Ejercicio_Grupal
{
    public class Inventario
    {
        private readonly List<Objeto> _objetos = new List<Objeto>();
        private readonly Stack<Objeto> _historial = new Stack<Objeto>();
        private readonly Queue<Objeto> _recompensas = new Queue<Objeto>();

        public IReadOnlyList<Objeto> Objetos => _objetos;

        public void Agregar(Objeto objeto)
        {
            if (objeto == null)
            {
                Console.WriteLine("No se puede agregar un objeto vacio.");
                return;
            }
            _objetos.Add(objeto);
            _historial.Push(objeto);
            Console.WriteLine("Obtuviste: " + objeto.Nombre);
        }

        public void Mostrar()
        {
            Console.WriteLine("\n===== INVENTARIO =====");
            if (_objetos.Count == 0)
            {
                Console.WriteLine("Inventario vacio.");
                return;
            }
            for (int i = 0; i < _objetos.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + _objetos[i].Nombre + " - " + _objetos[i].Descripcion);
            }
        }

        public void UsarObjeto(Jugador jugador)
        {
            try
            {
                Mostrar();
                if (_objetos.Count == 0) return;

                Console.Write("\nSelecciona un objeto: ");
                if (!int.TryParse(Console.ReadLine(), out int opcion))
                {
                    Console.WriteLine("Error: debes escribir un numero.");
                    return;
                }

                if (opcion < 1 || opcion > _objetos.Count)
                {
                    Console.WriteLine("Esa opcion no existe.");
                    return;
                }

                Objeto seleccionado = _objetos[opcion - 1];
                seleccionado.Usar(jugador);
                _objetos.Remove(seleccionado);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio un error: " + ex.Message);
            }
        }

        public Objeto Buscar(string nombre)
        {
            return _objetos.FirstOrDefault(
                objeto => objeto.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
        }

        public Objeto ObtenerPrimeraPocion()
        {
            return _objetos.FirstOrDefault(objeto => objeto is Pocion);
        }

        public bool TienePocion()
        {
            return _objetos.Any(objeto => objeto is Pocion);
        }

        public void Quitar(Objeto objeto)
        {
            if (objeto != null) _objetos.Remove(objeto);
        }

        public void MostrarUltimoObjeto()
        {
            if (_historial.Count == 0)
            {
                Console.WriteLine("No has recogido ningun objeto.");
                return;
            }
            Console.WriteLine("Ultimo objeto recogido: " + _historial.Peek().Nombre);
        }

        public void AgregarRecompensa(Objeto objeto)
        {
            if (objeto == null) return;
            _recompensas.Enqueue(objeto);
            Console.WriteLine(objeto.Nombre + " fue agregado a las recompensas.");
        }

        public void ReclamarRecompensa()
        {
            if (_recompensas.Count == 0)
            {
                Console.WriteLine("No tienes recompensas pendientes.");
                return;
            }
            Objeto recompensa = _recompensas.Dequeue();
            Agregar(recompensa);
            Console.WriteLine("Recompensa reclamada: " + recompensa.Nombre);
        }
    }
}