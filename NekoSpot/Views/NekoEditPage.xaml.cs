using CommunityToolkit.Mvvm.Messaging;

namespace NekoSpot;

public partial class NekoEditPage : ContentPage
{
    NekoEditPageViewModel vm;
    public NekoEditPage(NekoEditPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        this.vm = vm;
        WeakReferenceMessenger.Default.Register<string>(this, async (r, m) =>
        {
            await DisplayAlert("Information", m, "OK");
        });
    }
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        vm.InitDraft();
    }
}