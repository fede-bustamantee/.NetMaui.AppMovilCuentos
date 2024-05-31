using proyectoCuento.models;
using proyectoCuento.Repositories; 
using System.Collections.ObjectModel; 

namespace proyectoCuento.Views;
    public partial class EliminarCuento : ContentPage
    {
        // Instancia de la clase RepositoryCuentos para interactuar con la API de cuentos.
        RepositoryCuentos repositoryCuentos = new RepositoryCuentos();

        // Propiedad que representa el cuento seleccionado para eliminar.
        Cuento cuentoSeleccionado { get; set; }

        // Colección observable de cuentos.
        public ObservableCollection<Cuento> Cuentos { get; set; }

        // Constructor de la clase EliminarCuento.
        public EliminarCuento(ObservableCollection<Cuento> cuentos) : base()
        {
            InitializeComponent(); 
            Cuentos = cuentos; // Asignación de la colección de cuentos proporcionada como parámetro.
            BindingContext = this; // Establece el contexto de enlace de datos de la página como esta instancia.
        }

        // Método que se ejecuta cuando la página está a punto de aparecer en la pantalla.
        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Establece la colección de cuentos como origen de datos para la colección de vista CuentosCollectionView.
            CuentosCollectionView.ItemsSource = Cuentos;
        }

        // Manejador de eventos para el clic en el botón de eliminar cuento.
        private async void EliminarCuento_Clicked(object sender, EventArgs e)
        {
            var button = sender as ImageButton;
            if (button != null && button.BindingContext is Cuento cuentoSeleccionado)
            {
                // Muestra un cuadro de diálogo de confirmación para la eliminación del cuento.
                bool respuesta = await DisplayAlert("Eliminar", $"¿Está seguro que desea ELIMINAR este cuento?: {cuentoSeleccionado.nombre}", "Si", "No");
                if (respuesta)
                {
                    // Intenta eliminar el cuento utilizando el repositorio de cuentos.
                    var eliminado = await repositoryCuentos.RemoveAsync(cuentoSeleccionado._id);

                    // Si se elimina correctamente, muestra un cuadro de diálogo informativo y vuelve a la página principal.
                    if (eliminado)
                    {
                        await DisplayAlert("Eliminar", $"Se eliminó el cuento correctamente: {cuentoSeleccionado.nombre}", "Ok");
                        await Navigation.PopToRootAsync();
                    }
                }
            }
        }
    }
