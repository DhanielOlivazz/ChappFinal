using ChappFinal.Menus;
namespace ChappFinal.Vistas;

public partial class Registro : ContentPage
{
	public Registro()
	{
		InitializeComponent();
	}
    public async void OnRegisterClicked(object sender, EventArgs e)
    {
        // Navegar a la ruta de MainMenu
        await Shell.Current.GoToAsync("//Menu");
    }

    public async void OnLogInTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//login");
    }

}