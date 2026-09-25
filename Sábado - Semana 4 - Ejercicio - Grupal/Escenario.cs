using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sábado___Semana_4___Ejercicio___Grupal
{
    internal class Escenario : IMostrable
    {
        {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Enemy EnemyInStage { get; set; }
        public Item RewardItem { get; set; }
        public bool IsFinal { get; set; }

        public LinkedList<Option> Options { get; set; }

        public Stage(int id, string title, string description, Enemy enemy = null, Item rewardItem = null, bool isFinal = false)
        {
            Id = id;
            Title = title;
            Description = description;
            EnemyInStage = enemy;
            RewardItem = rewardItem;
            IsFinal = isFinal;
            Options = new LinkedList<Option>();
        }

        public void AddOption(Option option)
        {
            Options.AddLast(option);
        }

        public void MostrarInfo()
        {
            Console.WriteLine("=== ESCENARIO " + Id + ": " + Title + " ===");
            Console.WriteLine(Description);
        }
    }
}
