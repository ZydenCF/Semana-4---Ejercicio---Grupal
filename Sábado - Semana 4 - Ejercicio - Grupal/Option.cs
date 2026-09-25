using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sábado___Semana_4___Ejercicio___Grupal
{
    public class Option
    {
        public string Text { get; set; }

        public Action Action { get; set; }

        public Option(string text, Action action)
        {
            Text = text;
            Action = action;
        }

        public void Execute()
        {
            try
            {
                if (Action != null)
                {
                    Action();
                }
                else
                {
                    Console.WriteLine(
                        "Esta opcion no tiene una accion."
                    );
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "Error al ejecutar la opcion: " +
                    ex.Message
                );
            }
        }
    }
}
