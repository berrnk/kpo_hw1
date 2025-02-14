using System;
using kpo_hw1.Objects;
namespace kpo_hw1.Interfaces;

public interface IThings
{
    IEnumerable<Thing> Get();
    void Add(Thing thing);
    void Remove(Thing thing);
}
