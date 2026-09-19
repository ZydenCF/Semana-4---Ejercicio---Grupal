using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sábado___Semana_4___Ejercicio___Grupal
{
    internal class Escenario : IMostrable
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Enemy EnemyInStage { get; set; }

        public Escenario NextOption1 { get; set; }
        public Escenario NextOption2 { get; set; }

        public Escenario(int id, string title, string description, Enemy enemy = null)
        {
            Id = id;
            Title = title;
            Description = description;
            EnemyInStage = enemy;
            NextOption1 = null;
            NextOption2 = null;
        }

        public void MostrarInfo()
        {
            Console.WriteLine(" ESCENARIO " + Id + ": " + Title);
            Console.WriteLine(Description);
        }
    }
}
