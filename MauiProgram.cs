using Firebase.Auth; // Importa el espacio de nombres para la autenticación con Firebase
using Firebase.Auth.Providers; // Importa los proveedores de autenticación de Firebase
using Microsoft.Extensions.Logging; 
using SkiaSharp.Views.Maui.Controls.Hosting; // Importa las vistas SkiaSharp para Maui (animaciones)

namespace proyectoCuento;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseSkiaSharp()
			.ConfigureFonts(fonts =>

            // Agrega fuentes al diccionario de fuentes
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("Epilogue-Medium.ttf", "Epilogue");//letra
                fonts.AddFont("fontello.ttf", "Icons"); //icono
            });
        builder.Services.AddSingleton(new FirebaseAuthClient(new FirebaseAuthConfig() // Agrega un cliente de autenticación de Firebase como servicio único
        {
            ApiKey = "AIzaSyCUH5E11BfL1qpt7rJXqX_oF457n_chd0k",
            AuthDomain = "appcuentos-3532b.firebaseapp.com",
            Providers = new FirebaseAuthProvider[] // Define los proveedores de autenticación disponibles
            {
                new EmailProvider() // Proveedor de autenticación por correo electrónico
            }
        }));

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
