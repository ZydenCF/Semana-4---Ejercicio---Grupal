using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sábado___Semana_4___Ejercicio___Grupal
{
    public class Inventory
    {
        private readonly List<Item> _items = new List<Item>();

        private readonly Stack<Item> _history = new Stack<Item>();

        private readonly Queue<Item> _rewards = new Queue<Item>();

        public IReadOnlyList<Item> Items => _items;

        public void Add(Item item)
        {
            if (item == null)
            {
                Console.WriteLine("No se puede agregar un objeto vacio.");
                return;
            }

            _items.Add(item);
            _history.Push(item);

            Console.WriteLine("Obtuviste: " + item.Name);
        }

        public void Show()
        {
            Console.WriteLine("\n===== INVENTARIO =====");

            if (_items.Count == 0)
            {
                Console.WriteLine("Inventario vacio.");
                return;
            }

            for (int i = 0; i < _items.Count; i++)
            {
                Console.WriteLine(
                    (i + 1) + ". " +
                    _items[i].Name + " - " +
                    _items[i].Description
                );
            }
        }

        public void UseItem()
        {
            try
            {
                Show();

                if (_items.Count == 0)
                {
                    return;
                }

                Console.Write("\nSelecciona un objeto: ");

                int option = int.Parse(Console.ReadLine());

                if (option < 1 || option > _items.Count)
                {
                    Console.WriteLine("Esa opcion no existe.");
                    return;
                }

                Item selectedItem = _items[option - 1];

                selectedItem.Use();

                _items.Remove(selectedItem);
            }
            catch (FormatException)
            {
                Console.WriteLine(
                    "Error: debes escribir un numero."
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "Ocurrio un error: " + ex.Message
                );
            }
        }

        public Item Search(string name)
        {
            return _items.FirstOrDefault(
                item => item.Name.Equals(
                    name,
                    StringComparison.OrdinalIgnoreCase
                )
            );
        }

        public Item GetFirstPotion()
        {
            return _items.FirstOrDefault(
                item => item is Potion
            );
        }

        public bool HasPotion()
        {
            return _items.Any(
                item => item is Potion
            );
        }

        public void Remove(Item item)
        {
            if (item != null)
            {
                _items.Remove(item);
            }
        }

        public void ShowLastItem()
        {
            if (_history.Count == 0)
            {
                Console.WriteLine(
                    "No has recogido ningun objeto."
                );
                return;
            }

            Item lastItem = _history.Peek();

            Console.WriteLine(
                "Ultimo objeto recogido: " +
                lastItem.Name
            );
        }

        public void AddReward(Item item)
        {
            if (item == null)
            {
                return;
            }

            _rewards.Enqueue(item);

            Console.WriteLine(
                item.Name +
                " fue agregado a las recompensas."
            );
        }

        public void ClaimReward()
        {
            if (_rewards.Count == 0)
            {
                Console.WriteLine(
                    "No tienes recompensas pendientes."
                );
                return;
            }

            Item reward = _rewards.Dequeue();

            Add(reward);

            Console.WriteLine(
                "Recompensa reclamada: " +
                reward.Name
            );
        }
    }
}
