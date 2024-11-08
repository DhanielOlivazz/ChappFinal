using ChappFinal.Controllers;
using ChappFinal.Menus;
using ChappFinal.Models.DTOs;
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
        LoadingIdicador(true);
        var user = new DTO_User()
        {
            email = EmailEntry.Text,
            password = PasswordEntry.Text,
        };
        AuthController auth = new AuthController();

        var messages = await auth.LoginAsync(user);

        if (messages[1] == "Inicio de sesion exitoso")
        {
            LoadingIdicador(false);
            DisplayAlert(messages[0], messages[1], "OK");

            await Shell.Current.GoToAsync("//Menu");
        }
        else
        {
            LoadingIdicador(false);
            DisplayAlert(messages[0], messages[1], "OK");
        }
        
    }

    private void LoadingIdicador(bool status)
    {
        iniciarSesion.IsEnabled = !status;
        LoadingIndicator.IsRunning = status;
        LoadingIndicator.IsVisible = status;
        
    }
}