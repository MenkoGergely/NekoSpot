using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace NekoSpot;

public partial class NekoDetailsPage : ContentPage
{
    NekoDetailsPageViewModel vm;
    public NekoDetailsPage(NekoDetailsPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        this.vm = vm;
    }
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        ShowCatOnMap();
    }

    private void ShowCatOnMap()
    {
        var cat = vm.Cat;
        catMap.Pins.Clear();

        if (cat == null || (cat.Latitude == 0 && cat.Longitude == 0))
        {
            catMap.IsVisible = false;
            return;
        }

        catMap.IsVisible = true;
        var location = new Location(cat.Latitude, cat.Longitude);

        catMap.Pins.Add(new Pin
        {
            Label = cat.Name,
            Address = cat.Color,
            Type = PinType.Place,
            Location = location
        });

        catMap.MoveToRegion(MapSpan.FromCenterAndRadius(location, Distance.FromMeters(300)));
    }
}