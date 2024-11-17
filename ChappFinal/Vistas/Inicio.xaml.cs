using ChappFinal.Controllers;
using ChappFinal.Models;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace ChappFinal.Vistas;

public partial class Inicio : ContentPage
{
    private PostController _postController;
    public ObservableCollection<Post> Posts { get; set; }
    

    public Inicio()
    {
        InitializeComponent();
        Loading(true); // Inicializa el indicador de carga
        _postController = new PostController();
        BindingContext = this; // Vincula el binding context con esta página
        LoadPosts(); // Carga los posts al iniciar
        Loading(false); // Finaliza el indicador de carga
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
        Loading(true);
        // Cargar los posts
        var posts = await _postController.GetPostsAsync();

        // Asignar los posts a la colección Observable
        Posts = new ObservableCollection<Post>(posts);
        OnPropertyChanged(nameof(Posts)); // Notificar el cambio de propiedad

        Loading(false);
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Llama al método para cargar los posts
        await LoadPosts();
    }

    private void Loading(bool status)
    {
        loadingIndicator.IsVisible = status;
        loadingIndicator.IsRunning = status;
        
    }

}
