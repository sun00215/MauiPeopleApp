using MauiPeopleApp.Views;

namespace MauiPeopleApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        
        // Register route for navigation
        Routing.RegisterRoute("PersonDetail", typeof(PersonDetailPage));
    }
}