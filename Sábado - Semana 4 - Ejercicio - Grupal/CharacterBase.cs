using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sábado___Semana_4___Ejercicio___Grupal
{
    public abstract class CharacterBase : IAttackable, ICharacter
    {
        protected static readonly Random Random = new Random();

        public string Name { get; set; }
        public int Life { get; set; }
        public bool IsAlive => Life > 0;

        protected CharacterBase(string name, int life)
        {
            Name = name;
            Life = life;
        }

        public abstract int Attack(ICharacter target);

        public virtual void ReceiveDamage(int amount)
        {
            if (amount < 0) amount = 0;
            Life = Math.Max(0, Life - amount);
            Console.WriteLine(Name + " recibio " + amount + " de dano. Vida restante: " + Life);
        }

        protected static int SafeRandom(int min, int max)
        {
            if (max <= min) return Math.Max(min, 1);
            return Random.Next(min, max);
        }
    }
}
