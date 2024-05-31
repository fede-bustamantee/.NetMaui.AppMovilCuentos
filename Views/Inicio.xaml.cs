using System.Collections.ObjectModel;
using proyectoCuento.models;// Importa el espacio de nombres para los modelos de datos
using proyectoCuento.Models;
using proyectoCuento.Repositories;// Importa el espacio de nombres para los repositorios
using Firebase.Auth;// Importa el espacio de nombres para la autenticación con Firebase

namespace proyectoCuento.Views;
    public partial class Inicio : ContentPage // Define la clase 
    {
        public ObservableCollection<Cuento> Cuentos { get; set; } // Colección observable de cuentos
        private ObservableCollection<Cuento> CuentosFiltrados { get; set; } // Colección observable de cuentos filtrados
        public ObservableCollection<Recomendacione> Recomendaciones { get; set; } // Colección observable de recomendaciones

        RepositoryCuentos repositoryCuentos = new RepositoryCuentos(); // Instancia del repositorio de cuentos
        RepositoryRecomendaciones repositoryRecomendaciones = new RepositoryRecomendaciones(); // Instancia del repositorio de recomendaciones

        private readonly FirebaseAuthClient _clientAuth; // Cliente de autenticación de Firebase

        public HashSet<string> cuentosFavoritos = new HashSet<string>(); // Conjunto de cuentos favoritos


    public Inicio(FirebaseAuthClient firebaseAuthClient) // Constructor de la clase

    {
        InitializeComponent();
        Cuentos = new ObservableCollection<Cuento>(); // Inicializa la colección observable de cuentos
        Recomendaciones = new ObservableCollection<Recomendacione>(); // Inicializa la colección observable de recomendaciones

        _clientAuth = firebaseAuthClient; // Asigna el cliente de autenticación de Firebase

        GetAllRecomendaciones(); // Obtiene todas las recomendaciones al iniciar la página
        searchBar.TextChanged += SearchBar_TextChanged; // Asigna el evento de cambio de texto del campo de búsqueda
    }
    private async void CargarCuentos() // Método para cargar los cuentos
    {
        Cuentos = await repositoryCuentos.GetAllAsync(); // Obtiene todos los cuentos desde el repositorio
        CuentosFiltrados = new ObservableCollection<Cuento>(Cuentos); // Inicializa la colección filtrada con todos los cuentos
        CuentosCollectionView.ItemsSource = CuentosFiltrados; // Asigna la colección filtrada como origen de datos para la vista
    }
    public async void GetAllRecomendaciones() // Método para obtener todas las recomendaciones
    {
        Recomendaciones = await repositoryRecomendaciones.GetAllAsync(); // Obtiene todas las recomendaciones desde el repositorio
        RecomendacionesCollectionView.ItemsSource = Recomendaciones; // Asigna la colección de recomendaciones como origen de datos para la vista
    }

    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e) // Método que se ejecuta cuando cambia el texto del campo de búsqueda
    {
        string filtro = e.NewTextValue.ToLower(); // Obtiene el texto de búsqueda en minúsculas
        if (string.IsNullOrWhiteSpace(filtro)) // Si el texto de búsqueda está vacío
        {
            CargarCuentos(); // Carga todos los cuentos
            lblNoResults.IsVisible = false; // Oculta el mensaje de "No hay resultados"
        }
        else // Si hay texto de búsqueda
        {
            CuentosFiltrados.Clear(); // Borra la lista de cuentos filtrados
            foreach (var cuento in Cuentos) // Para cada cuento en la lista de cuentos
            {
                if (cuento.nombre.ToLower().Contains(filtro)) // Si el nombre del cuento contiene el texto de búsqueda
                {
                    CuentosFiltrados.Add(cuento); // Agrega el cuento a la lista de cuentos filtrados
                }
            }
            lblNoResults.IsVisible = CuentosFiltrados.Count == 0; // Muestra el mensaje de "No hay resultados" si no hay cuentos filtrados
        }
    }
    
    protected override void OnAppearing() // Método que se ejecuta cuando la página está visible
    {
        CargarCuentos(); // Carga los cuentos al aparecer la página
    }

    // Método que se ejecuta cuando se hace clic en un contador (cuento)
    private async void OnCounterClicked(object sender, EventArgs e)
    {
        // Obtiene el cuento seleccionado desde el contexto de enlace del botón
        Cuento selectedCuento = (Cuento)((ImageButton)sender).BindingContext;
        // Navega a la página de Historias pasando el cuento seleccionado y los cuentos favoritos
        await Navigation.PushAsync(new Historias(selectedCuento, cuentosFavoritos)); // Cambiado de cuentosGuardados a cuentosFavoritos
    }

    // Método que se ejecuta cuando se hace clic en una recomendación
    private async void OnRecomendacionClicked(object sender, EventArgs e)
    {
        // Obtiene la recomendación seleccionada desde el contexto de enlace del botón
        Recomendacione selectedRecomendacion = (Recomendacione)((ImageButton)sender).BindingContext;
        // Navega a la página de Recomendaciones pasando la recomendación seleccionada y los cuentos favoritos
        await Navigation.PushAsync(new Recomendaciones(selectedRecomendacion, cuentosFavoritos));
    }

    // Método que se ejecuta cuando se hace clic en un botón de agregar cuento
    private async void OnButtonTapped(object sender, EventArgs e)
    {
        // Navega a la página de AgregarCuento
        await Navigation.PushAsync(new AgregarCuento());
    }

    // Método que se ejecuta cuando se hace clic en un botón de eliminar cuento
    private async void OnEliminarTapped(object sender, EventArgs e)
    {
        // Navega a la página de EliminarCuento pasando la colección de cuentos
        await Navigation.PushAsync(new EliminarCuento(Cuentos));
    }

    // Método que se ejecuta cuando se hace clic en un botón de editar cuentos
    private async void OnEditarTapped(object sender, EventArgs e)
    {
        // Navega a la página de EditarCuentos pasando la colección de cuentos
        await Navigation.PushAsync(new EditarCuentos(Cuentos));
    }

    // Método que se ejecuta cuando se hace clic en un botón de registro
    private async void OnRegistroTapped(object sender, TappedEventArgs e)
    {
        // Navega a la página de IniciarSecion pasando el cliente de autenticación de Firebase
        await Navigation.PushAsync(new IniciarSecion(_clientAuth));
    }

    // Método que se ejecuta cuando se hace clic en un botón de favoritos
    private async void OnFavoritoTapped(object sender, TappedEventArgs e)
    {
        // Navega a la página de Favoritos pasando los cuentos favoritos y la instancia actual de la página Inicio
        await Navigation.PushAsync(new Favoritos(cuentosFavoritos, this));
    }


}