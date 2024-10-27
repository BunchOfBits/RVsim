using System;
using System.Reflection;

namespace PicoSim;

public class Joiner<T>
  where T : unmanaged, IComparable<T>, IConvertible
{
  private readonly Port<bool>[] _d;
  private readonly MethodInfo _setter;

  public Port<T> Q { get; }

  public Joiner(string name, Port<bool>[] D)
  {
    _d = D;

    Q = new Port<T>(name, nameof(Q));
    _setter = Q.GetType().GetProperty(nameof(Q.Value))?.SetMethod;

    if (Q.Size() == 1)
    {
      throw new ArgumentException($"At least two bits are needed to join into a single port");
    }

    if (D.Length > Q.Size())
    {
      throw new ArgumentException($"Too many input bits ({D.Length}) for {typeof(T).Name}");
    }

    foreach (var port in D)
    {
      port.PortChanged += Setup;
    }
  }

  private void Setup(object sender, EventArgs e)
  {
    var d = 0UL;

    for (var i = 0; i < _d.Length; i++)
    {
      d += _d[i].Value ? 1UL << i : 0UL;
    }

    switch (Q.Size())
    {
      case 8:
        _setter.Invoke(Q, [(byte)d]);
        break;
      case 16:
        _setter.Invoke(Q, [(ushort)d]);
        break;
      case 32:
        _setter.Invoke(Q, [(uint)d]);
        break;
      case 64:
        _setter.Invoke(Q, [d]);
        break;
    }
  }
}