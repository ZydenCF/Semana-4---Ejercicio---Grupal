namespace Sabado_Semana_4_Ejercicio_Grupal
{
    public interface IPersonaje
    {
        string Nombre { get; set; }
        int Vida { get; set; }
        bool EstaVivo { get; }
        void RecibirDanio(int cantidad);
    }
}