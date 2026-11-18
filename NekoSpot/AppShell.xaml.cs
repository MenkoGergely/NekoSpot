namespace NekoSpot
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("editcat", typeof(NekoEditPage));
            Routing.RegisterRoute("catdetails", typeof(NekoDetailsPage));
            Routing.RegisterRoute("catmap", typeof(NekoMapPage));
        }
    }
}
