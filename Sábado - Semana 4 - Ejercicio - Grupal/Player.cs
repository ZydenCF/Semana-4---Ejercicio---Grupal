using System;
using ConsoleApp3.Interfaces;

namespace Sábado___Semana_4___Ejercicio___Grupal
{
    public class Player : CharacterBase
    {
        public int BaseDamage { get; set; }
        public int Experience { get; set; }
        public int Level { get; set; }
        public int MaxLife { get; set; }
        public Inventory Inventory;

        public Player(string name, int life) : base(name, life)
        {
            MaxLife = life;
            Inventory = new Inventory();
        }


        private void AttackHelper(ICharacter enemy, int min, int decrease)
        {
            int max = BaseDamage - decrease;
            int damage = SafeRandom(min, max);
            enemy.ReceiveDamage(damage);
        }

        public void Attack1(ICharacter enemy) => AttackHelper(enemy, 2, 2);
        public void Attack2(ICharacter enemy) => AttackHelper(enemy, 5, 3);
        public void Attack3(ICharacter enemy) => AttackHelper(enemy, 2, 2);
        public void Attack4(ICharacter enemy) => AttackHelper(enemy, 3, 2);

        public void DamageIncrease()
        {
            BaseDamage += 5;
            Console.WriteLine(Name + " aumento su dano base a " + BaseDamage);
        }

        public void Heal(int amount)
        {
            Life = Math.Min(MaxLife, Life + amount);
            Console.WriteLine(Name + " recupero vida. Vida actual: " + Life + "/" + MaxLife);
        }

        public void GainExperience(int xp)
        {
            Experience += xp;
            Console.WriteLine(Name + " gano " + xp + " XP (total: " + Experience + ")");

            if (Experience >= Level * 100)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            Level++;
            MaxLife += 20;
            Life = MaxLife;
            BaseDamage += 5;
            Console.WriteLine(Name + " subio al nivel " + Level + ". Vida: " + Life + ", Dano base: " + BaseDamage);
        }

        public void ShowStatus()
        {
            Console.WriteLine("Vida: " + Life + "/" + MaxLife + " | Dano: " + BaseDamage + " | Nivel: " + Level + " | XP: " + Experience);
        }
    }
}