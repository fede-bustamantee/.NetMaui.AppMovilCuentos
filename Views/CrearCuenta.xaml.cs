using Firebase.Auth;// Importa el espacio de nombres 'Firebase.Auth', que contiene las clases necesarias para interactuar con Firebase Authentication.
using System.Net.Http.Headers;// Importa el espacio de nombres 'System.Net.Http.Headers', que contiene la definici�n de la clase 'MediaTypeWithQualityHeaderValue'.

namespace proyectoCuento.Views;
    public partial class CrearCuenta : ContentPage
    {
        // Declara variables privadas para almacenar instancias de clases relacionadas con la autenticaci�n.
        private readonly FirebaseAuthClient _client;
        // Define una constante para almacenar la clave de la API de Firebase.
        private const string FirebaseApiKey = "AIzaSyCUH5E11BfL1qpt7rJXqX_oF457n_chd0k";
        // Define una constante para almacenar la URI de la solicitud para enviar el c�digo de verificaci�n por correo electr�nico.
        private const string RequestUri = "https://identitytoolkit.googleapis.com/v1/accounts:sendOobCode?key=" + FirebaseApiKey;

        // Define el constructor de la clase 'CrearCuenta', que recibe un cliente de autenticaci�n de Firebase como par�metro.
        public CrearCuenta(FirebaseAuthClient firebaseAuthClient)
        {
            InitializeComponent();
            // Inicializa el cliente de autenticaci�n con el valor proporcionado.
            _client = firebaseAuthClient;
        }

        // Manejador de eventos para el bot�n de crear cuenta.
        private async void btnCrearCuenta_Clicked(object sender, EventArgs e)
        {
            // Verifica si las contrase�as ingresadas coinciden.
            if (txtPassword.Text == txtVerificarPassword.Text)
            {
                try
                {
                    // Crea un nuevo usuario con las credenciales proporcionadas por el usuario.
                    var user = await _client.CreateUserWithEmailAndPasswordAsync(txtEmail.Text, txtPassword.Text);
                    // Env�a un correo electr�nico de verificaci�n al usuario reci�n creado.
                    await SendVerificationEmailAsync(user.User.GetIdTokenAsync().Result);
                    // Muestra un mensaje de alerta indicando que la cuenta se ha creado correctamente.
                    await Application.Current.MainPage.DisplayAlert("Registrarse", "Cuenta creada!", "Ok");
                    // Navega de regreso a la p�gina anterior.
                    await Navigation.PopAsync();
                }
                catch (FirebaseAuthException error)
                {
                    // Muestra un mensaje de alerta en caso de error durante el proceso de registro.
                    await Application.Current.MainPage.DisplayAlert("Registrarse", "Ocurri� un problema:" + error.Reason, "Ok");
                }
            }
            else
            {
                // Muestra un mensaje de alerta si las contrase�as ingresadas no coinciden.
                await Application.Current.MainPage.DisplayAlert("Registrarse", "Las contrase�as ingresadas no coinciden", "Ok");
            }
        }

    // M�todo est�tico para enviar un correo electr�nico de verificaci�n.
    public static async Task SendVerificationEmailAsync(string idToken)
    {
        // Se utiliza un objeto HttpClient para realizar la solicitud HTTP.
        using (var client = new HttpClient())
        {
            // Se configuran las cabeceras de la solicitud para aceptar JSON.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Se crea el contenido del cuerpo de la solicitud JSON con el token de identificaci�n.
            var content = new StringContent("{\"requestType\":\"VERIFY_EMAIL\",\"idToken\":\"" + idToken + "\"}");

            // Se especifica el tipo de contenido del cuerpo de la solicitud como JSON.
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // Se realiza una solicitud POST as�ncrona al URI de la API de Firebase para enviar el correo electr�nico de verificaci�n.
            var response = await client.PostAsync(RequestUri, content);

            // Se asegura de que la solicitud sea exitosa (status code 2xx).
            response.EnsureSuccessStatusCode();
        }
    }
}