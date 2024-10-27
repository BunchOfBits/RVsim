namespace PicoSim;

public abstract class PortBase
{
  public string Name { get; init; }

  internal abstract void Set(object o);
}
