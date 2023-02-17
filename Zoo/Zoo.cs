using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace Zoo
{
    public class Zoo
    {
        private List<Animal> animals;
        
        public Zoo(string name, int capacity)
        {
            this.animals = new List<Animal>();
            Name = name;
            Capacity = capacity;
        }
        public string Name { get; private set; }

        public int Capacity { get; private set; }

        public IReadOnlyCollection<Animal> Animals => animals;

        public string AddAnimal(Animal animal)
        {
            if (String.IsNullOrEmpty(animal.Species))
            {
                return $"Invalid animal species.";
            }

            if (animal.Diet != "herbivore" && animal.Diet != "carnivore")
            {
                return "Invalid animal diet.";
            }

            if (Animals.Count == Capacity)
            {
                return "The zoo is full.";
            }

            this.animals.Add(animal);
            return $"Successfully added {animal.Species} to the zoo.";
        }

        public int RemoveAnimals(string species)
        {
            return this.animals.RemoveAll(a => a.Species == species);
        }

        public List<Animal> GetAnimalsByDiet(string diet)
        {
            return this.animals.FindAll(a => a.Diet == diet);
        }

        public Animal GetAnimalByWeight(double weight)
        {
            return this.animals.FirstOrDefault(a => a.Weight == weight);
        }

        public string GetAnimalCountByLength(double minimumLength, double maximumLength)
        {
            int count = this.animals.Count(a => a.Length >= minimumLength && a.Length <= maximumLength);

            return $"There are {count} animals with a length between {minimumLength} and {maximumLength} meters.";
        }

    }
}
