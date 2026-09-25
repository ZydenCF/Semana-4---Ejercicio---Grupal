using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sábado___Semana_4___Ejercicio___Grupal
{
    public class Weapon : Item
    {
        public int DamageBonus { get; set; }

        public Weapon(string name, string description, int damageBonus)
            : base(name, description)
        {
            DamageBonus = damageBonus;
        }

        public override void Use()
        {
            Console.WriteLine(
                "Equipaste " + Name +
                ". Esta arma otorga +" +
                DamageBonus + " de dano."
            );
        }
    }
}