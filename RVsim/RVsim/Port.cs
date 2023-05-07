using System;

namespace RVsim
{
  internal class Port
  {
    private uint _value;

    public Port(string name)
    {
      Name = name;
    }
    public string Name { get; }

    public uint Value
    {
      get
      {
        return _value;
      }

      set
      {
        _value = value;
        PortChanged?.Invoke(this, new PortChangedEventArgs { Port = value });
      }
    }

    public event EventHandler<PortChangedEventArgs> PortChanged;
  }
}
