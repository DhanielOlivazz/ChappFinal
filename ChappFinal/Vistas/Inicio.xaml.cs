using ChappFinal.Controllers;
using ChappFinal.Models;
using System.Collections.ObjectModel;

namespace ChappFinal.Vistas;

public partial class Inicio : ContentPage
{
    private PostController _postController;
    public ObservableCollection<Post> Posts { get; set; }
    

    public Inicio()
    {
        InitializeComponent();
        _postController = new PostController();
        BindingContext = this; // Vincula el binding context con esta página
        LoadPosts(); // Carga los posts al iniciar
    }

    private void OnNotificacionesClicked(object sender, EventArgs e)
    {
        // Mostrar el Frame de notificaciones
        NotificacionesFrame.IsVisible = true;
    }

    private void OnCerrarNotificacionesClicked(object sender, EventArgs e)
    {
        // Ocultar el Frame de notificaciones
        NotificacionesFrame.IsVisible = false;
    }

    public async Task LoadPosts()
    {
        // Cargar los posts
        var posts = await _postController.GetPostsAsync();

        // Asignar los posts a la colección Observable
        Posts = new ObservableCollection<Post>(posts);
        OnPropertyChanged(nameof(Posts)); // Notificar el cambio de propiedad
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Llama al método para cargar los posts, lo que actualizará la vista
        await LoadPosts();
    }

}
