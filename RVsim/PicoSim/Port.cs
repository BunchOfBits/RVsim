using System;

namespace PicoSim;

public class Port<T> : PortBase
    where T : unmanaged, IComparable<T>, IConvertible
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

    set => Scheduler.Instance.Schedule(this, value);
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

  public int Size()
  {
    return typeof(T) switch
    {
      { } t when t == typeof(bool) => 1,
      { } t when t == typeof(byte) => 8,
      { } t when t == typeof(ushort) => 16,
      { } t when t == typeof(uint) => 32,
      { } t when t == typeof(ulong) => 64,
      _ => throw new ArgumentException($"{typeof(T).Name} is not supported"),
    };
  }

  public override string ToString() => $"{Name}={_value}";
}
