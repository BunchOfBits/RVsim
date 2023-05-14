using System;

namespace PicoSim
{
  public class Port<T>
    where T : IComparable<T>
  {
    private T _value;

    public Port(string deviceName, string portName, T initialValue)
    {
      Name = string.IsNullOrEmpty(deviceName) ? portName : $"{deviceName}.{portName}";
      _value = initialValue;
    }

    public Port(string portName)
      : this(string.Empty, portName, default)
    {
    }

    public Port(string portName, T initialValue)
      : this(string.Empty, portName, initialValue)
    {
    }

    public Port(string deviceName, string portName)
      : this(deviceName, portName, default)
    {
    }

    public string Name { get; }

    public T Value
    {
      get
      {
        return _value;
      }

      set
      {
        if (_value.CompareTo(value) != 0)
        {
          PortChanging?.Invoke(this, new PortChangingEventArgs<T> { NewValue = value });
          _value = value;
          PortChanged?.Invoke(this, EventArgs.Empty);
        }
      }
    }

    public event EventHandler<PortChangingEventArgs<T>> PortChanging;

    public event EventHandler PortChanged;
  }
}
