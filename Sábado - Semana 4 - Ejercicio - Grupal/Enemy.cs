using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sábado___Semana_4___Ejercicio___Grupal
{
    public class Enemy : CharacterBase
    {
        private int Damage { get; set; }
        public int ExperienceReward { get; set; }

        public Enemy(string name, int life, int damage, int xpReward = 0)
            : base(name, life)
        {
            Damage = damage;
            ExperienceReward = xpReward;
        }

        public override int Attack(ICharacter target)
        {
            int dmg = SafeRandom(2, Damage);
            target.ReceiveDamage(dmg);
            return dmg;
        }

        private void AttackHelper(ICharacter player, int min, int decrease)
        {
            int max = Damage - decrease;
            int damage = SafeRandom(min, max);
            player.ReceiveDamage(damage);
        }

        public void Attack1(ICharacter player) => AttackHelper(player, 3, 1);
        public void Attack2(ICharacter player) => AttackHelper(player, 4, 2);
        public void Attack3(ICharacter player) => AttackHelper(player, 3, 3);
        public void Attack4(ICharacter player) => AttackHelper(player, 2, 1);

        public int ChooseRandomAttack(ICharacter player)
        {
            int choice = Random.Next(1, 5);
            switch (choice)
            {
                case 1: Attack1(player); break;
                case 2: Attack2(player); break;
                case 3: Attack3(player); break;
                default: Attack4(player); break;
            }
            return choice;
        }
    }
}
