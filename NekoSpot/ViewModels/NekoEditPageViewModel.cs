using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace NekoSpot
{
    [QueryProperty(nameof(EditedCat), "Cat")]
    public partial class NekoEditPageViewModel : ObservableObject
    {
        [ObservableProperty]
        Cat editedCat;
        [ObservableProperty]
        Cat draft;
        public void InitDraft()
        {
            Draft = EditedCat.GetCopy();
        }

        [RelayCommand]
        public async Task SaveEditAsync()
        {
            var param = new ShellNavigationQueryParameters
            {
                {"EditedCat",Draft }
            };
            await Shell.Current.GoToAsync("..", param);
        }
        [RelayCommand]
        public async Task CancelEditAsync()
        {
            await Shell.Current.GoToAsync("..");
        }


        [RelayCommand]
        private async Task OpenImageAsync()
        {
            FileResult? image = await MediaPicker.Default.PickPhotoAsync();
            if (image != null) await SaveImageAsync(image);
        }
        [RelayCommand]
        async Task TakePhotoAsync()
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                var image = await MediaPicker.Default.CapturePhotoAsync();
                if (image != null) await SaveImageAsync(image);
            }
            else
            {
                WeakReferenceMessenger.Default.Send("Camera is not supported on this device.");
                return;
            }
        }
        private async Task SaveImageAsync(FileResult image)
        {
            if (image is not null)
            {
                string localURL = Path.Combine(FileSystem.Current.AppDataDirectory, image.FileName);
                if (!File.Exists(localURL))
                {
                    using Stream stream = await image.OpenReadAsync();
                    using FileStream fs = File.OpenWrite(localURL);
                    await stream.CopyToAsync(fs);
                }
                Draft.ImageURL = localURL;
            }
        }
        [RelayCommand]
        public async Task GetLocationAsync()
        {
            try
            {
                Location? ownLocation = await Geolocation.Default.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Medium));
                if (ownLocation != null)
                {
                    Draft.Latitude = ownLocation.Latitude;
                    Draft.Longitude = ownLocation.Longitude;
                }
            }
            catch (Exception ex)
            {
                WeakReferenceMessenger.Default.Send($"Error: {ex.Message}");
            }

        }
    }
}
