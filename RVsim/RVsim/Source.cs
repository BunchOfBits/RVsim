using System;

namespace RVsim
{
  public class Source<T>
    where T : IComparable
  {
    public Source(string name)
    {
      Port = new Port<T>(name);
    }

    public Port<T> Port { get; }

    public void Set(T p)
    {
      Port.Value = p;
    }
  }
}
