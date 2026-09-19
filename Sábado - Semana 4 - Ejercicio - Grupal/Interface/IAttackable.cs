using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sábado___Semana_4___Ejercicio___Grupal
{
    public interface IAttackable
    {
        int Attack(ICharacter target);
        void ReceiveDamage(int amount);
    }

    public interface ICharacter
    {
        string Name { get; set; }
        int Life { get; set; }
        bool IsAlive { get; }
        void ReceiveDamage(int amount);
    }
}
