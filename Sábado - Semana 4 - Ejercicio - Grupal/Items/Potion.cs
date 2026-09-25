using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sábado___Semana_4___Ejercicio___Grupal
{
    public class Potion : Item
    {
        public int HealAmount { get; set; }

        public Potion(string name, string description, int healAmount)
            : base(name, description)
        {
            HealAmount = healAmount;
        }

        public override void Use()
        {
            Console.WriteLine(
                "Usaste " + Name +
                ". Esta pocion recupera " +
                HealAmount + " puntos de vida."
            );
        }
    }
}
