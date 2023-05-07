using System;

namespace RVsim
{
  internal class Port
  {
    private uint _value;

    public Port(string portName, uint initialValue = 0)
    {
      Name = $"{portName}";
      _value = initialValue;
    }

    public Port(string deviceName, string portName, uint initialValue = 0)
    {
      Name = $"{deviceName}.{portName}";
      _value = initialValue;
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
        if (_value != value)
        {
          _value = value;
          PortChanged?.Invoke(this, new PortChangedEventArgs { Port = value });
        }
      }
    }

    public event EventHandler<PortChangedEventArgs> PortChanged;
  }
}
