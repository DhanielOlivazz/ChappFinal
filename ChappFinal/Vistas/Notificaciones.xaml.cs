using System.Collections.ObjectModel;

namespace ChappFinal.Vistas;

public partial class Notificaciones : ContentView
{
	public Notificaciones()
	{
		InitializeComponent();
        BindingContext = new NotisViewModel();
	}
    // Clase que representa el ViewModel de Notificaciones
    public class NotisViewModel
    {
        public ObservableCollection<Notificacion> Notificaciones { get; set; }
        public NotisViewModel()
        {
            Notificaciones = new ObservableCollection<Notificacion>
            {
                new Notificacion { Titulo = "Contacta con Jayce", Descripcion = "Jayce ha solicitado hablar contigo, revisa tu bandeja" },
                new Notificacion { Titulo = "Contacta con Florentino", Descripcion = "Florentino ha solicitado hablar contigo, revisa tu bandeja" },
                new Notificacion { Titulo = "Contacta con Victor", Descripcion = "Victor ha solicitado hablar contigo, revisa tu bandeja" },
                new Notificacion { Titulo = "Contacta con Dante Castellon", Descripcion = "Dante Castellon ha solicitado hablar contigo, revisa tu bandeja" },
                new Notificacion { Titulo = "Contacta con Winston", Descripcion = "Winston ha solicitado hablar contigo, revisa tu bandeja" },
            };
        }
    }
    public class Notificacion
    {
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
    }
}