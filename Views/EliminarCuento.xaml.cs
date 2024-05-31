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

        // Colecci�n observable de cuentos.
        public ObservableCollection<Cuento> Cuentos { get; set; }

        // Constructor de la clase EliminarCuento.
        public EliminarCuento(ObservableCollection<Cuento> cuentos) : base()
        {
            InitializeComponent(); 
            Cuentos = cuentos; // Asignaci�n de la colecci�n de cuentos proporcionada como par�metro.
            BindingContext = this; // Establece el contexto de enlace de datos de la p�gina como esta instancia.
        }

        // M�todo que se ejecuta cuando la p�gina est� a punto de aparecer en la pantalla.
        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Establece la colecci�n de cuentos como origen de datos para la colecci�n de vista CuentosCollectionView.
            CuentosCollectionView.ItemsSource = Cuentos;
        }

        // Manejador de eventos para el clic en el bot�n de eliminar cuento.
        private async void EliminarCuento_Clicked(object sender, EventArgs e)
        {
            var button = sender as ImageButton;
            if (button != null && button.BindingContext is Cuento cuentoSeleccionado)
            {
                // Muestra un cuadro de di�logo de confirmaci�n para la eliminaci�n del cuento.
                bool respuesta = await DisplayAlert("Eliminar", $"�Est� seguro que desea ELIMINAR este cuento?: {cuentoSeleccionado.nombre}", "Si", "No");
                if (respuesta)
                {
                    // Intenta eliminar el cuento utilizando el repositorio de cuentos.
                    var eliminado = await repositoryCuentos.RemoveAsync(cuentoSeleccionado._id);

                    // Si se elimina correctamente, muestra un cuadro de di�logo informativo y vuelve a la p�gina principal.
                    if (eliminado)
                    {
                        await DisplayAlert("Eliminar", $"Se elimin� el cuento correctamente: {cuentoSeleccionado.nombre}", "Ok");
                        await Navigation.PopToRootAsync();
                    }
                }
            }
        }
    }
