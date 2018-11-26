using System;
using PetApp.Common;
using PetApp.Models;

namespace PetApp.ViewModels
{
    public class PetViewModel : BindableBase
    {
        public PetViewModel()
        {
            Pet = new Pet();
        }

        public PetViewModel(Pet pet)
        {
            Pet = new Pet();
            Pet.Name = pet.Name;
            Pet.Age = pet.Age;
            Pet.AnimalType = pet.AnimalType;
            IsFavorited = false;
        }

        private Pet Pet;

        public Guid PetId { get; } = Guid.NewGuid();

        public string Name
        {
            set
            {
                SetProperty(ref Pet.Name, value);
            }
            get
            {
                return Pet.Name;
            }
        }
        public string AnimalType
        {
            set
            {
                SetProperty(ref Pet.AnimalType, value);
            }
            get
            {
                return Pet.AnimalType;
            }
        }

        public string Age
        {
            set
            {
                SetProperty(ref Pet.Age, value);
            }
            get
            {
                return Pet.Age;
            }
        }

        public bool IsFavorited
        {
            get
            {
                return Pet.IsFavorited;
            }
            set
            {
                SetProperty(ref Pet.IsFavorited, value);
            }
        }
    }
}
