namespace Sabado_Semana_4_Ejercicio_Grupal
{
    public class NodoObjeto
    {
        public Objeto Objeto;
        public NodoObjeto Siguiente;

        public NodoObjeto(Objeto objeto)
        {
            Objeto = objeto;
            Siguiente = null;
        }
    }

    public class ListaEnlazadaObjetos
    {
        public NodoObjeto Cabeza;

        public void Agregar(Objeto objeto)
        {
            if (objeto == null) return;
            NodoObjeto nuevo = new NodoObjeto(objeto);
            if (Cabeza == null) { Cabeza = nuevo; return; }

            NodoObjeto actual = Cabeza;
            while (actual.Siguiente != null) actual = actual.Siguiente;
            actual.Siguiente = nuevo;
        }

        public Objeto Buscar(string nombre)
        {
            NodoObjeto actual = Cabeza;
            while (actual != null)
            {
                if (actual.Objeto.Nombre.Equals(nombre, System.StringComparison.OrdinalIgnoreCase))
                    return actual.Objeto;
                actual = actual.Siguiente;
            }
            return null;
        }

        public int Contar()
        {
            int c = 0;
            NodoObjeto actual = Cabeza;
            while (actual != null) { c++; actual = actual.Siguiente; }
            return c;
        }
    }
}