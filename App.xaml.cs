//importacion de los espacios de nombres para utilizar clases y funcionalidades 
using Firebase.Auth; // Importa el espacio de nombres para la autenticación con Firebase
using proyectoCuento.Views;
using proyectoCuento.Models; // Importa los modelos de datos de la aplicación
using proyectoCuento.Repositories; // Importa los repositorios para interactuar con las APIs
using System.Collections.ObjectModel; // Importa la clase ObservableCollection para colecciones que notifican cambios
using proyectoCuento.models; // Importa los modelos de datos de la aplicación

namespace proyectoCuento;
    public partial class App : Application
    {
        public static ObservableCollection<Cuento> AllCuentos { get; set; } // Colección observable de cuentos
        public static ObservableCollection<Recomendacione> AllRecomendaciones { get; set; } // Colección observable de recomendaciones

        private readonly RepositoryCuentos repositoryCuentos = new RepositoryCuentos(); // Instancia del repositorio de cuentos

        private readonly RepositoryRecomendaciones repositoryRecomendaciones = new RepositoryRecomendaciones(); // Instancia del repositorio de recomendaciones

        public App(FirebaseAuthClient firebaseAuthClient)
        {
            InitializeComponent(); // Inicializa los componentes de la aplicación

            MainPage = new NavigationPage(new Inicio(firebaseAuthClient)); // Establece la página principal como la vista de inicio con autenticación

            LoadAllCuentos(); // Carga todos los cuentos al iniciar la aplicación
        }

        private async void LoadAllCuentos()
        {
            AllCuentos = await repositoryCuentos.GetAllAsync(); // Carga todos los cuentos desde el repositorio de cuentos
            AllRecomendaciones = await repositoryRecomendaciones.GetAllAsync(); // Carga todas las recomendaciones desde el repositorio de recomendaciones
        }
    }