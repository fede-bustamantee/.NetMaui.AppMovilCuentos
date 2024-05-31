using proyectoCuento.models; // Importa el espacio de nombres de los modelos de datos

namespace proyectoCuento.Views;
    public partial class Historias : ContentPage
    {
        private Cuento selectedCuento; // Declara una variable para almacenar el cuento seleccionado
        private HashSet<string> cuentosFavoritos; // Declara una variable HashSet para almacenar los identificadores de los cuentos favoritos

        public Historias(Cuento cuento, HashSet<string> favoritos) // Define el constructor de la clase Historias
        {
            InitializeComponent();

            selectedCuento = cuento; // Almacena el cuento seleccionado
            cuentosFavoritos = favoritos; // Asigna el conjunto de cuentos favoritos recibido como par�metro

            BindingContext = selectedCuento; // Establece el contexto de enlace como el cuento seleccionado

            UpdateIconColor(); // Llama al m�todo para actualizar el color del �cono
        }

        private void UpdateIconColor() // Define el m�todo para actualizar el color del �cono
        {
            if (cuentosFavoritos.Contains(selectedCuento._id))
            {
                // Si el cuento est� en la lista de favoritos, muestra el �cono en blanco
                iconLabel.TextColor = Colors.White;
            }
            else
            {
                // Si el cuento no est� en la lista de favoritos, muestra el �cono en transparente
                iconLabel.TextColor = Colors.Transparent;
            }
        }

        private void OnIconTapped(object sender, EventArgs e) // Define el manejador de eventos para cuando se toca el �cono
        {
            var label = (Label)sender;
            var cuento = (Cuento)label.BindingContext; // Obtiene el cuento relacionado al �cono tocado

            if (label.TextColor == Colors.White)
            {
                // Si el cuento ya est� en favoritos, lo elimina de la lista y cambia el color del �cono a transparente
                label.SetValue(Label.TextColorProperty, Colors.Transparent);
                cuentosFavoritos.Remove(cuento._id);
            }
            else
            {
                // Si el cuento no est� en favoritos, lo agrega a la lista y cambia el color del �cono a blanco
                label.SetValue(Label.TextColorProperty, Colors.White);
                cuentosFavoritos.Add(cuento._id);
            }
        }
    }