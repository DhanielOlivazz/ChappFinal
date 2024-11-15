using ChappFinal.Controllers;
using ChappFinal.Models.DTOs;

namespace ChappFinal.Vistas;

public partial class Perfil : ContentPage
{
    private DTO_Post _post;
    private DTO_User _user;
    public FileResult _fileresult;
    public Perfil()
	{
		InitializeComponent();
    }
    private void OnCrearPublicacionClicked(object sender, EventArgs e)
    {
        CrearPublicacionFrame.IsVisible = true;
    }

    // Método para ocultar el Frame de CrearPublicacionFrame
    private void OnCerrarCrearPublicacionClicked(object sender, EventArgs e)
    {
        ClearEntries();
    }

    // Método para seleccionar una imagen de la galería
    private async void SeleccionarImageButton_Clicked(object sender, EventArgs e)
    {
        _fileresult = await MediaPicker.PickPhotoAsync();
        if(_fileresult == null)
            return;
        fotoImage.Source = ImageSource.FromStream(() => _fileresult.OpenReadAsync().Result);


    }


    private async void saveBtnEditProfile_Clicked(object sender, EventArgs e)
    {
        DTO_User user = await FillModelUser();

        if (user != null && _fileresult != null && !string.IsNullOrEmpty(_fileresult.FullPath))
        {
            UserController userController = new UserController();

            var messages = await userController.UpdateProfileAsync(user, _fileresult);

            if (messages[1] == "Perfil actualizado con exito")
            {
                Loading(false);
                await DisplayAlert(messages[0], "Perfil actualizado con exito", "OK");
                refresh();
                
            }
            else
            {
                Loading(false);
                await DisplayAlert(messages[0], messages[1], "OK");
            }

            ClearEntries();
        }
        else
        {
            Loading(false);
            await DisplayAlert("Error", "La imagen es necesaria", "OK");
            return;
        }
    }


    private void EditProfile_Clicked(object sender, EventArgs e)
    {
        EditarPerfilFrame.IsVisible = true;
    }
    private void OnCerrarCrearEditClicked(object sender, EventArgs e)
    {
        EditarPerfilFrame.IsVisible = false;
    }

    private async void SeleccionarImageButtonEdit_Clicked(object sender, EventArgs e)
    {
        _fileresult = await MediaPicker.PickPhotoAsync();
        if (_fileresult == null)
            return;
        fotoImageEdit.Source = ImageSource.FromStream(() => _fileresult.OpenReadAsync().Result);
    }

    private async void saveBtn_Clicked(object sender, EventArgs e)
    {
        Loading(true);
        DTO_Post post = await FillModel();

        if (post != null && _fileresult != null && !string.IsNullOrEmpty(_fileresult.FullPath))
        {
            PostController postController = new PostController();

            var messages = await postController.CreatePostAsync(post, _fileresult);

            if (messages[1] == "Post creado con éxito")
            {
                Loading(false);
                await DisplayAlert(messages[0], "Publicación exitosa", "OK");
            }
            else
            {
                Loading(false);
                await DisplayAlert(messages[0], messages[1], "OK");
            }

            ClearEntries();
        }
        else
        {
            Loading(false);
            await DisplayAlert("Error", "La imagen es necesaria", "OK");
            return;
        }
        
    }


    // Metodo para convertir la imagen seleccionada en un array de bytes
    private async Task<byte[]> ConvertPicture()
    {
        var foto = await MediaPicker.PickPhotoAsync(); // Abre la galería para seleccionar una imagen
        fotoImage.Source = ImageSource.FromStream(() => foto.OpenReadAsync().Result); // Muestra la imagen seleccionada en la interfaz

        if (foto == null)
            return null;

        using var stream = await foto.OpenReadAsync(); // Abre la imagen seleccionada
        using var memoryStream = new MemoryStream(); // Crea un MemoryStream para almacenar la imagen
        await stream.CopyToAsync(memoryStream); // Copia la imagen al MemoryStream
        byte[] imageBytes = memoryStream.ToArray(); // Convierte el MemoryStream a un array de bytes

        return imageBytes;

    }

    // Método para llenar el modelo DTO_Post con los datos ingresados por el usuario
    private async Task<DTO_Post> FillModel()
    {
        try
        {
            _post = new DTO_Post
            {
                title = titleEntry.Text,
                description = descriptionEntry.Text,
                location = locationEntry.Text,
                publication_date = DateTime.Now, // Obtiene la fecha actual
                categories = new string[] { "Electricidad", "Manpostería", "Plomería" }, // Convierte el valor seleccionado en un array de strings
                min_budget = float.Parse(min_budgetEntry.Text),
                max_budget = float.Parse(max_budgetEntry.Text)

            };
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Debes llenar todos los campos", "OK");
        }
        

        return _post;
    }

    private async Task<DTO_User> FillModelUser()
    {
        
        try
        {
            _user= new DTO_User
            {
                name = fullnameEntry.Text,
                phone = phoneEntry.Text,
                location = locationPEntry.Text,
                description = descriptionPEntry.Text,
                skills = new List<string> { "sdsd","sdasd"}
            };
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Debes llenar todos los campos", "OK");
        }


        return _user;
    }

    public void refresh()
    {
        usernameLabel.Text = _user.name;
        contatoLabel.Text = _user.phone;
        locationLabel.Text = _user.location;
        locationLabel.Text = _user.description;
        profileImage.Source = _fileresult.FullPath;
    }

    private void Loading(bool status)
    {
        loadingIndicator.IsRunning = status;
        loadingIndicator.IsVisible = status;
        saveBtn.IsEnabled = !status;

    }

    private void ClearEntries()
    {
        fotoImage.Source = null;
        CrearPublicacionFrame.IsVisible = false;
        titleEntry.Text = "";
        descriptionEntry.Text = "";
        locationEntry.Text = "";
        min_budgetEntry.Text = "";
        max_budgetEntry.Text = "";
    }

    
}