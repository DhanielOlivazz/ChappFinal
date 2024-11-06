using ChappFinal.Menus;
namespace ChappFinal.Vistas;

public partial class LogIn : ContentPage
{
	public LogIn()
	{
		InitializeComponent();
	}
    private async void OnRegisterTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//registro");
    }
    private async void OnLogInClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//Menu");
    }
}