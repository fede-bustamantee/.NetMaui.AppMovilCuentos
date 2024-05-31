using System.Collections.ObjectModel;
using proyectoCuento.models;// Importa el espacio de nombres para los modelos de datos
using proyectoCuento.Models;
using proyectoCuento.Repositories;// Importa el espacio de nombres para los repositorios
using Firebase.Auth;// Importa el espacio de nombres para la autenticaci�n con Firebase

namespace proyectoCuento.Views;
    public partial class Inicio : ContentPage // Define la clase 
    {
        public ObservableCollection<Cuento> Cuentos { get; set; } // Colecci�n observable de cuentos
        private ObservableCollection<Cuento> CuentosFiltrados { get; set; } // Colecci�n observable de cuentos filtrados
        public ObservableCollection<Recomendacione> Recomendaciones { get; set; } // Colecci�n observable de recomendaciones

        RepositoryCuentos repositoryCuentos = new RepositoryCuentos(); // Instancia del repositorio de cuentos
        RepositoryRecomendaciones repositoryRecomendaciones = new RepositoryRecomendaciones(); // Instancia del repositorio de recomendaciones

        private readonly FirebaseAuthClient _clientAuth; // Cliente de autenticaci�n de Firebase

        public HashSet<string> cuentosFavoritos = new HashSet<string>(); // Conjunto de cuentos favoritos


    public Inicio(FirebaseAuthClient firebaseAuthClient) // Constructor de la clase

    {
        InitializeComponent();
        Cuentos = new ObservableCollection<Cuento>(); // Inicializa la colecci�n observable de cuentos
        Recomendaciones = new ObservableCollection<Recomendacione>(); // Inicializa la colecci�n observable de recomendaciones

        _clientAuth = firebaseAuthClient; // Asigna el cliente de autenticaci�n de Firebase

        GetAllRecomendaciones(); // Obtiene todas las recomendaciones al iniciar la p�gina
        searchBar.TextChanged += SearchBar_TextChanged; // Asigna el evento de cambio de texto del campo de b�squeda
    }
    private async void CargarCuentos() // M�todo para cargar los cuentos
    {
        Cuentos = await repositoryCuentos.GetAllAsync(); // Obtiene todos los cuentos desde el repositorio
        CuentosFiltrados = new ObservableCollection<Cuento>(Cuentos); // Inicializa la colecci�n filtrada con todos los cuentos
        CuentosCollectionView.ItemsSource = CuentosFiltrados; // Asigna la colecci�n filtrada como origen de datos para la vista
    }
    public async void GetAllRecomendaciones() // M�todo para obtener todas las recomendaciones
    {
        Recomendaciones = await repositoryRecomendaciones.GetAllAsync(); // Obtiene todas las recomendaciones desde el repositorio
        RecomendacionesCollectionView.ItemsSource = Recomendaciones; // Asigna la colecci�n de recomendaciones como origen de datos para la vista
    }

    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e) // M�todo que se ejecuta cuando cambia el texto del campo de b�squeda
    {
        string filtro = e.NewTextValue.ToLower(); // Obtiene el texto de b�squeda en min�sculas
        if (string.IsNullOrWhiteSpace(filtro)) // Si el texto de b�squeda est� vac�o
        {
            CargarCuentos(); // Carga todos los cuentos
            lblNoResults.IsVisible = false; // Oculta el mensaje de "No hay resultados"
        }
        else // Si hay texto de b�squeda
        {
            CuentosFiltrados.Clear(); // Borra la lista de cuentos filtrados
            foreach (var cuento in Cuentos) // Para cada cuento en la lista de cuentos
            {
                if (cuento.nombre.ToLower().Contains(filtro)) // Si el nombre del cuento contiene el texto de b�squeda
                {
                    CuentosFiltrados.Add(cuento); // Agrega el cuento a la lista de cuentos filtrados
                }
            }
            lblNoResults.IsVisible = CuentosFiltrados.Count == 0; // Muestra el mensaje de "No hay resultados" si no hay cuentos filtrados
        }
    }
    
    protected override void OnAppearing() // M�todo que se ejecuta cuando la p�gina est� visible
    {
        CargarCuentos(); // Carga los cuentos al aparecer la p�gina
    }

    // M�todo que se ejecuta cuando se hace clic en un contador (cuento)
    private async void OnCounterClicked(object sender, EventArgs e)
    {
        // Obtiene el cuento seleccionado desde el contexto de enlace del bot�n
        Cuento selectedCuento = (Cuento)((ImageButton)sender).BindingContext;
        // Navega a la p�gina de Historias pasando el cuento seleccionado y los cuentos favoritos
        await Navigation.PushAsync(new Historias(selectedCuento, cuentosFavoritos)); // Cambiado de cuentosGuardados a cuentosFavoritos
    }

    // M�todo que se ejecuta cuando se hace clic en una recomendaci�n
    private async void OnRecomendacionClicked(object sender, EventArgs e)
    {
        // Obtiene la recomendaci�n seleccionada desde el contexto de enlace del bot�n
        Recomendacione selectedRecomendacion = (Recomendacione)((ImageButton)sender).BindingContext;
        // Navega a la p�gina de Recomendaciones pasando la recomendaci�n seleccionada y los cuentos favoritos
        await Navigation.PushAsync(new Recomendaciones(selectedRecomendacion, cuentosFavoritos));
    }

    // M�todo que se ejecuta cuando se hace clic en un bot�n de agregar cuento
    private async void OnButtonTapped(object sender, EventArgs e)
    {
        // Navega a la p�gina de AgregarCuento
        await Navigation.PushAsync(new AgregarCuento());
    }

    // M�todo que se ejecuta cuando se hace clic en un bot�n de eliminar cuento
    private async void OnEliminarTapped(object sender, EventArgs e)
    {
        // Navega a la p�gina de EliminarCuento pasando la colecci�n de cuentos
        await Navigation.PushAsync(new EliminarCuento(Cuentos));
    }

    // M�todo que se ejecuta cuando se hace clic en un bot�n de editar cuentos
    private async void OnEditarTapped(object sender, EventArgs e)
    {
        // Navega a la p�gina de EditarCuentos pasando la colecci�n de cuentos
        await Navigation.PushAsync(new EditarCuentos(Cuentos));
    }

    // M�todo que se ejecuta cuando se hace clic en un bot�n de registro
    private async void OnRegistroTapped(object sender, TappedEventArgs e)
    {
        // Navega a la p�gina de IniciarSecion pasando el cliente de autenticaci�n de Firebase
        await Navigation.PushAsync(new IniciarSecion(_clientAuth));
    }

    // M�todo que se ejecuta cuando se hace clic en un bot�n de favoritos
    private async void OnFavoritoTapped(object sender, TappedEventArgs e)
    {
        // Navega a la p�gina de Favoritos pasando los cuentos favoritos y la instancia actual de la p�gina Inicio
        await Navigation.PushAsync(new Favoritos(cuentosFavoritos, this));
    }


}