using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaInventario
{
    public class Inventario
    {
        private List<Producto> productos;

        public Inventario()
        {
            productos = new List<Producto>();
        }

        public void AgregarProductos(Producto producto)
        {
            productos.Add(producto);
            Console.WriteLine($"Producto '{producto.Nombre}' agregado exitosamente.");
        }

        public IEnumerable<Producto> FiltrarYOrdenarProductos(double precioMinimo)
        {
            // Filtrar y ordenar productos con LINQ
            return productos
                .Where(p => p.Precio > precioMinimo) // Filtra productos con precio mayor al mínimo especificado
                .OrderBy(p => p.Precio); // Ordena los productos de menor a mayor precio
        }

        public void ActualizarPrecio(string nombreProducto, double nuevoPrecio)
        {
            if (nuevoPrecio <= 0)
            {
                Console.WriteLine("El precio debe ser mayor que cero.");
                return;
            }

            var producto = productos.FirstOrDefault(p => p.Nombre.Equals(nombreProducto, StringComparison.OrdinalIgnoreCase));
            if (producto != null)
            {
                producto.Precio = nuevoPrecio;
                Console.WriteLine($"El precio del producto '{producto.Nombre}' ha sido actualizado a: {nuevoPrecio:C}.");
            }
            else
            {
                Console.WriteLine("Producto no encontrado.");
            }
        }

        public void MostrarProductos()
        {
            if (productos.Count == 0)
            {
                Console.WriteLine("No hay productos en el inventario.");
                return;
            }

            Console.WriteLine("Lista de productos:");
            foreach (var producto in productos)
            {
                Console.WriteLine($"- {producto.Nombre}: {producto.Precio:C}");
            }
        }

        public void EliminarProductoPorNombre(string nombre)
        {
            var producto = productos.FirstOrDefault(p => p.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));

            if (producto != null)
            {
                productos.Remove(producto);
                Console.WriteLine($"El producto '{nombre}' ha sido eliminado del inventario.");
            }
            else
            {
                Console.WriteLine("Producto no encontrado.");
            }
        }

        public void GenerarReporte()
        {
            int totalProductos = productos.Count;

            if (totalProductos == 0)
            {
                Console.WriteLine("No hay productos en el inventario para generar un reporte.");
                return;
            }

            double precioPromedio = productos.Average(p => p.Precio);

            var productoMasCaro = productos.OrderByDescending(p => p.Precio).FirstOrDefault();
            var productoMasBarato = productos.OrderBy(p => p.Precio).FirstOrDefault();

            // Mostrar reporte
            Console.WriteLine("\n--- Reporte Resumido del Inventario ---");
            Console.WriteLine($"Número total de productos: {totalProductos}");
            Console.WriteLine($"Precio promedio de todos los productos: {precioPromedio:C}");

            if (productoMasCaro != null)
                Console.WriteLine($"Producto más caro: {productoMasCaro.Nombre}, Precio: {productoMasCaro.Precio:C}");

            if (productoMasBarato != null)
                Console.WriteLine($"Producto más barato: {productoMasBarato.Nombre}, Precio: {productoMasBarato.Precio:C}");

            Console.WriteLine("--------------------------------------");
        }
    }
}