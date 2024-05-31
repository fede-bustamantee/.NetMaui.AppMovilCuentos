using System.Collections.ObjectModel;
using proyectoCuento.models;
using proyectoCuento.Models;

namespace proyectoCuento.Views;
    public partial class Favoritos : ContentPage
    {
        // HashSet para almacenar los IDs de los cuentos favoritos.
        public HashSet<string> cuentosFavoritos;

        // Colección observable que contiene los elementos favoritos (cuentos y recomendaciones).
        private ObservableCollection<object> favoritos;

        // Constructor de la clase Favoritos.
        public Favoritos(HashSet<string> favoritos, Inicio inicio)
        {
            InitializeComponent(); // Inicializa los componentes de la página.

            // Asigna el conjunto de cuentos favoritos proporcionado como parámetro.
            cuentosFavoritos = favoritos;

            // Inicializa la lista 'favoritos' como una colección observable.
            this.favoritos = new ObservableCollection<object>();

            // Actualiza la lista de favoritos.
            UpdateFavoritos();
        }

        // Método para actualizar la lista de favoritos.
        private void UpdateFavoritos()
        {
            favoritos.Clear(); // Borra la lista de favoritos.

            // Agrega todos los cuentos favoritos a la lista.
            foreach (var cuento in App.AllCuentos)
            {
                if (cuentosFavoritos.Contains(cuento._id))
                {
                    favoritos.Add(cuento);
                }
            }

            // Agrega todas las recomendaciones favoritas a la lista.
            foreach (var recomendacione in App.AllRecomendaciones)
            {
                if (cuentosFavoritos.Contains(recomendacione._id))
                {
                    favoritos.Add(recomendacione);
                }
            }

            // Establece la lista de favoritos como origen de datos de la ListView.
            listaCuentosFavoritos.ItemsSource = favoritos;

            // Muestra un mensaje si no hay cuentos favoritos.
            lblNoCuentosFavoritos.IsVisible = favoritos == null || !favoritos.Any();
        }

        // Manejador de eventos para el clic en el botón ImageButton (icono de cuento favorito).
        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            // Obtiene el cuento seleccionado del contexto del botón y navega a la página de detalles (Historias).
            Cuento selectedCuento = (Cuento)((ImageButton)sender).BindingContext;
            await Navigation.PushAsync(new Historias(selectedCuento, cuentosFavoritos));
        }
    private async void ImageButon_Clicked(object sender, EventArgs e)
    {
        // Obtiene el cuento seleccionado del contexto del botón y navega a la página de detalles (Historias).
        Recomendacione selectedCuento = (Recomendacione)((ImageButton)sender).BindingContext;
        await Navigation.PushAsync(new Recomendaciones(selectedCuento, cuentosFavoritos));
    }
}