using ChappFinal.Models.DTOs;
using System.Net.Http.Headers;
using ChappFinal.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace ChappFinal.Controllers
{
    public class UserController
    {
        private HttpClient _client;
        MultipartFormDataContent _content;
        private string _token = SecureStorage.GetAsync("auth_token").Result;
        public UserController()
        {
            _client = new HttpClient();
            _content = new MultipartFormDataContent();
            _client.BaseAddress = new Uri("https://chapp.domcloud.dev/api/");
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);
        }

        public async Task<List<string>> UpdateProfileAsync(DTO_User user, FileResult selectedFile)
        {
            try
            {
                // Convertir la imagen seleccionada a un arreglo de bytes
                byte[] imageBytes = await ConvertImageToByteArrayAsync(selectedFile);

                // Determinar el tipo MIME basado en la extensión del archivo
                var mimeType = GetMimeType(selectedFile.FileName);

                // Crear un ByteArrayContent con la imagen
                var byteArrayContent = new ByteArrayContent(imageBytes);
                byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue(mimeType); // Establecer el tipo MIME
                _content.Add(byteArrayContent, "url_img_avatar", selectedFile.FileName); // 'post_img_file' es el nombre del campo de imagen en el servidor

                // Agregar los demás campos del DTO_User como StringContent
                _content.Add(new StringContent(user.name.ToString()), "name");
                _content.Add(new StringContent(user.phone.ToString()), "phone");
                _content.Add(new StringContent(user.location.ToString()), "location");
                _content.Add(new StringContent(user.description.ToString()), "description");

                //Agregar las skills del usuario
                foreach (var skill in user.skills)
                {
                    _content.Add(new StringContent(skill), "skills[]");
                }

                // Realizar la solicitud POST
                var response = await _client.PostAsync("updateprofile/1?_method=PATCH", _content);
                var responseContent = await response.Content.ReadAsStringAsync();// Obtener la respuesta del servidor


                if (response.IsSuccessStatusCode)
                {
                    return new List<string> { "Perfil Atualizado", "Perfil actualizado con exito" };
                }
                else
                {
                    return new List<string> { "Error", "Error al actualizar el perfil" };
                }
            }
            catch (Exception e)
            {
                return new List<string> { "Error", e.ToString() };
                throw;
            }

        }


        public async Task<Users> GetProfileAsync()
        {
            try
            {
                var response = await _client.GetAsync("myprofile/");
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine(responseContent);
                    Users user = new Users();

                    var jsonResponse = JObject.Parse(responseContent);

                    //user = JsonConvert.DeserializeObject<Users>(responseContent);
                    user = jsonResponse["user"].ToObject<Users>();
                    return user;
                }

                Console.WriteLine(responseContent);
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine("--------------------------------------------------------------------------------   "+e.Message);
                return null;
                throw;
            }
        }

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
    }
}
