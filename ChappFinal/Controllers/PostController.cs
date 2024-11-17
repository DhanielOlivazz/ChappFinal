using ChappFinal.Models;
using ChappFinal.Models.DTOs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace ChappFinal.Controllers
{
    public class PostController
    {
        private HttpClient _client;
        private string _token = SecureStorage.GetAsync("auth_token").Result;
        public PostController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://chapp.domcloud.dev/api/");
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);
        }

        public async Task<List<string>> CreatePostAsync(DTO_Post post, FileResult selectedFile)
        {
            try
            {
                // Crear un objeto MultipartFormDataContent para enviar tanto los datos como la imagen
                var content = new MultipartFormDataContent();

                // Convertir la imagen seleccionada a un arreglo de bytes
                byte[] imageBytes = await ConvertImageToByteArrayAsync(selectedFile);

                // Determinar el tipo MIME basado en la extensión del archivo
                var mimeType = GetMimeType(selectedFile.FileName);

                // Crear un ByteArrayContent con la imagen
                var byteArrayContent = new ByteArrayContent(imageBytes);
                byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue(mimeType); // Establecer el tipo MIME
                content.Add(byteArrayContent, "post_img_file", selectedFile.FileName); // 'post_img_file' es el nombre del campo de imagen en el servidor

                // Agregar los demás campos del DTO_Post como StringContent
                content.Add(new StringContent(post.title), "title");
                content.Add(new StringContent(post.description), "description");
                content.Add(new StringContent(post.location), "location");
                content.Add(new StringContent(post.publication_date.ToString("yyyy-MM-dd")), "publication_date");

                foreach (var category in post.categories)
                {
                    content.Add(new StringContent(category), "categories[]");
                }
                content.Add(new StringContent(post.min_budget.ToString()), "min_budget");
                content.Add(new StringContent(post.max_budget.ToString()), "max_budget");

                // Realizar la solicitud POST
                var response = await _client.PostAsync("createpost/", content);

                var responseContent = await response.Content.ReadAsStringAsync(); // Obtener la respuesta del servidor

                if (response.IsSuccessStatusCode)
                {

                    return new List<string> { "Publicación", "Publicacion Exitosa" };
                }
                else
                {
                    // En caso de error, intenta deserializar errores de validación
                    var errors = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(responseContent);
                    var errorMessages = new List<string> { "Error de validación" };

                    foreach (var error in errors)
                    {
                        errorMessages.Add($"{error.Key.ToUpper()}: {string.Join(", ", error.Value)}");
                    }

                    return errorMessages;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex);
                return new List<string> { "Excepción", "No estás loggeado" };
            }
        }

        public async Task<List<Post>> GetPostsAsync()
        {
            try
            {
                var response = await _client.GetAsync("allposts/");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var jsonResponse = JObject.Parse(responseContent);

                    var posts = jsonResponse["posts"].ToObject<List<Post>>();
                    return posts;
                }
                else
                {
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }





        // Método para determinar el tipo MIME de la imagen
        private string GetMimeType(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLower();

            switch (extension)
            {
                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                default:
                    return "application/octet-stream";
            }
        }

        private async Task<byte[]> ConvertImageToByteArrayAsync(FileResult selectedFile)
        {
            using var fileStream = await selectedFile.OpenReadAsync(); // Abrimos la imagen
            using var memoryStream = new MemoryStream(); // Creamos un MemoryStream
            await fileStream.CopyToAsync(memoryStream); // Copiamos el contenido al MemoryStream
            return memoryStream.ToArray(); // Devolvemos la imagen como un arreglo de bytes
        }

        public void DeletePost() {
            throw new NotImplementedException();
        }
    }
}
