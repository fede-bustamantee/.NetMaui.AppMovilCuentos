using proyectoCuento.models; // Espacio de nombres que contiene la definición de la clase Cuento.
using proyectoCuento.Repositories; // Espacio de nombres que contiene la definición de la clase RepositoryCuentos.

namespace proyectoCuento.Views;
    public partial class AgregarCuento : ContentPage
    {
        // Declaración del evento CuentoActualizado.
        public event EventHandler CuentoActualizado;

        // Instancia de la clase RepositoryCuentos para interactuar con la API de cuentos.
        RepositoryCuentos repositoryCuentos = new RepositoryCuentos();

        // Constructor de la clase AgregarCuento.
        public AgregarCuento()
        {
            InitializeComponent();
        }

        // Manejador de eventos para el botón Guardar.
        private async void GuardarBtn_Clicked(object sender, EventArgs e)
        {
            // Creación de un nuevo objeto de tipo Cuento con los datos ingresados en los campos del formulario.
            Cuento nuevoCuento = new Cuento()
            {
                nombre = txtnombre.Text,
                tiempodelectura = Convert.ToInt32(txttiempodelectura.Text),
                imagen_url = txtimagen_url.Text,
                historia = txthistoria.Text,
            };

            // Envío del nuevo cuento al servidor mediante una solicitud POST a través del repositoryCuentos.
            var agregada = await repositoryCuentos.AddAsynk(nuevoCuento);

            // Si el cuento se agregó exitosamente, se cierra la página actual.
            if (agregada)
            {
                await DisplayAlert("Editar", $"Se Agregó correctamente: {nuevoCuento.nombre}", "Ok");
                await Navigation.PopAsync();
            }

            // Se invoca el evento CuentoActualizado para notificar a los suscriptores que se ha actualizado la lista de cuentos.
            CuentoActualizado?.Invoke(this, EventArgs.Empty);
        }
    }