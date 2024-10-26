using System;

namespace PicoSim
{
  public class Source<T>
    where T : unmanaged, IComparable<T>
  {
    public Source(string name, T initialValue = default)
    {
      Port = new Port<T>(name, initialValue);

      Scheduler.Instance.Initialize(Port);
    }

    public Port<T> Port { get; }

    public void Set(T p)
    {
      if (Port.Value.CompareTo(p) == 0)
      {
        return;
      }

      Port.Value = p;

      Scheduler.Instance.Run();
    }
  }
}
