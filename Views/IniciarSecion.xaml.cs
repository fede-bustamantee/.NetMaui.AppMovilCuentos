using Firebase.Auth; // Importa el espacio de nombres 'Firebase.Auth', que contiene las clases necesarias para interactuar con Firebase Authentication.
using Firebase.Auth.Repository;// Importa el espacio de nombres 'Firebase.Auth.Repository', que contiene la definici�n de la clase 'FileUserRepository'.

namespace proyectoCuento.Views;
    public partial class IniciarSecion : ContentPage
    {
        // Declara variables privadas para almacenar instancias de clases relacionadas con la autenticaci�n.
        private readonly FirebaseAuthClient _clientAuth; // Cliente de autenticaci�n de Firebase
        private FileUserRepository _userRepository; // Repositorio de usuario en archivo local
        private UserInfo _userInfo; // Informaci�n del usuario autenticado
        private FirebaseCredential _firebaseCredential; // Credencial de autenticaci�n de Firebase
                                                   
    // Define el constructor de la clase 'IniciarSecion', que recibe un cliente de autenticaci�n de Firebase como par�metro.
    public IniciarSecion(FirebaseAuthClient firebaseAuthClient)
        {
            InitializeComponent();
            // Inicializa las variables y asigna valores iniciales.
            _clientAuth = firebaseAuthClient;
            _userRepository = new FileUserRepository("AppCuentos");
            ChequearSiHayUsuarioAlmacenado(); // Llama al m�todo para verificar si hay un usuario almacenado localmente.
        }

        // M�todo privado para verificar si hay un usuario almacenado localmente y mostrar la pantalla de inicio si es as�.
        private async void ChequearSiHayUsuarioAlmacenado()
        {
            if (_userRepository.UserExists())
            {
                // Lee la informaci�n del usuario almacenado.
                (_userInfo, _firebaseCredential) = _userRepository.ReadUser();
                // Navega a la p�gina de inicio.
                await Navigation.PushAsync(new Inicio(_clientAuth));
            }
        }

        // Manejador de eventos para el bot�n de inicio de sesi�n.
        private async void btnIniciarSesion_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Intenta iniciar sesi�n con las credenciales proporcionadas por el usuario.
                var userCredential = await _clientAuth.SignInWithEmailAndPasswordAsync(txtEmail.Text, txtPassword.Text);
                // Verifica si el usuario desea recordar la contrase�a.
                if (chkRecordarContrase�a.IsChecked)
                {
                    // Guarda la informaci�n del usuario para futuros inicios de sesi�n.
                    _userRepository.SaveUser(userCredential.User);
                }
                else
                {
                    // Borra la informaci�n del usuario almacenada localmente.
                    _userRepository.DeleteUser();
                }
            // Muestra un mensaje de alerta indicando que se ha iniciado sesion correctamente.
            await Application.Current.MainPage.DisplayAlert("Iniciar Sesi�n", "Inicio de sesion exitoso!", "Ok");
            // Navega a la p�gina de inicio despu�s del inicio de sesi�n exitoso.
            await Navigation.PushAsync(new Inicio(_clientAuth));
            }
            catch (FirebaseAuthException error)
            {
                // Muestra un mensaje de alerta en caso de error durante el inicio de sesi�n.
                await Application.Current.MainPage.DisplayAlert("Inicio de sesi�n", "Ocurri� un problema: " + error.Reason, "Ok");
            }
        }

        // Manejador de eventos para el bot�n de registro.
        private async void btnRegistrarse_Clicked(object sender, EventArgs e)
        {
            // Navega a la p�gina para crear una cuenta nueva.
            await Navigation.PushAsync(new CrearCuenta(_clientAuth));
        }
    }