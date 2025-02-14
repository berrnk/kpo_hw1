using System;
using kpo_hw1.Objects;

namespace kpo_hw1.Interfaces;

public interface IAnimals
{
    void Add(Animal animal);
    void Remove(Animal animal);
    IEnumerable<Animal> Get();
}
