namespace Sábado___Semana_4___Ejercicio___Grupal
{
    public class Inventory
    {
        private readonly List<Item> _items = new List<Item>();

        public IReadOnlyList<Item> Items => _items;

        public void Add(Item item)
        {
            if (item == null) return;
            _items.Add(item);
            Console.WriteLine("Obtuviste: " + item.Name);
        }

        public void Show()
        {
            if (_items.Count == 0)
            {
                Console.WriteLine("Inventario vacio.");
                return;
            }

            Console.WriteLine("Inventario:");
            for (int i = 0; i < _items.Count; i++)
            {
                Console.WriteLine("  " + (i + 1) + ". " + _items[i].Name + " - " + _items[i].Description);
            }
        }

        public Item GetFirstPotion()
        {
            return _items.FirstOrDefault(i => i is Potion);
        }

        public void Remove(Item item)
        {
            _items.Remove(item);
        }

        public bool HasPotion() => _items.Any(i => i is Potion);
    }
}