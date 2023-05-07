namespace RVsim
{
  public abstract class SourceBase
  {
    public SourceBase(string name)
    {
      Port = new Port(name);
    }

    public Port Port { get; }

    public void Set(uint p)
    {
      Port.Value = p;
    }
  }
}
