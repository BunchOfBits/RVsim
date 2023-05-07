using System;

namespace RVsim
{
  public class Port<T>
    where T : IComparable
  {
    private T _value;

    public Port(string portName, T initialValue = default)
    {
      Name = $"{portName}";
      _value = initialValue;
    }

    public Port(string deviceName, string portName, T initialValue = default)
    {
      Name = $"{deviceName}.{portName}";
      _value = initialValue;
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
