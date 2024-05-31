namespace proyectoCuento.models; // Define el espacio de nombres para los modelos de datos

public class Cuento
{
    public string _id { get; set; } // Propiedad para el ID del cuento
    public string nombre { get; set; } // Propiedad para el nombre del cuento
    public string imagen_url { get; set; } // Propiedad para la URL de la imagen del cuento
    public int tiempodelectura { get; set; } // Propiedad para el tiempo de lectura del cuento
    public string historia { get; set; } // Propiedad para la historia del cuento
}