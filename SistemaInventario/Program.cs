using SistemaInventario;
using System;

class Program
{
    static void Main()
    {
        Inventario inventario = new Inventario();
        int opcion;

        Console.WriteLine("Bienvenido al sistema de gestión de inventario de ElectronicosAtenea");

        do
        {
            Console.WriteLine("|----------------------------------------------|");
            Console.WriteLine("| ¿Qué desea hacer?:                           |");
            Console.WriteLine("|                                              |");
            Console.WriteLine("| 1. Agregar productos al inventario           |");
            Console.WriteLine("| 2. Filtrar Productos                         |");
            Console.WriteLine("| 3. Ver inventario                            |");
            Console.WriteLine("| 4. Actualizar el precio de un producto       |");
            Console.WriteLine("| 5. Eliminar producto                         |");
            Console.WriteLine("| 6. Generar reporte resumido                  |");
            Console.WriteLine("| 7. Salir                                     |");
            Console.WriteLine("|----------------------------------------------|");


            // Leer opción
            if (!int.TryParse(Console.ReadLine(), out opcion))
            {
                Console.WriteLine("Por favor, ingrese un número válido.");
                continue; // Si la entrada no es un número, vuelve al menú
            }


            switch (opcion)
            {
                case 1:
                    // Agregar nuevos productos
                    AgregarProductos(inventario);
                    break;

                case 2:
                    // Filtrar por precio
                    FiltrarProductosPorPrecio(inventario);
                    break;

                case 3:
                    // Mostrar todos los productos
                    inventario.MostrarProductos();
                    break;
                    

                case 4:
                    // Actualizar el precio de un producto
                    ActualizarPrecioProducto(inventario);
                    break;

                case 5:
                    // Eliminar un producto
                    EliminarProducto(inventario);
                    break;

                case 6:
                    // Generar reporte resumido
                    inventario.GenerarReporte();
                    break;

                case 7:
                    // Salir del programa
                    Console.WriteLine("¡Gracias por usar el sistema de gestión de inventario! Hasta luego.");
                    break;

                default:
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                    break;
            }

            Console.WriteLine(); // Espacio para mayor claridad

        } while (opcion != 7); // El ciclo continúa mientras no se seleccione la opción 7 (Salir)
    }

    private static void AgregarProductos(Inventario inventario)
    {
        Console.WriteLine("¿Cuántos productos desea ingresar? ");
        int cantidad = LeerCantidadProductos();

        for (int i = 0; i < cantidad; i++)
        {
            Console.WriteLine($"\nProducto {i + 1}");
            string nombre = LeerNombre();
            double precio = LeerPrecio();

            Producto producto = new Producto(nombre, precio);
            inventario.AgregarProductos(producto);
        }
    }

    private static double LeerPrecio()
    {
        double precio;
        while (true)
        {
            Console.Write("Precio: ");
            if (double.TryParse(Console.ReadLine(), out precio) && precio > 0)
                return precio;
            else
                Console.WriteLine("¡Ups! Por favor, ingrese un precio válido (número positivo).");
        }
    }

    private static string LeerNombre()
    {
        string nombre;
        while (true)
        {
            Console.Write("Nombre: ");
            nombre = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nombre))
                return nombre;
            else
                Console.WriteLine("¡Atención! El nombre no puede estar vacío. Inténtelo nuevamente.");
        }
    }

    private static int LeerCantidadProductos()
    {
        int cantidad;
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out cantidad) && cantidad > 0)
                return cantidad;
            else
                Console.WriteLine("Lo siento, debe ingresar un número entero positivo para la cantidad de productos.");
        }
    }

    private static void FiltrarProductosPorPrecio(Inventario inventario)
    {
        double precioMinimo;

        // Ingresar el precio mínimo para el filtro
        Console.Write("\nIngrese el precio mínimo para filtrar los productos: ");
        precioMinimo = LeerPrecio();

        var productosFiltrados = inventario.FiltrarYOrdenarProductos(precioMinimo);

        // Mostrar los resultados filtrados
        if (!productosFiltrados.Any())
        {
            Console.WriteLine("No hay productos que cumplan con ese criterio.");
        }
        else
        {
            foreach (var producto in productosFiltrados)
            {
                Console.WriteLine(producto);
            }
        }
    }

    private static void ActualizarPrecioProducto(Inventario inventario)
    {
        if (LeerConfirmacion("\n¿Desea actualizar el precio de un producto? (s/n)"))
        {
            string nombreProducto = LeerNombre();
            double nuevoPrecio = LeerPrecio();
            inventario.ActualizarPrecio(nombreProducto, nuevoPrecio);
        }
    }

    private static void EliminarProducto(Inventario inventario)
    {
        if (LeerConfirmacion("\n¿Desea eliminar un producto? (s/n)"))
        {
            string nombreProducto = LeerNombre();
            inventario.EliminarProductoPorNombre(nombreProducto);
        }
    }

    private static bool LeerConfirmacion(string mensaje)
    {
        string respuesta;
        while (true)
        {
            Console.Write(mensaje);
            respuesta = Console.ReadLine().ToLower();
            if (respuesta == "s" || respuesta == "n")
                return respuesta == "s";
            else
                Console.WriteLine("Por favor, ingrese 's' para sí o 'n' para no.");
        }
    }
}