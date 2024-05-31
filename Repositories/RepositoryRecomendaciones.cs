using Newtonsoft.Json;// Importa el espacio de nombres para el manejo de JSON
using System.Collections.ObjectModel; // Importa el espacio de nombres para colecciones observables
using proyectoCuento.Models; // Importa el espacio de nombres para los modelos de datos

namespace proyectoCuento; // Define el espacio de nombres para los repositorios
public class RepositoryRecomendaciones // Define la clase RepositoryRecomendaciones
{
    string urlApi = "https://proyectofinalcuentos-2f03.restdb.io/rest/recomendacione"; // URL de la API de recomendaciones
    HttpClient cliente = new HttpClient(); // Cliente HTTP para realizar solicitudes

    public RepositoryRecomendaciones() // Constructor de la clase
    {
        // Configura que el cliente trabaje con respuestas JSON
        cliente.DefaultRequestHeaders.Add("Accept", "application/json");
        cliente.DefaultRequestHeaders.Add("apikey", "65baae32f0b9f016bd9693bc"); // Agrega la clave de la API
    }

    public async Task<ObservableCollection<Recomendacione>> GetAllAsync() // Método para obtener todas las recomendaciones de forma asincrónica
    {
        try
        {
            var response = await cliente.GetStringAsync(urlApi); // Realiza una solicitud GET para obtener las recomendaciones
            return JsonConvert.DeserializeObject<ObservableCollection<Recomendacione>>(response); // Deserializa los datos JSON en una colección observable de recomendaciones
        }
        catch (Exception error)
        {
            await Application.Current.MainPage.DisplayAlert("error", "ubo un error:" + error.Message, "Ok"); // Muestra una alerta en caso de error
            return null; // Retorna nulo en caso de error
        }
    }
}