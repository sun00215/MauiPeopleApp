using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;

namespace MauiPeopleApp.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    [ObservableProperty]
    private string statusMessage = "Ready to authenticate";

    [ObservableProperty]
    private bool isBusy;

    public bool IsNotBusy => !IsBusy;

    public LoginViewModel()
    {
        CheckBiometricAvailability();
    }

    [RelayCommand]
    public async Task LoginWithBiometric()
    {
        if (IsBusy) return;
        
        IsBusy = true;
        OnPropertyChanged(nameof(IsNotBusy));
        StatusMessage = "Checking biometric availability...";

        try
        {
            var isAvailable = await CrossFingerprint.Current.IsAvailableAsync(allowAlternativeAuthentication: true);
            
            if (!isAvailable)
            {
                StatusMessage = "Biometric authentication not available";
                await Shell.Current.DisplayAlert("Error", "Biometric authentication is not available on this device. Please use the skip option.", "OK");
                return;
            }

            StatusMessage = "Please authenticate...";
            
            var result = await CrossFingerprint.Current.AuthenticateAsync(new AuthenticationRequestConfiguration(
                "Secure Login", 
                "Please authenticate to access the People App")
            {
                CancelTitle = "Cancel",
                FallbackTitle = "Use PIN/Password"
            });

            if (result.Authenticated)
            {
                StatusMessage = "Authentication successful!";
                await NavigateToPeopleList();
            }
            else
            {
                StatusMessage = "Authentication failed";
                await Shell.Current.DisplayAlert("Authentication Failed", 
                    result.ErrorMessage ?? "Authentication was not successful. Please try again.", 
                    "OK");
            }
        }
        catch (Exception ex)
        {
            StatusMessage = "Authentication error occurred";
            await Shell.Current.DisplayAlert("Error", 
                $"An error occurred during authentication: {ex.Message}", 
                "OK");
        }
        finally
        {
            IsBusy = false;
            OnPropertyChanged(nameof(IsNotBusy));
        }
    }

    [RelayCommand]
    public async Task SkipAuthentication()
    {
        if (IsBusy) return;
        
        var result = await Shell.Current.DisplayAlert("Skip Authentication", 
            "Are you sure you want to skip biometric authentication? This is for demo purposes only.", 
            "Yes, Skip", "Cancel");
            
        if (result)
        {
            StatusMessage = "Skipping authentication (Demo Mode)";
            await NavigateToPeopleList();
        }
    }

    private async Task NavigateToPeopleList()
    {
        try
        {
            await Shell.Current.GoToAsync("//PeopleList");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Navigation Error", 
                $"Failed to navigate to people list: {ex.Message}", 
                "OK");
        }
    }

    private async void CheckBiometricAvailability()
    {
        try
        {
            var isAvailable = await CrossFingerprint.Current.IsAvailableAsync(allowAlternativeAuthentication: true);
            
            if (isAvailable)
            {
                StatusMessage = "Biometric authentication is available and ready";
            }
            else
            {
                StatusMessage = "Biometric authentication not available on this device";
            }
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error checking biometric availability: {ex.Message}";
        }
    }
}