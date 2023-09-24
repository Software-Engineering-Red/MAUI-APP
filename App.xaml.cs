namespace MauiApp1;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
        Current.UserAppTheme = AppTheme.Light;
        MainPage = new AppShell();
	}
}
