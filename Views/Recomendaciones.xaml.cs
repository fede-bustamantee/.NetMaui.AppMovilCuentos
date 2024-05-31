using proyectoCuento.Models; // Importa el espacio de nombres para los modelos de datos

namespace proyectoCuento.Views;
    public partial class Recomendaciones : ContentPage // Define la clase Recomendaciones
    {
        private Recomendacione selectedRecomendacione; // Recomendación seleccionada
        public HashSet<string> cuentosFavoritos; // Conjunto de cuentos favoritos

        public Recomendaciones(Recomendacione recomendacione, HashSet<string> favoritos) // Constructor de la clase
        {
            InitializeComponent(); 
            selectedRecomendacione = recomendacione; // Asigna la recomendación seleccionada
            cuentosFavoritos = favoritos; // Asigna el conjunto de cuentos favoritos

            BindingContext = selectedRecomendacione; // Asigna el contexto de datos para la página

            UpdateIconColor(); // Actualiza el color del icono de favorito
        }

        private void UpdateIconColor() // Método para actualizar el color del icono de favorito
        {
            if (cuentosFavoritos.Contains(selectedRecomendacione._id)) // Si la recomendación está en favoritos
            {
                iconLabel.TextColor = Colors.White; // Muestra el icono de favorito en blanco
            }
            else // Si la recomendación no está en favoritos
            {
                iconLabel.TextColor = Colors.Transparent; // Oculta el icono de favorito
            }
        }

        private void OnIconTapped(object sender, EventArgs e) // Método que se ejecuta cuando se hace clic en el icono de favorito
        {
            var label = (Label)sender; // Obtiene la etiqueta del icono de favorito
            var cuento = (Recomendacione)label.BindingContext; // Obtiene la recomendación relacionada con el icono

            if (label.TextColor == Colors.White) // Si la recomendación está en favoritos
            {
                label.SetValue(Label.TextColorProperty, Colors.Transparent); // Oculta el icono de favorito
                cuentosFavoritos.Remove(cuento._id); // Elimina la recomendación de favoritos
            }
            else // Si la recomendación no está en favoritos
            {
                label.SetValue(Label.TextColorProperty, Colors.White); // Muestra el icono de favorito en blanco
                cuentosFavoritos.Add(cuento._id); // Agrega la recomendación a favoritos
            }
        }
    }