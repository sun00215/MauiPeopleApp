using MauiPeopleApp.Models;
using MauiPeopleApp.ViewModels;

namespace MauiPeopleApp.Views;

[QueryProperty(nameof(Person), "Person")]
public partial class PersonDetailPage : ContentPage
{
    private Person _person;
    
    public Person Person
    {
        get => _person;
        set
        {
            _person = value;
            BindingContext = new PersonDetailViewModel(value);
        }
    }

    public PersonDetailPage()
    {
        InitializeComponent();
    }
}