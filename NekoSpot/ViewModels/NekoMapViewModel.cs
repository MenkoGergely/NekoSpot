using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace NekoSpot
{
    [QueryProperty(nameof(FocusCat),"Cat")]
    public partial class NekoMapViewModel : ObservableObject
    {
        ICatDatabase db;
        public ObservableCollection<Cat> Cats { get; } = new();
        [ObservableProperty]
        Cat focusCat;

        public NekoMapViewModel(ICatDatabase db) => this.db = db;

        public async Task InitAsync()
        {
            var list = await db.GetCatsAsync();
            Cats.Clear();
            list.ForEach(Cats.Add);
        }

        public async Task ShowCatDetailsAsync(Cat cat)
        {
            var param = new ShellNavigationQueryParameters { { "Cat", cat } };
            await Shell.Current.GoToAsync("catdetails", param);
        }
    }


}
