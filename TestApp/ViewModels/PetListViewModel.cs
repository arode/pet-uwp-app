using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestApp.Common;
using TestApp.Models;
using Windows.UI.Xaml.Controls;

namespace TestApp.ViewModels
{
    public class PetListViewModel : BindableBase
    {
        public PetListViewModel()
        {
            IsFavoritesViewOpen = false;
            FavoritePets = new ObservableCollection<PetViewModel>();
        }

        private bool isFavoritesViewOpen;
        public bool IsFavoritesViewOpen
        {
            get
            {
                return isFavoritesViewOpen;
            }
            set
            {
                SetProperty(ref isFavoritesViewOpen, value);
            }
        }

        public ObservableCollection<PetViewModel> PetList = new ObservableCollection<PetViewModel>()
        {
            new PetViewModel{
                Name = "Comet",
                Age = 5,
                AnimalType = "Cat"
            },
            new PetViewModel{
                Name = "Charlie",
                Age = 3,
                AnimalType = "Dog"
            },
            new PetViewModel{
                Name = "Marvin",
                Age = 4,
                AnimalType = "Cat"
            }
        };

        public ObservableCollection<PetViewModel> FavoritePets { get; set; }

        private RelayCommand _navigateToBasketCommand { get; set; }
        public ICommand NavigateToBasketCommand
        {
            get
            {
                return _navigateToBasketCommand ?? (
                     _navigateToBasketCommand = new RelayCommand(() =>
                     {
                         FavoritePets = new ObservableCollection<PetViewModel>(PetList.Where(x => x.IsFavorited).ToList());
                         OnPropertyChanged(nameof(FavoritePets));
                         IsFavoritesViewOpen = !IsFavoritesViewOpen;
                     }));
            }
        }

        private RelayCommand _removeFromFavoritesCommand { get; set; }
        public ICommand RemoveFromFavoritesCommand => _removeFromFavoritesCommand ?? (
                     _removeFromFavoritesCommand = new RelayCommand(() =>
                     {

                     }));
    }
}
