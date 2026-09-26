using System;

namespace Sabado_Semana_4_Ejercicio_Grupal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                new MotorJuego().Iniciar();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error critico: " + ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}