using Firebase.Auth; // Importa el espacio de nombres 'Firebase.Auth', que contiene las clases necesarias para interactuar con Firebase Authentication.
using Firebase.Auth.Repository;// Importa el espacio de nombres 'Firebase.Auth.Repository', que contiene la definición de la clase 'FileUserRepository'.

namespace proyectoCuento.Views;
    public partial class IniciarSecion : ContentPage
    {
        // Declara variables privadas para almacenar instancias de clases relacionadas con la autenticación.
        private readonly FirebaseAuthClient _clientAuth; // Cliente de autenticación de Firebase
        private FileUserRepository _userRepository; // Repositorio de usuario en archivo local
        private UserInfo _userInfo; // Información del usuario autenticado
        private FirebaseCredential _firebaseCredential; // Credencial de autenticación de Firebase
                                                   
    // Define el constructor de la clase 'IniciarSecion', que recibe un cliente de autenticación de Firebase como parámetro.
    public IniciarSecion(FirebaseAuthClient firebaseAuthClient)
        {
            InitializeComponent();
            // Inicializa las variables y asigna valores iniciales.
            _clientAuth = firebaseAuthClient;
            _userRepository = new FileUserRepository("AppCuentos");
            ChequearSiHayUsuarioAlmacenado(); // Llama al método para verificar si hay un usuario almacenado localmente.
        }

        // Método privado para verificar si hay un usuario almacenado localmente y mostrar la pantalla de inicio si es así.
        private async void ChequearSiHayUsuarioAlmacenado()
        {
            if (_userRepository.UserExists())
            {
                // Lee la información del usuario almacenado.
                (_userInfo, _firebaseCredential) = _userRepository.ReadUser();
                // Navega a la página de inicio.
                await Navigation.PushAsync(new Inicio(_clientAuth));
            }
        }

        // Manejador de eventos para el botón de inicio de sesión.
        private async void btnIniciarSesion_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Intenta iniciar sesión con las credenciales proporcionadas por el usuario.
                var userCredential = await _clientAuth.SignInWithEmailAndPasswordAsync(txtEmail.Text, txtPassword.Text);
                // Verifica si el usuario desea recordar la contraseña.
                if (chkRecordarContraseña.IsChecked)
                {
                    // Guarda la información del usuario para futuros inicios de sesión.
                    _userRepository.SaveUser(userCredential.User);
                }
                else
                {
                    // Borra la información del usuario almacenada localmente.
                    _userRepository.DeleteUser();
                }
            // Muestra un mensaje de alerta indicando que se ha iniciado sesion correctamente.
            await Application.Current.MainPage.DisplayAlert("Iniciar Sesión", "Inicio de sesion exitoso!", "Ok");
            // Navega a la página de inicio después del inicio de sesión exitoso.
            await Navigation.PushAsync(new Inicio(_clientAuth));
            }
            catch (FirebaseAuthException error)
            {
                // Muestra un mensaje de alerta en caso de error durante el inicio de sesión.
                await Application.Current.MainPage.DisplayAlert("Inicio de sesión", "Ocurrió un problema: " + error.Reason, "Ok");
            }
        }

        // Manejador de eventos para el botón de registro.
        private async void btnRegistrarse_Clicked(object sender, EventArgs e)
        {
            // Navega a la página para crear una cuenta nueva.
            await Navigation.PushAsync(new CrearCuenta(_clientAuth));
        }
    }