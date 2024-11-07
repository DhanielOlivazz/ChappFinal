namespace ChappFinal.Vistas;

public partial class Perfil : ContentPage
{
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
        CrearPublicacionFrame.IsVisible = false;
    }

    private async void SeleccionarImageButton_Clicked(object sender, EventArgs e)
    {
        var foto = await MediaPicker.PickPhotoAsync();
        fotoImage.Source = ImageSource.FromStream(() => foto.OpenReadAsync().Result);
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
        var foto = await MediaPicker.PickPhotoAsync();
        fotoImageEdit.Source = ImageSource.FromStream(() => foto.OpenReadAsync().Result);
    }
}