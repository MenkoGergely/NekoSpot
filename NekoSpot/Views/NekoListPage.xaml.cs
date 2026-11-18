using CommunityToolkit.Mvvm.Messaging;

namespace NekoSpot
{
    public partial class NekoListPage : ContentPage
    {

        NekoListPageViewModel vm;
        public NekoListPage(NekoListPageViewModel vm)
        {
            InitializeComponent();
            this.vm = vm;
            BindingContext = vm;
            WeakReferenceMessenger.Default.Register<string>(this, async (r, m) =>
            {
                await DisplayAlert("Information", m, "OK");
            });
        }

        private async void ContentPage_Loaded(object sender, EventArgs e)
        {
            await vm.InitAsync();
        }
    }
}
