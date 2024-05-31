using Newtonsoft.Json;// Importa el espacio de nombres para el manejo de JSON
using proyectoCuento.models;// Importa el espacio de nombres para los modelos de datos
using System.Collections.ObjectModel;
using System.Text; // Importa el espacio de nombres para manipulación de cadenas

namespace proyectoCuento.Repositories;
    public class RepositoryCuentos
    {
        string urlApi = "https://proyectofinalcuentos-2f03.restdb.io/rest/cuentos"; // URL de la API de cuentos
    HttpClient cliente = new HttpClient(); // Cliente HTTP para realizar solicitudes

        public RepositoryCuentos() // Constructor de la clase
    {
            cliente.DefaultRequestHeaders.Add("Accept", "application/json"); //configuramos que trabajará con respuestas JSON
        cliente.DefaultRequestHeaders.Add("apikey", "65baae32f0b9f016bd9693bc"); // Agrega la clave de la API
    }

        public async Task<ObservableCollection<Cuento>> GetAllAsync() // Método para obtener todos los cuentos de forma asincrónica
    {
            try
            {
                var response = await cliente.GetStringAsync(urlApi); // Realiza una solicitud GET para obtener los cuentos
            return
                JsonConvert.DeserializeObject<ObservableCollection<Cuento>>(response); // Deserializa los datos JSON en una colección observable de cuentos
        }

            catch (Exception error) 
        {
                await Application.Current.MainPage.DisplayAlert(
                "error", "ubo un error:" + error.Message, "Ok"); // Muestra una alerta en caso de error
            return null;  // Retorna nulo en caso de error
        }
        }

    public async Task<bool> RemoveAsync(string id)
    {
        // Método para eliminar un cuento de la base de datos mediante su ID

        // Envía una solicitud DELETE al endpoint de la API con el ID del cuento a eliminar
        var response = await cliente.DeleteAsync($"{urlApi}/{id}");

        // Retorna true si la solicitud fue exitosa (código de estado HTTP 200-299), de lo contrario, false
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> AddAsynk(Cuento cuento)
    {
        // Método para agregar un nuevo cuento a la base de datos

        // Serializa el objeto 'cuento' a formato JSON para enviarlo en el cuerpo de la solicitud POST
        var content = new StringContent(JsonConvert.SerializeObject(cuento), Encoding.UTF8, "application/json");

        // Envía una solicitud POST al endpoint de la API con el objeto 'cuento' en el cuerpo de la solicitud
        var response = await cliente.PostAsync(urlApi, content);

        // Retorna true si la solicitud fue exitosa (código de estado HTTP 200-299), de lo contrario, false
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateAsynk(Cuento cuento)
    {
        // Método para actualizar un cuento existente en la base de datos

        // Serializa el objeto 'cuento' a formato JSON para enviarlo en el cuerpo de la solicitud PUT
        var content = new StringContent(JsonConvert.SerializeObject(cuento), Encoding.UTF8, "application/json");

        // Envía una solicitud PUT al endpoint de la API con el ID del cuento y el objeto 'cuento' en el cuerpo de la solicitud
        var response = await cliente.PutAsync($"{urlApi}/{cuento._id}", content);

        // Retorna true si la solicitud fue exitosa (código de estado HTTP 200-299), de lo contrario, false
        return response.IsSuccessStatusCode;
    }

}