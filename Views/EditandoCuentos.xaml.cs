using proyectoCuento.models;
using proyectoCuento.Repositories;
namespace proyectoCuento.Views;

    public partial class EditandoCuentos : ContentPage
    {
        // Campo que representa el cuento seleccionado para edici�n.
        private Cuento selectedCuento;

        // Instancia de la clase RepositoryCuentos para interactuar con la API de cuentos.
        RepositoryCuentos repositoryCuentos = new RepositoryCuentos();

        // Constructor de la clase EditandoCuentos.
        public EditandoCuentos(Cuento cuento)
        {
            InitializeComponent(); 

            // Almacena el cuento seleccionado para su uso posterior.
            selectedCuento = cuento;

            // Establece el contexto de enlace de datos de la p�gina como el cuento seleccionado.
            BindingContext = selectedCuento;

            // Carga los datos del cuento en los campos de entrada.
            CargarDatosEnPantalla();
        }

        // M�todo para cargar los datos del cuento en los campos de entrada.
        private void CargarDatosEnPantalla()
        {
            // Asigna los valores del cuento a los campos de entrada correspondientes.
            txtnombre.Text = selectedCuento.nombre;
            txttiempodelectura.Text = selectedCuento.tiempodelectura.ToString();
            txtimagen_url.Text = selectedCuento.imagen_url;
            txthistoria.Text = selectedCuento.historia;
        }

        // Manejador de eventos para el clic en el bot�n de guardar cambios.
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

            // Env�a una solicitud PUT para actualizar el cuento en la API.
            var guardada = await repositoryCuentos.UpdateAsynk(cuentoEditado);

            // Si la operaci�n de guardado es exitosa, muestra un mensaje informativo y vuelve a la p�gina principal.
            if (guardada)
            {
                await DisplayAlert("Editar", $"Se edit� correctamente: {selectedCuento.nombre}", "Ok");
                await Navigation.PopToRootAsync();
            }
        }
    }