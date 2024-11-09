using System;

namespace SistemaInventario
{
    public class Producto
    {
        public string Nombre { get; set; }
        public double Precio { get; set; }

        public Producto(string nombre, double precio)
        {
            Nombre = nombre;
            Precio = precio;
        }

        public override string ToString()
        {
            return $"Nombre: {Nombre}, Precio: {Precio:C}";
        }
    }
}