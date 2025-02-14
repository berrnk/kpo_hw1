using kpo_hw1.Objects;
using kpo_hw1.Interfaces;


public class ThingsList : IThings
{
    private List<Thing> _data = new List<Thing>();

    public void Add(Thing thing)
    {
        _data.Add(thing);
    }

    public void Remove(Thing thing)
    {
        _data.Remove(thing);
    }

    public IEnumerable<Thing> Get()
    {
        return _data;
    }
}
