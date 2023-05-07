using System;

namespace RVsim
{
  public class Port
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
          // To facilitate edge detection, during the event handler:
          // - Port.Value will contain the previous value
          // - args.NewValue will contain the new value
          PortChanged?.Invoke(this, new PortChangedEventArgs { NewValue = value });
          _value = value;
        }
      }
    }

    public event EventHandler<PortChangedEventArgs> PortChanged;
  }
}
