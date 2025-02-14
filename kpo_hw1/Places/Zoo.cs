using kpo_hw1.Objects;
using kpo_hw1.Interfaces;
using kpo_hw1.Objects;

namespace kpo_hw1.Places;

public class Zoo
{
    private const int KindnessThreshold = 5;

    private readonly IAnimals animalsList;
    private readonly IThings thingsList;
    public IHealth VetClinic { get; set; }

    public Zoo(IAnimals animalsRepository, IThings thingsRepository, IHealth vetClinic)
    {
        this.animalsList = animalsRepository;
        this.thingsList = thingsRepository;
        this.VetClinic = vetClinic;
    }

    public bool TryAddAnimal(Animal animal)
    {
        if (!VetClinic.CheckHealth(animal)) return false;
        animalsList.Add(animal);
        return true;
    }

    public void AddThing(Thing thing)
    {
        thingsList.Add(thing);
    }

    public void AddAnimals(IEnumerable<Animal> animals)
    {
        foreach (var animal in animals)
        {
            TryAddAnimal(animal);
        }
    }

    public IEnumerable<Animal> Animals => animalsList.Get();

    public IEnumerable<Thing> Things => thingsList.Get();

    public IEnumerable<Animal> GetPettingAnimals()
    {
        foreach (var animal in Animals)
        {
            if (animal is IKindness kindAnimal && kindAnimal.Kindness > KindnessThreshold)
            {
                yield return animal;
            }
        }
    }

    public int TotalAnimals => Animals.Count();

    public int TotalThings => Things.Count();

    public int GetTotalFood() => Animals.Sum(animal => animal.Food);
}
