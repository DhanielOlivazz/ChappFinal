using ChappFinal.Controllers;
using ChappFinal.Menus;
using ChappFinal.Models.DTOs;
using System.Runtime.InteropServices;
namespace ChappFinal.Vistas;

public partial class Registro : ContentPage
{
	public Registro()
	{
		InitializeComponent();
        IsRemembered();

    }
    public async void OnRegisterClicked(object sender, EventArgs e)
    {
        LoadingIdicador(true);

        //Crear a object drom DTO_User
        var user = new DTO_User()
        {
            name = UsernameEntry.Text,
            email = emailEntry.Text,
            password = PasswordEntry.Text,
        };

        //Crear a object AuthController
        AuthController auth = new AuthController();

        //Llamar a la funcion RegisterAsync y registrar el objeto user
        var messages = await auth.RegisterAsync(user);

        //Verificar si el mensaje es de exito o error
        if (messages[1] == "User created successfully.")
        {
            LoadingIdicador(false);

            //Mesage success
            DisplayAlert(messages[0], "Usuario creado con exito", "OK");

            // Navegar a la ruta de MainMenu
            await Shell.Current.GoToAsync("//login");
            
        }
        else
        {
            LoadingIdicador(false);
            //Mesage error
            DisplayAlert(messages[0], messages[1], "OK");
        }
    }

    public async void OnLogInTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//login");
        //await Shell.Current.GoToAsync("//Menu");
    }

    //Funcion para mostrar el indicador de carga segun lo requerido
    public void LoadingIdicador(bool status)
    {
        registerBtn.IsEnabled = !status;
        LoadingIndicator.IsRunning = status;
        LoadingIndicator.IsVisible = status;
    }
    private async void IsRemembered()
    {
        LoadingIdicador(true);
        await Task.Delay(1000);
        string? keep_session = SecureStorage.GetAsync("keep_session").Result;
        if (keep_session == "true")
        {
            await Shell.Current.GoToAsync("//Menu");
        }
        LoadingIdicador(false);

    }

}