using System;
using TestApp.Common;
using TestApp.Models;

namespace TestApp.ViewModels
{
    public class PetViewModel : BindableBase
    {
        public PetViewModel()
        {
            Pet = new Pet();
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

        public uint? Age
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
