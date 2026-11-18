using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;

namespace NekoSpot
{
    [QueryProperty(nameof(EditedCat), "EditedCat")]
    public partial class NekoListPageViewModel : ObservableObject
    {
        ICatDatabase db;
        public ObservableCollection<Cat> Cats { get; private set; }

        
        [ObservableProperty]     
        Cat selectedCat;

        [ObservableProperty]
        Cat editedCat;

        async partial void OnEditedCatChanged(Cat value)
        {
            if (value != null)
            {
                if (SelectedCat != null)
                {
                    Cats.Remove(SelectedCat);
                    SelectedCat = null;
                    await db.UpdateCatAsync(value);
                }
                else
                {
                    await db.CreateCatAsync(value);
                }
                Cats.Add(value);
            }
            EditedCat = null;
        }
        public NekoListPageViewModel(ICatDatabase db)
        {
            this.db = db;
            Cats = new();
        }

        public async Task InitAsync()
        {
            var petList = await db.GetCatsAsync();
            Cats.Clear();
            petList.ForEach(Cats.Add);
        }
        [RelayCommand]
        public async Task ShowMapAsync() => await Shell.Current.GoToAsync("catmap");
        [RelayCommand]
        public async Task AddCatAsync()
        {
            SelectedCat = null;
            var param = new ShellNavigationQueryParameters
            {
                {"Cat", new Cat(){SeenDate=DateTime.Today } }

            };
            await Shell.Current.GoToAsync("editcat", param);

        }

        [RelayCommand]
        public async Task EditSelectedCatAsync()
        {
            if (SelectedCat != null)
            {
                var param = new ShellNavigationQueryParameters
                {
                    {"Cat", SelectedCat }
                };
                await Shell.Current.GoToAsync("editcat", param);
            }
            else
            {
                WeakReferenceMessenger.Default.Send("Select a cat to edit.");
            }
        }
        [RelayCommand]
        public async Task ShowCatDetailsAsync()
        {
            if(SelectedCat != null)
            {
                var param = new ShellNavigationQueryParameters
                {
                    {"Cat", SelectedCat }
                };
                await Shell.Current.GoToAsync("catdetails", param);
            }
            else
            {
                WeakReferenceMessenger.Default.Send("Select a cat first");
            }
           
        }
        [RelayCommand]
        public async Task DeleteCatAsync()
        {
            if (SelectedCat != null)
            {
                await db.DeleteCatAsync(SelectedCat);
                Cats.Remove(SelectedCat);
                SelectedCat = null;
            }
            else
            {
                WeakReferenceMessenger.Default.Send("Select a cat to delete");
            }
        }


    }
}
