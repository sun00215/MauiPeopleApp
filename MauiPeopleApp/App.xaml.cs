namespace MauiPeopleApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = new Window(new AppShell());
        
        // Navigate to login page after app starts
        Dispatcher.Dispatch(async () =>
        {
            await Shell.Current.GoToAsync("//Login");
        });
        
        return window;
    }
}