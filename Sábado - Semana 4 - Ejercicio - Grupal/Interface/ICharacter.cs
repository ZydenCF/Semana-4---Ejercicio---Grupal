namespace Sábado___Semana_4___Ejercicio___Grupal
{
    public interface ICharacter
    {
        string Name { get; set; }
        int Life { get; set; }
        bool IsAlive { get; }
        void ReceiveDamage(int amount);   
    }
}