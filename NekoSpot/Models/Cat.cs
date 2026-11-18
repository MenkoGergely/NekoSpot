using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.VisualBasic;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NekoSpot
{
    public partial class Cat : ObservableObject
    {
        [property:AutoIncrement]
        [property:PrimaryKey]
        [ObservableProperty]
        int id;
        [ObservableProperty]
        string name;
        [ObservableProperty]
        string color;
        [ObservableProperty]
        string description;
        [ObservableProperty]
        DateTime seenDate;
        [ObservableProperty]
        double longitude;
        [ObservableProperty]
        double latitude;
        [ObservableProperty]
        string imageURL;

        public Cat GetCopy()
        {
            return (Cat)MemberwiseClone();
        }

    }
}
