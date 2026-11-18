using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace NekoSpot;

public partial class NekoMapPage : ContentPage
{
    NekoMapViewModel vm;
    public NekoMapPage(NekoMapViewModel vm)
    {
        InitializeComponent();
        this.vm = vm;
        BindingContext = vm;
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        await vm.InitAsync();          
        ShowPins();
    }

    private void ShowPins()
    {
        catMap.Pins.Clear();

   
        var located = vm.Cats.Where(c => c.Latitude != 0 || c.Longitude != 0).ToList();

        foreach (var cat in located)
        {
            var pin = new Pin
            {
                Label = cat.Name!=null? cat.Name :"",
                Address = cat.Color,
                Type = PinType.Place,
                Location = new Location(cat.Latitude, cat.Longitude)
            };

           
            pin.MarkerClicked += async (s, e) =>
            {
                e.HideInfoWindow = true;
                await vm.ShowCatDetailsAsync(cat);
            };

            catMap.Pins.Add(pin);
        }

        if (vm.FocusCat != null && (vm.FocusCat.Latitude != 0 || vm.FocusCat.Longitude != 0))
        {
            var center = new Location(vm.FocusCat.Latitude, vm.FocusCat.Longitude);
            catMap.MoveToRegion(MapSpan.FromCenterAndRadius(center, Distance.FromMeters(300)));
        }
        else if (located.Count > 0)
        {
            catMap.MoveToRegion(GetRegion(located));
        }
    }
    private static MapSpan GetRegion(List<Cat> cats)
    {
        double lat = cats.Average(c => c.Latitude);
        double lon = cats.Average(c => c.Longitude);
        var center = new Location(lat, lon);

        double maxKm = cats.Max(c =>
            Location.CalculateDistance(center, new Location(c.Latitude, c.Longitude), DistanceUnits.Kilometers));

        return MapSpan.FromCenterAndRadius(center, Distance.FromKilometers(Math.Max(maxKm * 1.3, 1)));
    }

    
}