using proyectoCuento.models;
using proyectoCuento.Repositories;
namespace proyectoCuento.Views;

    public partial class EditandoCuentos : ContentPage
    {
        // Campo que representa el cuento seleccionado para edición.
        private Cuento selectedCuento;

        // Instancia de la clase RepositoryCuentos para interactuar con la API de cuentos.
        RepositoryCuentos repositoryCuentos = new RepositoryCuentos();

        // Constructor de la clase EditandoCuentos.
        public EditandoCuentos(Cuento cuento)
        {
            InitializeComponent(); 

            // Almacena el cuento seleccionado para su uso posterior.
            selectedCuento = cuento;

            // Establece el contexto de enlace de datos de la página como el cuento seleccionado.
            BindingContext = selectedCuento;

            // Carga los datos del cuento en los campos de entrada.
            CargarDatosEnPantalla();
        }

        // Método para cargar los datos del cuento en los campos de entrada.
        private void CargarDatosEnPantalla()
        {
            // Asigna los valores del cuento a los campos de entrada correspondientes.
            txtnombre.Text = selectedCuento.nombre;
            txttiempodelectura.Text = selectedCuento.tiempodelectura.ToString();
            txtimagen_url.Text = selectedCuento.imagen_url;
            txthistoria.Text = selectedCuento.historia;
        }

        // Manejador de eventos para el clic en el botón de guardar cambios.
        private async void Guardarbtn_Clicked(object sender, EventArgs e)
        {
            // Crea un nuevo objeto Cuento con los datos editados.
            Cuento cuentoEditado = new Cuento()
            {
                _id = selectedCuento._id,
                nombre = txtnombre.Text,
                tiempodelectura = Convert.ToInt32(txttiempodelectura.Text),
                imagen_url = txtimagen_url.Text,
                historia = txthistoria.Text,
            };

            // Envía una solicitud PUT para actualizar el cuento en la API.
            var guardada = await repositoryCuentos.UpdateAsynk(cuentoEditado);

            // Si la operación de guardado es exitosa, muestra un mensaje informativo y vuelve a la página principal.
            if (guardada)
            {
                await DisplayAlert("Editar", $"Se editó correctamente: {selectedCuento.nombre}", "Ok");
                await Navigation.PopToRootAsync();
            }
        }
    }