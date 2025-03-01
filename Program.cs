using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        // Definir las rutas de las carpetas y archivos
        string Laboratorio = "LaboratorioAvengers";
        string ArchivoInventos = Path.Combine(Laboratorio, "inventos.txt");
        string Backup = Path.Combine(Laboratorio, "Backup");
        string ArchivosClasificados = Path.Combine(Laboratorio, "ArchivosClasificados");
        string ProyectosSecretos = Path.Combine(Laboratorio, "ProyectosSecretos");

        // Crear la carpeta LaboratorioAvengers si no existe
        CrearCarpeta(Laboratorio);

        // Menú principal del programa
        while (true)
        {
            Console.WriteLine("\n--- Menú de Operaciones ---");
            Console.WriteLine("1. Crear archivo 'inventos.txt'");
            Console.WriteLine("2. Agregar un invento");
            Console.WriteLine("3. Leer archivo línea por línea");
            Console.WriteLine("4. Leer todo el contenido del archivo");
            Console.WriteLine("5. Copiar archivo a 'Backup'");
            Console.WriteLine("6. Mover archivo a 'ArchivosClasificados'");
            Console.WriteLine("7. Crear carpeta 'ProyectosSecretos'");
            Console.WriteLine("8. Listar archivos en 'LaboratorioAvengers'");
            Console.WriteLine("9. Eliminar archivo 'inventos.txt'");
            Console.WriteLine("10. Salir");
            Console.Write("Selecciona una opción: ");

            string opcion = Console.ReadLine();

            // Manejar la opción seleccionada por el usuario
            switch (opcion)
            {
                case "1":
                    CrearArchivo(ArchivoInventos);
                    break;
                case "2":
                    Console.Write("Ingresa el nombre del invento: ");
                    string invento = Console.ReadLine();
                    AgregarInvento(ArchivoInventos, invento);
                    break;
                case "3":
                    LeerLineaPorLinea(ArchivoInventos);
                    break;
                case "4":
                    LeerTodoElTexto(ArchivoInventos);
                    break;
                case "5":
                    CopiarArchivo(ArchivoInventos, Path.Combine(Backup, "inventos.txt"));
                    break;
                case "6":
                    MoverArchivo(ArchivoInventos, Path.Combine(ArchivosClasificados, "inventos.txt"));
                    break;
                case "7":
                    CrearCarpeta(ProyectosSecretos);
                    break;
                case "8":
                    ListarArchivos(Laboratorio);
                    break;
                case "9":
                    EliminarArchivo(ArchivoInventos);
                    break;
                case "10":
                    Console.WriteLine("Gracias por ayudar a Tony Stark");
                    return;
                default:
                    Console.WriteLine("Opción no válida. Intenta de nuevo.");
                    break;
            }
        }
    }

    // Método para crear un archivo si no existe
    static void CrearArchivo(string rutaArchivo)
    {
        try
        {
            if (!File.Exists(rutaArchivo))
            {
                File.Create(rutaArchivo).Close();
                Console.WriteLine($"Archivo '{rutaArchivo}' creado exitosamente.");
            }
            else
            {
                Console.WriteLine($"El archivo '{rutaArchivo}' ya existe.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    // Método para agregar un invento al archivo
    static void AgregarInvento(string rutaArchivo, string invento)
    {
        try
        {
            using (StreamWriter sw = File.AppendText(rutaArchivo))
            {
                sw.WriteLine(invento);
                Console.WriteLine($"Invento '{invento}' agregado exitosamente.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    // Método para leer el archivo línea por línea
    static void LeerLineaPorLinea(string rutaArchivo)
    {
        try
        {
            if (File.Exists(rutaArchivo))
            {
                Console.WriteLine("\nInventos:");
                using (StreamReader sr = new StreamReader(rutaArchivo))
                {
                    string linea;
                    while ((linea = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(linea);   
                    }
                }
            }
            else
            {
                Console.WriteLine($"Error: El archivo '{rutaArchivo}' no existe. ¡Ultron debe haberlo borrado!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    // Método para leer todo el contenido del archivo
    static void LeerTodoElTexto(string rutaArchivo)
    {
        try
        {
            if (File.Exists(rutaArchivo))
            {
                string contenido = File.ReadAllText(rutaArchivo);
                Console.WriteLine("\nContenido completo del archivo:");
                Console.WriteLine(contenido);
            }
            else
            {
                Console.WriteLine($"Error: El archivo '{rutaArchivo}' no existe. ¡Ultron debe haberlo borrado!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    // Método para copiar un archivo a otra ubicación
    static void CopiarArchivo(string origen, string destino)
    {
        try
        {
            CrearCarpeta(Path.GetDirectoryName(destino));
            File.Copy(origen, destino, true);
            Console.WriteLine($"Archivo copiado a '{destino}' exitosamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    // Método para mover un archivo a otra ubicación
    static void MoverArchivo(string origen, string destino)
    {
        try
        {
            CrearCarpeta(Path.GetDirectoryName(destino));
            File.Move(origen, destino);
            Console.WriteLine($"Archivo movido a '{destino}' exitosamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    // Método para crear una carpeta si no existe
    static void CrearCarpeta(string rutaCarpeta)
    {
        try
        {
            if (!Directory.Exists(rutaCarpeta))
            {
                Directory.CreateDirectory(rutaCarpeta);
                Console.WriteLine($"Carpeta '{rutaCarpeta}' creada exitosamente.");
            }
            else
            {
                Console.WriteLine($"La carpeta '{rutaCarpeta}' ya existe.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    // Método para listar los archivos en una carpeta
    static void ListarArchivos(string Carpeta)
    {
        try
        {
            if (Directory.Exists(Carpeta))
            {
                string[] archivos = Directory.GetFiles(Carpeta);
                Console.WriteLine("\nArchivos en la carpeta:");
                foreach (string archivo in archivos)
                {
                    Console.WriteLine(Path.GetFileName(archivo));
                }
            }
            else
            {
                Console.WriteLine($"La carpeta '{Carpeta}' no existe.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    // Método para eliminar un archivo
    static void EliminarArchivo(string Archivo)
    {
        try
        {
            if (File.Exists(Archivo))
            {
                File.Delete(Archivo);
                Console.WriteLine($"Archivo '{Archivo}' eliminado exitosamente.");
            }
            else
            {
                Console.WriteLine($"El archivo '{Archivo}' no existe.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}