using proyectoCuento.models; // Contiene la definici�n de la clase Cuento.
using System.Collections.ObjectModel;


namespace proyectoCuento.Views;
    
    public partial class EditarCuentos : ContentPage
    {
        // Colecci�n observable de cuentos.
        public ObservableCollection<Cuento> Cuentos;

        // Constructor de la clase EditarCuentos.
        public EditarCuentos(ObservableCollection<Cuento> cuentos) : base()
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

        // Manejador de eventos para el clic en el bot�n de editar cuento.
        private async void EditarCuento_Clicked(object sender, EventArgs e)
        {
            // Obtiene el cuento seleccionado del contexto del bot�n.
            Cuento selectedCuento = (Cuento)((ImageButton)sender).BindingContext;

            // Navega a la p�gina de edici�n de cuentos (EditandoCuentos) y pasa el cuento seleccionado como par�metro.
            await Navigation.PushAsync(new EditandoCuentos(selectedCuento));
        }
    }