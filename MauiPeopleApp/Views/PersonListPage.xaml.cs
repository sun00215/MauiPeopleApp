using MauiPeopleApp.Models;
using MauiPeopleApp.ViewModels;

namespace MauiPeopleApp.Views;

public partial class PersonListPage : ContentPage
{
    private PersonListViewModel ViewModel => BindingContext as PersonListViewModel;

    public PersonListPage()
    {
        InitializeComponent();
        BindingContext = new PersonListViewModel();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (ViewModel.People.Count == 0)
            ViewModel.LoadPeopleCommand.Execute(null);
    }

    private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Person selectedPerson)
        {
            // Clear selection
            ((CollectionView)sender).SelectedItem = null;
            
            // Navigate to detail page
            await Shell.Current.GoToAsync($"PersonDetail", true, new Dictionary<string, object>
            {
                {"Person", selectedPerson}
            });
        }
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        var result = await DisplayAlert("Logout", "Are you sure you want to logout?", "Yes", "No");
        
        if (result)
        {
            // Navigate back to login page
            await Shell.Current.GoToAsync("//Login");
        }
    }
}