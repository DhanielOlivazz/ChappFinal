namespace ChappFinal.Menus;

public partial class MainMenu : Shell
{
	public MainMenu()
	{
		InitializeComponent();
        CurrentItem = this.Items[0].Items[1];
    }
}