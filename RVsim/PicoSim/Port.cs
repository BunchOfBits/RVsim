using System;

namespace PicoSim
{
  public class Port<T> : PortBase
    where T : unmanaged, IComparable<T>
  {
    private T _value;

    public Port(string deviceName, string portName, T initialValue = default)
    {
      Name = string.IsNullOrEmpty(deviceName) ? portName : $"{deviceName}.{portName}";
      _value = initialValue;
    }

    public Port(string portName, T initialValue = default)
      : this(string.Empty, portName, initialValue)
    {
    }

    public T Value
    {
      get => _value;

      set
      {
        if (_value.CompareTo(value) != 0)
        {
          Scheduler.Instance.Schedule(this, value);
        }
      }
    }

    public event EventHandler<PortChangingEventArgs<T>> PortChanging;

    public event EventHandler PortChanged;

    internal override void Set(object o)
    {
      var value = (T)o;

      if (_value.CompareTo(value) == 0)
      {
        return;
      }

      PortChanging?.Invoke(this, new PortChangingEventArgs<T> { NewValue = value });
      _value = value;
      PortChanged?.Invoke(this, EventArgs.Empty);
    }

    public override string ToString() => $"{Name}={_value}";
  }
}
