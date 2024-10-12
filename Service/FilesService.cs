using Firebase.Auth;
using Firebase.Storage;

namespace S_CIngenieria.Service
{
    public class FilesService : IFilesService
    {
        public async Task<string> SubirArchivo(Stream archivo, string nombre)
        {
            string email = "msantiagov99@gmail.com";
            string clave = "S&CIngenieria";
            string ruta = "pruebaspiloto-2e2e4.appspot.com";
            string api_key = "AIzaSyAbZsPmty8VXygIZ8eOKXufmHYyiNXqzF4";

            var auth = new FirebaseAuthProvider(new FirebaseConfig(api_key));
            var a = await auth.SignInWithEmailAndPasswordAsync(email, clave);

            var cancellation = new CancellationTokenSource();
            var task = new FirebaseStorage(
                ruta,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true
                })
                .Child("fotoPerfil")
                .Child(nombre)
                .PutAsync(archivo, cancellation.Token);

            var downloadURL = await task;
            return downloadURL;
        }
    }
}
