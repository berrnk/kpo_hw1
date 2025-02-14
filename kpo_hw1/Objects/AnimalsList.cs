using kpo_hw1.Objects;
using kpo_hw1.Interfaces;
using kpo_hw1.Objects;

namespace kpo_hw1.Objects;

public class AnimalsList : IAnimals
{
    private List<Animal> _data = new List<Animal>();

    public void Add(Animal animal)
    {
        _data.Add(animal);
    }

    public void Remove(Animal animal)
    {
        _data.Remove(animal);
    }

    public IEnumerable<Animal> Get()
    {
        return _data;
    }
}