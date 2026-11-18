using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace NekoSpot
{
    [QueryProperty(nameof(Cat), "Cat")]
    public partial class NekoDetailsPageViewModel : ObservableObject
    {
        [ObservableProperty]
        Cat cat;
        [RelayCommand]
        public async Task OpenMapAsync()
        {
            if (Cat == null) return;

            var param = new ShellNavigationQueryParameters { { "Cat", Cat } };
            await Shell.Current.GoToAsync("catmap", param);
        }
    }
}
    


