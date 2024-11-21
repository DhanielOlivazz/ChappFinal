
using System.Collections.ObjectModel;

namespace ChappFinal.Vistas;

public partial class Proyectos : ContentPage
{
	public Proyectos()
	{
		InitializeComponent();
        BindingContext = new ProyectosViewModel();
    }
    public class ProyectosViewModel
    {
        public ObservableCollection<Chat> Chats { get; set; }

        public ProyectosViewModel()
        {
            Chats = new ObservableCollection<Chat>
        {
            new Chat { Name = "Angel Cordoba", LastMessage = "Trato cerrado!", Avatar = "perfil.png" },
            new Chat { Name = "Mar�a L�pez", LastMessage = "Nos vemos ma�ana", Avatar = "perfil.png" },
            new Chat { Name = "Juan P�rez", LastMessage = "Env�ame el documento", Avatar = "perfil.png" },
            new Chat { Name = "Sof�a Garc�a", LastMessage = "Gracias por todo!", Avatar = "perfil.png" },
            new Chat { Name = "Carlos G�mez", LastMessage = "Te llamo m�s tarde", Avatar = "perfil.png" },
            new Chat { Name = "Donatelo Osorio", LastMessage = "Revivan el server!", Avatar = "perfil.png" },
            new Chat { Name = "Casquillo Ruiz", LastMessage = "Sale proyectito?", Avatar = "perfil.png" },
            new Chat { Name = "Pedro", LastMessage = "Ma�ana por la ma�ana", Avatar = "perfil.png" },
            new Chat { Name = "Victor", LastMessage = "El lunes sin falta", Avatar = "perfil.png" },
            new Chat { Name = "Jayce", LastMessage = "Antes de lo planeado", Avatar = "perfil.png" },
            new Chat { Name = "Winston", LastMessage = "Mandame el dato cuando puedas", Avatar = "perfil.png" },
            new Chat { Name = "Florentino", LastMessage = "A las 8am", Avatar = "perfil.png" },
            new Chat { Name = "Dante Castellon", LastMessage = "Que m�s debo llevar?", Avatar = "perfil.png" }
        };
        }
    }
    public class Chat
    {
        public string Name { get; set; }
        public string LastMessage { get; set; }
        public string Avatar { get; set; }
    }
}