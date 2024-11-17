using ChappFinal.Controllers;
using ChappFinal.Models;
using ChappFinal.Models.DTOs;
using System.Collections.ObjectModel;
using System.Xml;

namespace ChappFinal.Vistas;

public partial class Perfil : ContentPage
{
    private DTO_Post _post;
    private DTO_User _user;
    public FileResult _fileresult;
    public Users User { get; set; }
    public ObservableCollection<Post> Posts { get; set; }
    public Perfil()
	{
		InitializeComponent();
        LoadProfile();
        BindingContext = this;
        
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Llama al método para cargar los posts
        await LoadProfile();
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

    public async Task LoadProfile()
    {
        UserController userController = new UserController();
        User = await userController.GetProfileAsync();
        Posts = new ObservableCollection<Post>(User.posts);
        OnPropertyChanged(nameof(User)); // Notificar el cambio de propiedad
    }

    private async void saveBtnEditProfile_Clicked(object sender, EventArgs e)
    {
        LoadingProfile(true);
        DTO_User user = await FillModelUser();


        if (user != null && _fileresult != null && !string.IsNullOrEmpty(_fileresult.FullPath))
        {
            UserController userController = new UserController();

            var messages = await userController.UpdateProfileAsync(user, _fileresult);

            if (messages[1] == "Perfil actualizado con exito")
            {
                LoadingProfile(false);
                await DisplayAlert(messages[0], "Perfil actualizado con exito", "OK");
                BindingContext = this;
                refresh();
                ClearEntriesProfile();


            }
            else
            {
                LoadingProfile(false);
                await DisplayAlert(messages[0], messages[1], "OK");
                
            }
            ClearEntriesProfile();

        }
        else
        {
            LoadingProfile(false);
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
            await Task.Delay(1000);
            await LoadProfile();
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
                categories = new string[] { categoryPicker.SelectedItem.ToString() }, // Convierte el valor seleccionado en un array de strings
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
                skills = new List<string> { skillsPicker.SelectedItem.ToString()}
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
        nameLabel.Text = _user.name;
        usernameLabel.Text = _user.name;
        contatoLabel.Text = _user.phone;
        locationLabel.Text = _user.location;
        labelDescription.Text = _user.description;
        profileImage.Source = _fileresult.FullPath;
    }

    private void Loading(bool status)
    {
        loadingIndicator.IsRunning = status;
        loadingIndicator.IsVisible = status;
        saveBtn.IsEnabled = !status;
        cerrarBtn.IsEnabled = !status;

    }

    private void LoadingProfile(bool status)
    {
        loadingIndicatorE.IsRunning = status;
        loadingIndicatorE.IsVisible = status;
        saveBtnEditProfile.IsEnabled = !status;
        closeBtnEditProfile.IsEnabled = !status;

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

    private void ClearEntriesProfile()
    {
        fotoImageEdit.Source = "user_icon.png";
        fullnameEntry.Text = "";
        descriptionPEntry.Text = "";
        phoneEntry.Text = "";
        locationPEntry.Text = "";
        EditarPerfilFrame.IsVisible = false;

    }

}