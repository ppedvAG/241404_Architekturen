// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var p1 = new Käse(new Käse( new Salami(new Pizza())));

Console.WriteLine($"{p1.Name} {p1.Preis}$");

interface IComponent
{
    string Name { get; }
    decimal Preis { get; }
}

abstract class Deco : IComponent
{
    public abstract string Name { get; }
    public abstract decimal Preis { get; }

    protected IComponent parent;

    protected Deco(IComponent parent)
    {
        this.parent = parent;
    }
}

class Salami : Deco
{
    public Salami(IComponent parent) : base(parent)
    {
    }

    public override string Name => parent.Name + " Salami";

    public override decimal Preis => parent.Preis + 1.5m;
}

class Käse : Deco
{
    public Käse(IComponent parent) : base(parent)
    {
    }

    public override string Name => parent.Name + " Käse";

    public override decimal Preis => parent.Preis + 2.3m;
}

class Brot : IComponent
{
    public string Name => "Brot";

    public decimal Preis => 3.00m;
}
class Pizza : IComponent
{
    public string Name => "Pizza";

    public decimal Preis => 5.00m;
}
