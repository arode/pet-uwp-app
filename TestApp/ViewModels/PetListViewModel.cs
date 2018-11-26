using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PetApp.Common;
using PetApp.Data;
using PetApp.Models;
using Windows.UI.Xaml.Controls;

namespace PetApp.ViewModels
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
                Age = "Adult",
                AnimalType = "Cat"
            },
            new PetViewModel{
                Name = "Charlie",
                Age = "Young",
                AnimalType = "Dog"
            },
            new PetViewModel{
                Name = "Marvin",
                Age = "Young",
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
                     _navigateToBasketCommand = new RelayCommand((param) =>
                     {
                         FavoritePets = new ObservableCollection<PetViewModel>(PetList.Where(x => x.IsFavorited).ToList());
                         OnPropertyChanged(nameof(FavoritePets));
                         IsFavoritesViewOpen = !IsFavoritesViewOpen;
                     }));
            }
        }

        private RelayCommand _removeFromFavoritesCommand { get; set; }
        public ICommand RemoveFromFavoritesCommand
        {
            get
            {
                return _removeFromFavoritesCommand ?? (
              _removeFromFavoritesCommand = new RelayCommand((param) =>
              {
                  FavoritePets.Where(x => x.PetId == (Guid)param).First().IsFavorited = false;
                  FavoritePets.Remove(FavoritePets.Single(x => x.PetId == (Guid)param));
              }));
            }
        }

        private RelayCommand _closeFavoritesCommand { get; set; }
        public ICommand CloseFavoritesCommand
        {
            get
            {
                return _closeFavoritesCommand ?? (
              _closeFavoritesCommand = new RelayCommand((param) =>
              {
                  IsFavoritesViewOpen = false;
              }));
            }
        }

        private RelayCommand _loadedCommand { get; set; }
        public ICommand LoadedCommand
        {
            get
            {
                return _loadedCommand ?? (
              _loadedCommand = new RelayCommand(async (param) =>
              {
                  PetList.Clear();

                  var pets = await WebHelper.GetPetsAsync();

                  foreach (Pet pet in pets)
                  {
                      PetList.Add(new PetViewModel(pet));
                  }
              }));
            }
        }


    }
}
