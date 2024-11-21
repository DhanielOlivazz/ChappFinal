
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
            new Chat { Name = "María López", LastMessage = "Nos vemos mañana", Avatar = "perfil.png" },
            new Chat { Name = "Juan Pérez", LastMessage = "Envíame el documento", Avatar = "perfil.png" },
            new Chat { Name = "Sofía García", LastMessage = "Gracias por todo!", Avatar = "perfil.png" },
            new Chat { Name = "Carlos Gómez", LastMessage = "Te llamo más tarde", Avatar = "perfil.png" },
            new Chat { Name = "Donatelo Osorio", LastMessage = "Revivan el server!", Avatar = "perfil.png" },
            new Chat { Name = "Casquillo Ruiz", LastMessage = "Sale proyectito?", Avatar = "perfil.png" },
            new Chat { Name = "Pedro", LastMessage = "Mañana por la mañana", Avatar = "perfil.png" },
            new Chat { Name = "Victor", LastMessage = "El lunes sin falta", Avatar = "perfil.png" },
            new Chat { Name = "Jayce", LastMessage = "Antes de lo planeado", Avatar = "perfil.png" },
            new Chat { Name = "Winston", LastMessage = "Mandame el dato cuando puedas", Avatar = "perfil.png" },
            new Chat { Name = "Florentino", LastMessage = "A las 8am", Avatar = "perfil.png" },
            new Chat { Name = "Dante Castellon", LastMessage = "Que más debo llevar?", Avatar = "perfil.png" }
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