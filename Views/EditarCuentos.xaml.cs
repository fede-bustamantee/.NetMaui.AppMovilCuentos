using proyectoCuento.models; // Contiene la definición de la clase Cuento.
using System.Collections.ObjectModel;


namespace proyectoCuento.Views;
    
    public partial class EditarCuentos : ContentPage
    {
        // Colección observable de cuentos.
        public ObservableCollection<Cuento> Cuentos;

        // Constructor de la clase EditarCuentos.
        public EditarCuentos(ObservableCollection<Cuento> cuentos) : base()
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

        // Manejador de eventos para el clic en el botón de editar cuento.
        private async void EditarCuento_Clicked(object sender, EventArgs e)
        {
            // Obtiene el cuento seleccionado del contexto del botón.
            Cuento selectedCuento = (Cuento)((ImageButton)sender).BindingContext;

            // Navega a la página de edición de cuentos (EditandoCuentos) y pasa el cuento seleccionado como parámetro.
            await Navigation.PushAsync(new EditandoCuentos(selectedCuento));
        }
    }