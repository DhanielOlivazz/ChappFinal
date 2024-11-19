using ChappFinal.Controllers;
using ChappFinal.Menus;
using ChappFinal.Models.DTOs;
namespace ChappFinal.Vistas;

public partial class LogIn : ContentPage
{
	public LogIn()
	{
		InitializeComponent();
        IsRemembered();

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

            // Guardar la sesión si el usuario lo desea
            SaveSesion();
            clearEntries();

            await Shell.Current.GoToAsync("//Menu");
        }
        else
        {
            LoadingIdicador(false);
            DisplayAlert(messages[0], messages[1], "OK");
            clearEntries();
        }
        
    }

    private void LoadingIdicador(bool status)
    {
        iniciarSesion.IsEnabled = !status;
        LoadingIndicator.IsRunning = status;
        LoadingIndicator.IsVisible = status;
        
    }

    private async void IsRemembered()
    {
        string? keep_session = SecureStorage.GetAsync("keep_session").Result;
        if (keep_session == "true")
        {
            await Shell.Current.GoToAsync("//Menu");
        }
        
    }
    private async void SaveSesion()
    {
        if (SaveUserCheckBox.IsChecked)
        {
            await SecureStorage.SetAsync("keep_session","true");
        }
        else
        {
            await SecureStorage.SetAsync("keep_session", "false");
        }
    }

    private void clearEntries()
    {
        EmailEntry.Text = "";
        PasswordEntry.Text = "";
    }
}